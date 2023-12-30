using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using TestPaperApi.Models;
using System.Net;
using OfficeOpenXml;
using System;
using System.Linq;

namespace TestPaperApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BulkUpload: ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public BulkUpload(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddQuestionBulk(string folderPath, string excelpath)
        {
            if (!Directory.Exists(folderPath))
            {
                return BadRequest(folderPath + " does not exist");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(excelpath))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Access first worksheet

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Loop through rows and columns to read data
                for (int row = 2; row <= rowCount; row++)
                {
                    // Subject
                    string subject = worksheet.Cells[row,1].Value.ToString();
                    await addSubjects(subject);

                    // paper 
                    string paper =  worksheet.Cells[row, 2].Value.ToString();
                    var parentsubject = _dbContext.subjects.Where(x => x.SubjectName == subject);
                    await addpaper(paper, parentsubject);
                    

                    // Images 
                    string imagename= worksheet.Cells[row, 3].Value !=null? worksheet.Cells[row, 3].Value.ToString():string.Empty;
                    string imageFile = Path.Combine(folderPath, imagename);
                    
                   
                    //Question Text
                    string questiontext = worksheet.Cells[row, 4].Value.ToString();

                    if(!string.IsNullOrWhiteSpace(imagename) || !string.IsNullOrWhiteSpace(questiontext))
                    {
                        var subsubject = _dbContext.subSubjects.
                            Where(x => x.fk_SubjectGroupId == parentsubject.FirstOrDefault().SubjectId);

                        SubjectQuestions que = new SubjectQuestions();
                        que.fk_SubjectId = parentsubject.FirstOrDefault().SubjectId;
                        que.fk_SubSubjectId= subsubject.FirstOrDefault().SubSubjectId;

                        if(!string.IsNullOrWhiteSpace(imagename) && imageFile.Length > 0)
                        {
                            FileStream fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read);
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                await fileStream.CopyToAsync(memoryStream);
                                que.ImageQuestion = memoryStream.ToArray();
                            }
                        }
                        que.Question = questiontext;
                        // Option1,Option2,Option3,Option4,Option5,isMultiplechoice ,Marks
                        que.Option1 = worksheet.Cells[row, 5].Value.ToString();
                        que.Option2 = worksheet.Cells[row, 6].Value.ToString();
                        que.Option3 = worksheet.Cells[row, 7].Value.ToString();
                        que.Option4 = worksheet.Cells[row, 8].Value.ToString();

                        string multichoice = worksheet.Cells[row, 10].Value != null ? worksheet.Cells[row,10].Value.ToString() : string.Empty;
                        que.IsMultipleChoice = multichoice == "Y" ? true : false;
                        que.Marks = int.Parse(worksheet.Cells[row, 11].Value.ToString());

                        //answers
                        que.Answers = worksheet.Cells[row, 12].Value.ToString();

                        //Order
                        que.Order = int.Parse(worksheet.Cells[row, 13].Value.ToString());

                        await _dbContext.subjectQuestions.AddAsync(que);
                        await _dbContext.SaveChangesAsync();

                    }
                }
            }


                return Ok("Added");
        }

        private async Task addpaper(string paper, IQueryable<Subjects> subjects)
        {
            var isexist = _dbContext.subSubjects.Any(x => x.SubSubjectName == paper);
            

            if (!isexist)
            {
                SubSubject sub = new SubSubject();
                sub.fk_userId = 1;
                sub.Duration = 3;
                sub.fk_SubjectGroupId = subjects.Count() > 0 ? subjects.FirstOrDefault().SubjectId : 0;
                sub.isComplete = false;
                sub.isVisible = true;
                sub.SubSubjectName = paper;
                sub.TotalMarks = 300;

                await _dbContext.subSubjects.AddAsync(sub);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task addSubjects(string subject)
        {
            var isexist = _dbContext.subjects.Any(x => x.SubjectName == subject);
            
            if (!isexist)
            {
                Subjects sub = new Subjects();
                sub.fk_UserId = 1;
                sub.CreatedDateTime = DateTime.Now;
                sub.SubjectLabel = subject;
                sub.SubjectName = subject;

                await _dbContext.subjects.AddAsync(sub);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

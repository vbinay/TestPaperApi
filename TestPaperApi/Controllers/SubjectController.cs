﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPaperApi.Models;

namespace TestPaperApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public SubjectController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Subjects>>> GetSubjects()
        {
           var subs= await  _dbContext.subjects.ToListAsync();

            if(subs.Any())
            {
                return subs;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetSubjectbyId")]
        public async Task<ActionResult<List<Subjects>>> GetSubjectbyId(int subjectid)
        {
            var subs = await _dbContext.subjects.ToListAsync();

            if (subs.Any())
            {
                return subs.Where(x => x.SubjectId == subjectid).ToList();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetSubjectByName")]
        public async Task<ActionResult<List<Subjects>>> GetSubjectByName(string subjectName)
        {
            var subs = await _dbContext.subjects.ToListAsync();

            if (subs.Any())
            {
                return subs.Where(x => x.SubjectName == subjectName).ToList();
            }
            else
            {
                return NoContent();
            }
        }

        //Commented now will modify according to requirement 
        //[HttpGet("getsubjectpapers")]
        //public async Task<ActionResult<List<SubjectPapers>>> GetSubjectPapers()
        //{
        //    var subs = await _dbContext.subjects.ToListAsync();
        //    var papers = await _dbContext.subSubjects.ToListAsync();



        //    var listofSubpapers = new List<SubjectPapers>();

        //    foreach(var s in subs)
        //    {
        //        var subtosend = new SubjectPapers();
        //        subtosend.SubjectId = s.SubjectId;
        //        subtosend.fk_UserId = s.fk_UserId;
        //        subtosend.SubjectLabel = s.SubjectLabel;
        //        subtosend.SubjectName = s.SubjectName;
        //        subtosend.CreatedDateTime = s.CreatedDateTime;
        //        var papercnt = papers.Count(x => x.fk_SubjectGroupId == s.SubjectId);
        //        subtosend.PapersCount = papercnt;

        //        listofSubpapers.Add(subtosend);
        //    }

        //    if (listofSubpapers.Any())
        //    {
        //        return listofSubpapers;
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}


        [HttpPost]
        public async  Task<ActionResult<Subjects>> AddSubjects(Subjects sub)
        {
            var subpresent = await _dbContext.subjects.ToListAsync();
            if(subpresent.Any())
            {
                if (!subpresent.Any(x => x.SubjectName == sub.SubjectName))
                {
                    await _dbContext.subjects.AddAsync(sub);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                await _dbContext.subjects.AddAsync(sub);
                await _dbContext.SaveChangesAsync();
            }
            return Ok(sub);
        }
    }
}

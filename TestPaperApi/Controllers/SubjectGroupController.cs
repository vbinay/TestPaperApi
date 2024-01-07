using Microsoft.AspNetCore.Cors;
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
    public class SubjectGroupController : ControllerBase
    {
        public readonly DatabaseContext _dbContext;
        public SubjectGroupController(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubjectGroup>>> GetSubjectGroups()
        {
           var subs= await  _dbContext.subjectGroup.ToListAsync();

            if(subs.Any())
            {
                return subs;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetSubjectGroupbyId")]
        public async Task<ActionResult<List<SubjectGroup>>> GetSubjectGroupbyId(int subjectGroupid)
        {
            var subs = await _dbContext.subjectGroup.ToListAsync();

            if (subs.Any())
            {
                return subs.Where(x => x.SubjectGroupId == subjectGroupid).ToList();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetSubjectByName")]
        public async Task<ActionResult<List<SubjectGroup>>> GetSubjectByName(string subjectgroupname)
        {
            var subs = await _dbContext.subjectGroup.ToListAsync();

            if (subs.Any())
            {

                return subs.Where(x => x.SubjectGroupName == subjectgroupname).ToList();
            }
            else
            {
                return NoContent();
            }
        }


        [HttpPost]
        public async  Task<ActionResult<SubjectGroup>> AddSubjectGroup(CustomSubjectGroup sub)
        {
            var subpresent = await _dbContext.subjectGroup.ToListAsync();

            if (subpresent.Any(x => x.SubjectGroupName == sub.SubjectGroupName))
            {
                var resultset = subpresent.Where(x => x.SubjectGroupName == sub.SubjectGroupName);
                foreach (var subject in sub.SubjectIds)
                {
                    var notpresent = resultset.Where(x => !sub.SubjectIds.Any(y => y == x.fk_SubjectId));
                    _dbContext.subjectGroup.RemoveRange(notpresent);
                    if (!resultset.Any(x => x.fk_SubjectId == subject))
                    {
                        var subgroupadd = new SubjectGroup();
                        subgroupadd.SubjectGroupId = resultset.First().SubjectGroupId;
                        subgroupadd.fk_UserId = sub.fk_UserId;
                        subgroupadd.SubjectGroupName = sub.SubjectGroupName;
                        subgroupadd.CreatedDateTime = DateTime.Now;
                        subgroupadd.fk_SubjectId = subject;
                        _dbContext.subjectGroup.Update(subgroupadd);
                    }
                }
            }
            else
            {
                int latestId = 0;
                if(subpresent.Count>0)
                {
                     latestId = subpresent.OrderByDescending(x => x.SubjectGroupId).First().SubjectGroupId;
                }
                
                foreach (var subject in sub.SubjectIds)
                {
                    var subgroupadd = new SubjectGroup();
                    subgroupadd.SubjectGroupId = latestId + 1;
                    subgroupadd.fk_UserId = sub.fk_UserId;
                    subgroupadd.SubjectGroupName = sub.SubjectGroupName;
                    subgroupadd.CreatedDateTime = DateTime.Now;
                    subgroupadd.fk_SubjectId = subject;
                    await _dbContext.subjectGroup.AddAsync(subgroupadd);
                }
            }
            await _dbContext.SaveChangesAsync();

            return Ok(sub);
        }
    }
}

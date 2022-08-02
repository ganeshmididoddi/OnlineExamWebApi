using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();

        // ADD Attempt

        [HttpPost]
        [Route("AddAttempt")]
        public IActionResult PostAttempt(Attempt attempt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Attempts.Add(attempt);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest("Something went wrong while saving record");
                }
            }
            return Created("Question Added Successfully", attempt);
        }


        //Get Attempts of users  by userid
        [HttpGet]
        [Route("GetAttempt/{id}")]
        public IActionResult GetAttempt(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }
            var data = (from attempt in db.Attempts where attempt.UserId == id select attempt);

            if (data == null)
            {
                return NotFound($"You have not Given Any Test");
            }
            return Ok(data);
        }

        /// Edit Attempt\       /// 
        /// 
        [HttpPut]
        [Route("EditAttempt")]
        public IActionResult PutAttempt([FromQuery] int attempt_id, [FromQuery] int level, [FromQuery] int marks)
        {
            
            if (ModelState.IsValid)
            {
                Attempt oldAttempt = db.Attempts.Find(attempt_id);
                if(level ==1)
                {
                    oldAttempt.LevelCleared = 1;
                    oldAttempt.LOneMarks = marks;
                }
                if (level == 2)
                {
                    oldAttempt.LevelCleared = 2;
                    oldAttempt.LTwoMarks = marks;
                }
                if (level == 3)
                {
                    oldAttempt.LevelCleared = 3;
                    oldAttempt.LThreeMarks = marks;
                }
                db.SaveChanges();
                return Ok("Done");
            }
            return BadRequest("Unable to edit the record");
        }


        
 
        public string updateAttempt( int attempt_id, int level, int marks, bool ispass)
        {

            if (ModelState.IsValid)
            {
                Attempt oldAttempt = db.Attempts.Find(attempt_id);
                if (level == 1)
                {
                    if (ispass == true) { oldAttempt.LevelCleared = 1; }
                    oldAttempt.LOneMarks = marks;
                }
                else if (level == 2)
                {
                    if (ispass == true) { oldAttempt.LevelCleared = 2; }
                    oldAttempt.LTwoMarks = marks;
                }
                else if (level == 3)
                {
                    if (ispass == true) { oldAttempt.LevelCleared = 3; }
                    oldAttempt.LThreeMarks = marks;
                }
                db.SaveChanges();
                return ("Done");
            }
            return ("Unable to edit the record");
        }


        //Answer Check
        [HttpPost]
        [Route("CheckResult")]
        public IActionResult CheckResultPost(List<CheckResult> ans, [FromQuery] int level_id, [FromQuery] int test_id, [FromQuery] int attempt_id)
        {
            resultresponse rr = new resultresponse();
            rr.Marks = 0;
            foreach (var data in ans)
            {
                var question = db.Questions.Where(q => q.QuestionId == data.Id).FirstOrDefault();
                if (question.OptionsCorrect == data.Answer)
                {
                    rr.Marks++;
                }

            }
            bool ispassed = false;
            var data1 = db.Tests.Where(t => t.TestId == test_id).FirstOrDefault();
            if (level_id == 1)
            {
                if (data1.LOneReq <= rr.Marks)
                {
                    ispassed = true;
                }

            }
            else if (level_id == 2)
            {
                if (data1.LTwoReq <= rr.Marks)
                {
                    {
                        ispassed = true;
                    }
                }


            }
            else if (level_id == 3)
            {
                if (data1.LThreeReq <= rr.Marks)
                {
                    ispassed = true;
                }

            }
            
            updateAttempt(attempt_id: attempt_id, level: level_id, marks: rr.Marks);
           

            rr.IsPassed = ispassed;
            return Ok(rr);
        }

    }


}

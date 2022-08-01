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
    public class QuestionsController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();
        // ADD QUESTIONS

        [HttpPost]
        [Route("AddQuestion")]
        public IActionResult PostQuestion(Question que)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Questions.Add(que);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest("Something went wrong while saving record");
                }
            }
            return Created("Question Added Successfully", que);
        }


        // Get QUESTIONS

        [HttpGet()]
        [Route("GetQuestion")]
        public IActionResult GetQuestion(int test_id, int level_id,int user_id)
        {
            //var data = db.Questions.Where(d => d.TestId == test_id && d.LevelId == level_id)

            var data = from q in db.Questions join a in db.Attempts on q.TestId equals a.TestId where q.TestId == test_id && q.LevelId == level_id && a.UserId==user_id
                       select new { question = q.Question1, option1=q.OptionsOne, option2 = q.OptionsTwo, option3 = q.OptionsThree, option4 = q.OptionsFour, ans=q.OptionsCorrect, attempt_id = a.AttemptId };
            
            if (data.Count() == 0)
            {
                return NotFound($"Cant Fetch Questions Select Proper levelid and testid");
            }
            return Ok(data);
        }


        // Add Attempt 

        [HttpPost()]
        [Route("AddAttempt")]
        public IActionResult PostAttempt([FromQuery] int test_id, [FromQuery] int level_id,[FromQuery] int user_id)
        {
           
            if(level_id==1)
            {
                Attempt attempt = new Attempt();

                if (ModelState.IsValid)
                {
                    try
                    {   

                        attempt.UserId = user_id;
                        attempt.TestId = test_id;
                        attempt.LevelCleared = 0;
                        attempt.LOneMarks = 0;
                        attempt.LTwoMarks = 0;
                        attempt.LThreeMarks = 0;
                        db.Attempts.Add(attempt);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return BadRequest("Something went wrong while saving record");
                    }
                }
                return GetQuestion(level_id: level_id, test_id:test_id, user_id:user_id);
                
                
            }
             else 
             if(level_id==2 || level_id==3)
            {
                return GetQuestion(level_id: level_id, test_id: test_id, user_id: user_id);
            }

            return NotFound("No Data ");
        }
    }

}

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
                return Ok();
            }
            return BadRequest("Unable to edit the record");
        }
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /// ADMIN ANAYZE
        /// 

        [HttpGet]
        [Route("GetAnalysis")]
        public IActionResult GetAnalysis([FromQuery]int test_id, [FromQuery] int level, [FromQuery] int? marks, [FromQuery] string? qualification, [FromQuery] string?city)
        {
           
           var data = (from attempt in db.Attempts where attempt.TestId == test_id && attempt.LevelCleared==level select attempt);

            if (data == null)
            {
                return NotFound($"No Any record Found");
            }
            return Ok(data);
        }
    }
}

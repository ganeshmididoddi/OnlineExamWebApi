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

        [HttpGet()]
        [Route("GetQuestion")]
        public IActionResult GetQuestion([FromQuery] int test_id, [FromQuery] int level_id)
        {
            var data = db.Questions.Where(d => d.TestId ==test_id && d.LevelId == level_id);
            if (data.Count() == 0)
            {
                return NotFound($"Cant Fetch Questions check levelid and testid are proper");
            }
            return Ok(data);
        }
    }

}

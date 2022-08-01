using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExamWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineExamWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();
                       
        // GET: api/<TestController>

        // Create New Test 

        [HttpPost]
        [Route("AddTest")]
        public IActionResult PostTest(Test test)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Tests.Add(test);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest("Something went wrong while saving record");
                }
            }
            return Created("Test Created Successfully", test);
        }


        // Get Test By ID


        [HttpGet]
        [Route("GetTest/{id}")]
        public IActionResult GetTest(int? id)
        {
            if (id == null)
            {
                return BadRequest("Test-Id cannot be null");
            }
            var data = (from test in db.Tests where test.TestId == id select test);

            if (data.Count() == 0)
            {
                return NotFound($"Test {id} not present");
            }
            return Ok(data);
        }


        // Get Test ID AND SUBJECT NAME

        [HttpGet]
        [Route("GetTestList")]
        public IActionResult GetTestList()
        {

            var data =(from test in db.Tests select new  {Id=test.TestId, Subject=test.SubjectName });
            return Ok(data.ToList());
        }

        // GET NOT GIVEN TEST
        [HttpGet]
        [Route("GetToAttempt")]
        public IActionResult GetNotAttempted([FromQuery]int user_id)
        {
            var data = db.Tests.FromSqlInterpolated<Test>($"gettoattempt @user_id={user_id}");
            return Ok(data);
        }


        //Delete Test by Id

        [HttpDelete]
        [Route("DeleteTest/{id}")]
        public IActionResult DeleteTest(int id)
        {
            var data = db.Tests.Find(id);
            db.Tests.Remove(data);
            db.SaveChanges();
            return Ok($"{data.SubjectName} Test Deleted");
        }


        // GET ALL TEST

        [HttpGet]
        [Route("GetTest")]
        public IActionResult GetTest()
        {
            var data = from test in db.Tests select test;
            return Ok(data);
        }
    }
}

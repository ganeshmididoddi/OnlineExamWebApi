using Microsoft.AspNetCore.Mvc;
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
        // 


        [HttpGet]
        [Route("GetTest/{id}")]
        public IActionResult GetTest(int? id)
        {
            if (id == null)
            {
                return BadRequest("Test-Id cannot be null");
            }
            var data = (from test in db.Tests where test.TestId == id select new { Id = test.TestId, Subject_Name = test.SubjectName, Date=test.TestDate,Duration=test.Duration,L1marks=test.LOneReq,l2marks=test.LTwoReq,l3marks=test.LThreeReq,
            adminid=test.AdminId});

            if (data.Count() == 0)
            {
                return NotFound($"Test {id} not present");
            }
            return Ok(data);
        }




        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

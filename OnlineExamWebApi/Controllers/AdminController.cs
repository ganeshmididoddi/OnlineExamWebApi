using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineExamWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineExamWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();

        //Get All Admin 

        [HttpGet]
        [Route("GetAdmins")]
        public IActionResult GetUser()
        {
            var data = from admin in db.Admins select new { Id = admin.AdminId, Name = admin.Username };
            return Ok(data);
        }


        [HttpGet]
        [Route("GetAnalysis")]
        public IActionResult GetAnalysis([FromQuery] int test_id, [FromQuery] int level, [FromQuery] int? marks, [FromQuery] string? qualification, [FromQuery] string? city)
        {
            List<Attempt> attemptlist = db.Attempts.Include("User").Where(c => c.TestId == test_id && c.LevelCleared == level && (c.LOneMarks >= marks || c.LTwoMarks >= marks || c.LThreeMarks >= marks)).Include("Test").ToList();

            List<Admin_Analysis> analysislist = new List<Admin_Analysis>();
            foreach (var attempt in attemptlist)
            {
                if (attempt.User.City == city && attempt.User.Qualification == qualification)
                {
                    Admin_Analysis analysis = new Admin_Analysis();
                    analysis.UserId = attempt.User.UserId;
                    analysis.Name = attempt.User.Name;
                    analysis.Phone = attempt.User.Phone;
                    analysis.Email = attempt.User.Email;
                    analysis.College = attempt.User.College;
                    analysis.YearOfPassing = attempt.User.YearOfPassing;
                    analysis.Qualification = attempt.User.Qualification;
                    analysis.City = attempt.User.City;
                    analysis.Gender = attempt.User.Gender;
                    analysis.DateOfBirth = attempt.User.DateOfBirth;

                    analysis.AttemptId = attempt.AttemptId;
                    analysis.TestId = attempt.TestId;
                    analysis.LevelCleared = attempt.LevelCleared;
                    analysis.LOneMarks = attempt.LOneMarks;
                    analysis.LTwoMarks = attempt.LTwoMarks;
                    analysis.LThreeMarks = attempt.LThreeMarks;
                    analysis.SubjectName = attempt.Test.SubjectName;
                    if (analysis.LevelCleared == 1) analysis.Marks = analysis.LOneMarks;
                    if (analysis.LevelCleared == 1) analysis.Marks = analysis.LTwoMarks;
                    else analysis.Marks = analysis.LTwoMarks;

                    analysislist.Add(analysis);
                }

            }
            if (analysislist.Count == 0)
            {
                return NotFound(" No Record found");
            }
            return Ok(analysislist);
        }




        // FIND ALL Users who Attempted the test

        [HttpGet]
        [Route("GetAllAnalysis")]
        public IActionResult GetAllAnalysis([FromQuery] int admin_id)
        {
            try
            {
                var data = db.AdminAllAnalyses.FromSqlInterpolated<AdminAllAnalysis>($"getallanalysis @admin_id={admin_id}");
                
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
    }
}

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
    public class UserController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();


        //Get All Users

        // GET: api/<UserController>
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUser()
        {
            var data = from user in db.Users select user;
            return Ok(data);
        }

        //Login for User

        [HttpPost()]
        [Route("UserLogin")]
        public IActionResult PostUser(Login user)
        {
            if (user.Role == "user") {
                var data = db.Users.Where(d => d.Email == user.Username && d.Password == user.Password);
                if (data.Count() == 0)
                {
                    return NotFound($"Invalid Credintials");
                }
                return Ok(data);
            }
            else {
                var data = db.Admins.Where(d => d.Username == user.Username && d.Password == user.Password);
                if (data.Count() == 0)
                {
                    return NotFound($"Invalid Credintials");
                }
                return Ok(data);
            }
        }



        //Registration for new Users

        // POST api/<UserController>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    //calling a store procedure
                    // db.Database.ExecuteSqlInterpolated($"adddept {dept.Id} {dept.Name} {dept.Location}");
                }
                catch (Exception)
                {
                    return BadRequest("Email-Id Alredy Exist");
                }
            }
            return Created("Record Successfull Added", user);
        }

        //Get User by id
        [HttpGet]
        [Route("GetUsers/{id}")]
        public IActionResult GetUser(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }
            var data = (from user in db.Users where user.UserId == id select user);
            
            if (data.Count() == 0)
            {
                return NotFound($"User {id} not present");
            }
            return Ok(data);
        }


        // Forget Password

        [HttpGet()]
        [Route("ResetPassword/{email}")]
        public IActionResult PutUser(string email)
        {
            if (email == null)
            {
                return BadRequest("Email cannot be null");
            }
            var data = db.Users.Where(d => d.Email == email).FirstOrDefault();
            
            if (data == null)
            {
                return NotFound($"User Does not exist");
            }
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
           
            ////User ouser = (User)db.Users.Where(User=>User.Email==email);
            //var data1 = (from user in db.Users where user.Email == email select new { Id = user.UserId, } );
            //Console.WriteLine(data1);
            User ouser = db.Users.Find(data.UserId);
            ouser.Password = finalString;
            db.SaveChanges();
            return Ok($"Hey {email} your New Password is {finalString}");

        }

        // User analysis

        [HttpGet]
        [Route("GetAnalysis")]
        public IActionResult GetAnalysis([FromQuery] int user_id)
        {
            List<Attempt> attemptlist = db.Attempts.Include("Test").Where(c => c.UserId == user_id).ToList();

            List<User_Analysis> analysislist = new List<User_Analysis>();
            foreach (var attempt in attemptlist)
            {
                User_Analysis analysis = new User_Analysis();

                analysis.AttemptId = attempt.AttemptId;
                analysis.UserID = attempt.UserId;
                analysis.TestId = attempt.TestId;
                analysis.SubjectName = attempt.Test.SubjectName;
                analysis.LevelCleared = attempt.LevelCleared;
                analysis.LOneMarks = attempt.LOneMarks;
                analysis.LTwoMarks = attempt.LTwoMarks;
                analysis.LThreeMarks = attempt.LThreeMarks;
                if (analysis.LevelCleared == 1) analysis.Marks = analysis.LOneMarks;
                if (analysis.LevelCleared == 1) analysis.Marks = analysis.LTwoMarks;
                else analysis.Marks = analysis.LTwoMarks;

                analysislist.Add(analysis);
            }
            if (analysislist.Count == 0)
            {
                return NotFound(" No Record found");
            }
            return Ok(analysislist);
        }

    }
}

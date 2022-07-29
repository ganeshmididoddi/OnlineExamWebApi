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
    public class UserController : ControllerBase
    {
        onlineexamContext db = new onlineexamContext();


        //Get All Users

        // GET: api/<UserController>
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUser()
        {
            var data = from user in db.Users select new { Id = user.UserId, Name = user.Name, Email=user.Email, Mobile=user.Phone, College= user.College, YOP=user.YearOfPassing, City = user.City, };
            return Ok(data);
        }

        //Login for User

        [HttpPost()]
        [Route("UserLogin")]
        public IActionResult PostUser(Login user)
        {
            var data = db.Users.Where(d => d.Email == user.Username && d.Password == user.Password);
            if (data.Count() == 0)
            {
                return NotFound($"Invalid Credintials");
            }
            return Ok(data);
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
                    return BadRequest("Something went wrong while saving record");
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
            var data = (from user in db.Users where user.UserId == id select new { Id = user.UserId, Name = user.Name, Email = user.Email, Mobile = user.Phone, College = user.College, YOP = user.YearOfPassing, City = user.City });
            
            if (data.Count() == 0)
            {
                return NotFound($"User {id} not present");
            }
            return Ok(data);
        }


        // Forget Password

        [HttpGet()]
        [Route("ForgetPassword")]
        public IActionResult GetUser([FromQuery] string email)
        {
            if (email == null)
            {
                return BadRequest("Email cannot be null");
            }
            var data = db.Users.Where(d => d.Email == email);
            if (data.Count() == 0)
            {
                return NotFound($"User email Incorrect");
            }
            return Ok($"Email sent to {email} Successfully");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}

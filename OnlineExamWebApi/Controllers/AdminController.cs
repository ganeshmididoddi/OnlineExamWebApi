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



    }
}

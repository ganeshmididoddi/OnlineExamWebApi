﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamWebApi.Models
{
    public class Login
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
    }
}

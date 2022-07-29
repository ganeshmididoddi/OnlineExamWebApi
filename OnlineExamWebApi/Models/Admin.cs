using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Tests = new HashSet<Test>();
        }

        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class User
    {
        public User()
        {
            Attempts = new HashSet<Attempt>();
        }

        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string College { get; set; }
        public int YearOfPassing { get; set; }
        public string Qualification { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Verified { get; set; }

        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}

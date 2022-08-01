using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamWebApi.Models
{
    public class Admin_Analysis
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string College { get; set; }
        public int YearOfPassing { get; set; }
        public string Qualification { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string SubjectName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int AttemptId { get; set; }
        public int TestId { get; set; }
        public int LevelCleared { get; set; }
        public int Marks { get; set; }
        public int LOneMarks { get; set; }
        public int LTwoMarks { get; set; }
        public int LThreeMarks { get; set; }
    }
}

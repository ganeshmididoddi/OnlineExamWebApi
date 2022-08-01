using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamWebApi.Models
{
    public class User_Analysis
    {
        public int AttemptId { get; set; }
        public int UserID { get; set; }
        public int TestId { get; set; }
        public string SubjectName { get; set; }
        public int LevelCleared { get; set; }
        public int Marks { get; set; }
        public int LOneMarks { get; set; }
        public int LTwoMarks { get; set; }
        public int LThreeMarks { get; set; }
    }
}

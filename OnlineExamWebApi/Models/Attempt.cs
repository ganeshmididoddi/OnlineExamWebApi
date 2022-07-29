using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class Attempt
    {
        public int AttemptId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int LevelCleared { get; set; }
        public int LOneMarks { get; set; }
        public int LTwoMarks { get; set; }
        public int LThreeMarks { get; set; }

        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
    }
}

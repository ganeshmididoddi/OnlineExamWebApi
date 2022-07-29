using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class Test
    {
        public Test()
        {
            Attempts = new HashSet<Attempt>();
            Questions = new HashSet<Question>();
            Questionsadvanceds = new HashSet<Questionsadvanced>();
        }

        public int TestId { get; set; }
        public string SubjectName { get; set; }
        public DateTime TestDate { get; set; }
        public int Duration { get; set; }
        public int LOneReq { get; set; }
        public int LTwoReq { get; set; }
        public int LThreeReq { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Questionsadvanced> Questionsadvanceds { get; set; }
    }
}

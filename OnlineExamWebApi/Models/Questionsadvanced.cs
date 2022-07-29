using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class Questionsadvanced
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public int IsSingleCorrect { get; set; }
        public string Question { get; set; }
        public int LevelId { get; set; }
        public string OptionsOne { get; set; }
        public string OptionsTwo { get; set; }
        public string OptionsThree { get; set; }
        public string OptionsFour { get; set; }
        public string OptionsCorrect { get; set; }

        public virtual Test Test { get; set; }
    }
}

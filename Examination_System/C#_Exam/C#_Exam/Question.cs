using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public class Question
    {
        public string HeaderOfTheQuestion { get; set; }
        public string BodyOfTheQuestion { get;  set; }
        public decimal Marks { get; set; }

        public List<Answers> AnswerList { get; set; }

    }
}

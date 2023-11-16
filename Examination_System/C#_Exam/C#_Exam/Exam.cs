using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public abstract class Exam
    {
        public int TimeOfExam { get; set; }
        public int NumberOfQuestion { get; set; }

        public Subject AssociatedToSubject { get; set; }

        public Exam(int timeOfExam, int numberOfQuestion , Subject associatedToSubject)
        {
            TimeOfExam = timeOfExam;
            NumberOfQuestion = numberOfQuestion;
            AssociatedToSubject = associatedToSubject;
        }

        public abstract void AddQuestion(Question question);
        public abstract void ShowExam();
    }
}

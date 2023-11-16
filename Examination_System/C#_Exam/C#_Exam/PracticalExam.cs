using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public class PracticalExam : Exam
    {
        private List<Question> questions;

        
        public PracticalExam(int _TimeOfExam, int _NumberOfQuestions, Subject subject)
            : base(_TimeOfExam, _NumberOfQuestions, subject)
        {
            questions = new List<Question>();
        }

        public override void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        public override void ShowExam()
        {
            Console.WriteLine("Practical Exam");
            foreach (var question in questions)
            {

               
                Helper.CreateMCQQuestion(question);


                Console.WriteLine();
            }

            

            Console.Clear();

            Helper.ShowResultExam(questions);
            
        }
    }
}
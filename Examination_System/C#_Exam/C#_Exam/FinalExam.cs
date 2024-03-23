using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public class FinalExam : Exam
    {
        private List<Question> questions;

        public FinalExam(int _TimeOfExam, int _NumberOfQuestions, Subject subject)
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
            Console.WriteLine("Final Exam");
            foreach (var question in questions)
            {
                

                if (question is TFQuestion)
                {
                    Helper.CreateTFQuestion(question);
                    
                }
                else if (question is MCQOneChoice)
                {
                    Helper.CreateMCQQuestion(question);
                    
                }

                Console.WriteLine();
                Console.WriteLine("==============================================");
            }

            Console.Clear();

            Helper.ShowResultExam(questions);
           

        }
    }
}

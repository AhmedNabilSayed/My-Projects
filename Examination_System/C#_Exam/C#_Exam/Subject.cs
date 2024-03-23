using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public Exam ExamOfSubject { get; set; }

        public void CreateExam()
        {
            ExamOfSubject =  Helper.CreateExam(ExamOfSubject, this);
        }

        //public void CreateExam()
        //{
        //    int examType;
        //    int timeOfExam;
        //    int quetionsNumber;
        //    Console.Write("Please Enter The Type Of Exam You Want To Create ( 1 for Practical and 2 for Final):");
        //    examType = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Please Enter The Time Of Exam in Minutes:");
        //    timeOfExam = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Please Enter The NUmber Of Questions You Wanted To Create:");
        //    quetionsNumber = int.Parse(Console.ReadLine());

        //    Console.Clear();

        //    if( examType == 1 )
        //    {
        //        ExamOfSubject = new PracticalExam(timeOfExam, quetionsNumber, this);
        //        for (int i = 0; i < quetionsNumber; i++)
        //        {

        //            Console.WriteLine("Choose One Answer Question");
        //            Console.WriteLine("Please Enter The Body Of The Question:");
        //            string body = Console.ReadLine();

        //            Console.WriteLine("Please Enter The Marks Of Question:");
        //            int marks = int.Parse(Console.ReadLine());

        //            string[] options = new string[3];

        //            Console.WriteLine("The Choices Of The Questions :");
        //            for (int j = 0; j < options.Length; j++)
        //            {
        //                Console.Write($"Please Enter The Choice Number {j + 1} : ");
        //                options[j] = Console.ReadLine();
        //            }

        //            Console.WriteLine("Please Enter The Right Answer of Question: ");
        //            int correctIndex = int.Parse(Console.ReadLine());

        //            var question = new MCQOneChoice()
        //            {
        //                HeaderOfTheQuestion = "Choose One Answer Question",
        //                BodyOfTheQuestion = body,
        //                Marks = marks,
        //                Options = options,
        //                correctOptionIndex = correctIndex,
        //                AnswerList = new List<Answers>
        //                {
        //                    new Answers { AnswerId = correctIndex, AnswerText = options[correctIndex-1] },
        //                }


        //            };

        //            ExamOfSubject.AddQuestion(question);
        //            Console.Clear();
        //        }
                

        //    }
        //    else if( examType == 2 )
        //    {
        //        ExamOfSubject = new FinalExam(timeOfExam, quetionsNumber, this);

        //        for (int i = 0; i < quetionsNumber; i++)
        //        {
        //            int TForMCQ;
        //            Console.WriteLine($"Please Choose Type Of Question Number({i + 1}) (1 for True or False and 2 for MCQ):");
        //            TForMCQ = int.Parse(Console.ReadLine());
        //            if (TForMCQ == 1)
        //            {
 
        //                Console.WriteLine("True | False Question");
        //                Console.WriteLine("Please Enter The Body Of The Question:");
        //                string body = Console.ReadLine();

        //                Console.WriteLine("Please Enter The Marks Of Question:");
        //                int marks = int.Parse(Console.ReadLine());

        //                Console.WriteLine("Please Enter The Right Answer of Question (1 for True and 2 for False): ");
        //                int rightAnswer = int.Parse(Console.ReadLine());

        //                var question1 = new TFQuestion()
        //                {
        //                    HeaderOfTheQuestion = "True | False Question",
        //                    BodyOfTheQuestion = body,
        //                    Marks = marks,
        //                    Answer = rightAnswer == 1 ,
        //                    AnswerList = new List<Answers>
        //                    {
        //                        new Answers {AnswerId = rightAnswer , AnswerText = rightAnswer == 1 ? "True" : "False" }
        //                    }
                            
     
        //                };

        //                ExamOfSubject.AddQuestion(question1);
        //                Console.Clear();
        //            }
        //            else
        //            {
        //                Console.WriteLine("Choose One Answer Question");
        //                Console.WriteLine("Please Enter The Body Of The Question:");
        //                string body = Console.ReadLine();

        //                Console.WriteLine("Please Enter The Marks Of Question:");
        //                int marks = int.Parse(Console.ReadLine());

        //                string[] options = new string[3];

        //                Console.WriteLine("The Choices Of The Questions :");
        //                for (int j = 0; j < options.Length; j++)
        //                {
        //                    Console.Write($"Please Enter The Choice Number {j + 1} : ");
        //                    options[j] = Console.ReadLine();
        //                }

        //                Console.WriteLine("Please Enter The Right Answer of Question: ");
        //                int correctIndex =int.Parse( Console.ReadLine());

        //                var question2 = new MCQOneChoice()
        //                {
        //                    HeaderOfTheQuestion = "Choose One Answer Question",
        //                    BodyOfTheQuestion = body,
        //                    Marks = marks,
        //                    Options = options,
        //                    correctOptionIndex = correctIndex,
        //                    AnswerList = new List<Answers>
        //                {
        //                    new Answers { AnswerId = correctIndex, AnswerText = options[ correctIndex -1 ] },
        //                }


        //                };

        //                ExamOfSubject.AddQuestion(question2);
        //                Console.Clear();
        //            }
        //        }
                

        //    }

            

       

            

        //}
    }
}

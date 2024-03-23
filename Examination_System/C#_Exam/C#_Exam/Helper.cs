using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace C__Exam
{
    public class Helper
    {
        // Create Exam
        public static Exam CreateExam(Exam  ExamOfSubject , Subject subject)
        {
            int examType;
            int timeOfExam;
            int quetionsNumber;
            Console.Write("Please Enter The Type Of Exam You Want To Create ( 1 for Practical and 2 for Final):");
            examType = int.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter The Time Of Exam in Minutes:");
            timeOfExam = int.Parse(Console.ReadLine());

            Console.WriteLine("Please Enter The NUmber Of Questions You Wanted To Create:");
            quetionsNumber = int.Parse(Console.ReadLine());

            Console.Clear();

            if (examType == 1)
            {
                ExamOfSubject = new PracticalExam(timeOfExam, quetionsNumber, subject);
                for (int i = 0; i < quetionsNumber; i++)
                {

                    Console.WriteLine("Choose One Answer Question");
                    Console.WriteLine("Please Enter The Body Of The Question:");
                    string body = Console.ReadLine();

                    Console.WriteLine("Please Enter The Marks Of Question:");
                    int marks = int.Parse(Console.ReadLine());

                    string[] options = new string[3];

                    Console.WriteLine("The Choices Of The Questions :");
                    for (int j = 0; j < options.Length; j++)
                    {
                        Console.Write($"Please Enter The Choice Number {j + 1} : ");
                        options[j] = Console.ReadLine();
                    }

                    Console.WriteLine("Please Enter The Right Answer of Question: ");
                    int correctIndex = int.Parse(Console.ReadLine());

                    var question = new MCQOneChoice()
                    {
                        HeaderOfTheQuestion = "Choose One Answer Question",
                        BodyOfTheQuestion = body,
                        Marks = marks,
                        Options = options,
                        correctOptionIndex = correctIndex,
                        AnswerList = new List<Answers>
                        {
                            new Answers { AnswerId = correctIndex, AnswerText = options[correctIndex-1] },
                        }


                    };

                    ExamOfSubject.AddQuestion(question);
                    Console.Clear();
                }


            }
            else if (examType == 2)
            {
                ExamOfSubject = new FinalExam(timeOfExam, quetionsNumber, subject);

                for (int i = 0; i < quetionsNumber; i++)
                {
                    int TForMCQ;
                    Console.WriteLine($"Please Choose Type Of Question Number({i + 1}) (1 for True or False and 2 for MCQ):");
                    TForMCQ = int.Parse(Console.ReadLine());
                    if (TForMCQ == 1)
                    {

                        Console.WriteLine("True | False Question");
                        Console.WriteLine("Please Enter The Body Of The Question:");
                        string body = Console.ReadLine();

                        Console.WriteLine("Please Enter The Marks Of Question:");
                        int marks = int.Parse(Console.ReadLine());

                        Console.WriteLine("Please Enter The Right Answer of Question (1 for True and 2 for False): ");
                        int rightAnswer = int.Parse(Console.ReadLine());

                        var question1 = new TFQuestion()
                        {
                            HeaderOfTheQuestion = "True | False Question",
                            BodyOfTheQuestion = body,
                            Marks = marks,
                            Answer = rightAnswer == 1,
                            AnswerList = new List<Answers>
                            {
                                new Answers {AnswerId = rightAnswer , AnswerText = rightAnswer == 1 ? "True" : "False" }
                            }


                        };

                        ExamOfSubject.AddQuestion(question1);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Choose One Answer Question");
                        Console.WriteLine("Please Enter The Body Of The Question:");
                        string body = Console.ReadLine();

                        Console.WriteLine("Please Enter The Marks Of Question:");
                        int marks = int.Parse(Console.ReadLine());

                        string[] options = new string[3];

                        Console.WriteLine("The Choices Of The Questions :");
                        for (int j = 0; j < options.Length; j++)
                        {
                            Console.Write($"Please Enter The Choice Number {j + 1} : ");
                            options[j] = Console.ReadLine();
                        }

                        Console.WriteLine("Please Enter The Right Answer of Question: ");
                        int correctIndex = int.Parse(Console.ReadLine());

                        var question2 = new MCQOneChoice()
                        {
                            HeaderOfTheQuestion = "Choose One Answer Question",
                            BodyOfTheQuestion = body,
                            Marks = marks,
                            Options = options,
                            correctOptionIndex = correctIndex,
                            AnswerList = new List<Answers>
                        {
                            new Answers { AnswerId = correctIndex, AnswerText = options[ correctIndex -1 ] },
                        }


                        };

                        ExamOfSubject.AddQuestion(question2);
                        Console.Clear();
                    }
                }


            }
            return ExamOfSubject;


        }

        // Create Multiple Choices Question......
        public static Question CreateMCQQuestion(Question question)
        {
            
                Console.WriteLine($"{question.HeaderOfTheQuestion}             Mark({question.Marks})");
                Console.WriteLine($"{question.BodyOfTheQuestion}");


                var mcQuestion = (MCQOneChoice)question;
                Console.WriteLine("Choose One Answer Question Choi:");
                int i = 1;
                foreach (var option in mcQuestion.Options)
                {

                    Console.Write($"{i}. {option}    ");
                    i++;
                }
                Console.WriteLine("\n");
                Answers answer = new Answers();
                answer.AnswerId = int.Parse(Console.ReadLine());
                answer.AnswerText = mcQuestion.Options[answer.AnswerId - 1];
                question.AnswerList.Add(answer);


                Console.WriteLine();
            
            return question;

        }



        // Create TrueOrFales Question...........
        public static Question CreateTFQuestion(Question question)
        {
            Console.WriteLine($"{question.HeaderOfTheQuestion}             Mark({question.Marks})");
            Console.WriteLine($"{question.BodyOfTheQuestion}");
            Console.WriteLine($"1. True               2. False \n ");
            Answers answer = new Answers();
            answer.AnswerId = int.Parse(Console.ReadLine());
            answer.AnswerText = answer.AnswerId == 1 ? "True" : "False";

            question.AnswerList.Add(answer);

            Console.WriteLine();

            return question;
        }


        // Create Result Exam..........
        public static void ShowResultExam(List<Question> questions)
        {
            int questionNum = 1;
            decimal totalGrade = 0;
            decimal totalMarks = 0;

            Console.WriteLine("Your Answers:");
            foreach (var question in questions)
            {

                Console.WriteLine($"Q{questionNum})   {question.BodyOfTheQuestion} {question.AnswerList[0].AnswerText}");
                questionNum++;

                if (question.AnswerList[0].AnswerText == question.AnswerList[1].AnswerText || question.AnswerList[0].AnswerId == question.AnswerList[1].AnswerId)
                {
                    totalGrade += question.Marks;
                }
                totalMarks += question.Marks;
            }
            Console.WriteLine($"Your Exam Grade is {totalGrade} from {totalMarks} ");

        }


 
    }
}

using System.Diagnostics;

namespace C__Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject();
            subject.CreateExam();
            Console.Clear();
            Console.WriteLine("Do you Want To Start The Exam (y | n):");

            if(char.Parse(Console.ReadLine()) == 'y')
            {
                Stopwatch sw = Stopwatch.StartNew();
                sw.Start();
                subject.ExamOfSubject.ShowExam();
                Console.WriteLine($"The Elapsed Time = {sw.Elapsed}");

            }
        }
    }
}
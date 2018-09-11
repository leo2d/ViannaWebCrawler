using System;
using System.IO;
using ViannaWebCrawler.Controls.GradebookColtrol;
using ViannaWebCrawler.Controls.LoginControl;
using ViannaWebCrawler.Controls.TimeTableControl;
using ViannaWebCrawler.Entities;

namespace ViannaWebCrawler
{
    class ProGram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite a matricula:");
            var mat = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a senha:");

            LoginRequest loginRequest = new LoginRequest
            (
                new LoginData()
                {
                    Id = mat,
                    Password = GetPassw()
                }
            );

            Console.Clear();

            Console.WriteLine("\n\t #### Aguarde enquanto os dados sao processados...");

            var responseMessage = loginRequest.Login();

            var table = TimeTableManager.GetTimeTable(responseMessage.Content.ReadAsStringAsync().Result);

            Console.Clear();

            PrintTimeTable(table);

            GradebookRequest requestPage = new GradebookRequest(loginRequest.Client);

            var html = requestPage.GradebookPageRequest();

            var gradebook = GradebookManager.GetGradebookResume(html);

            Console.Clear();

            PrintGradeBook(gradebook);

            Console.ReadKey();
        }

        //This password mask has created by Morgan: https://www.morgantechspace.com/2014/11/Read-Password-from-C-Sharp-Console-Application.html
        public static string GetPassw()
        {
            string password = null;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                // Skip if Backspace or Enter is Pressed
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        // Remove last charcter if Backspace is Pressed
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Getting Password Once Enter is Pressed
            while (keyInfo.Key != ConsoleKey.Enter);

            return password;
        }
        private static void PrintGradeBook(Gradebook gradebook)
        {
            Console.WriteLine("\r\t ** Boletim **\n\n");

            foreach (var disclipline in gradebook.GradebookResume)
            {
                Console.WriteLine($"Disciplina: {disclipline.Name}");
                Console.WriteLine($"Nota 1o B: {disclipline.FirstBimesterGrade}");
                Console.WriteLine($"Nota 2o B: {disclipline.SecondBimesterGrade}");
                Console.WriteLine($"Media das notas: {disclipline.Media}");
                Console.WriteLine($"Nota prova exame: {disclipline.RetakeTestGrade}");
                Console.WriteLine($"Media Final: {disclipline.FinalMedia}");
                Console.WriteLine($"No de faltas ate agora: {disclipline.MissedClasses}");
                Console.WriteLine($"Percentual de faltas ate agora: {disclipline.MissedClassPercentage}");
                Console.WriteLine("============================================================\n\n");
            }
        }
        private static void PrintTimeTable(TimeTable table)
        {
            foreach (var classDay in table.ClassSchedule)
            {
                Console.WriteLine(classDay.DayOfWeek);
                Console.WriteLine("-------");
                Console.WriteLine(classDay.FirstClass.NickName);
                Console.WriteLine(classDay.FirstClass.Name);
                Console.WriteLine("--");
                Console.WriteLine(classDay.SecondClass.NickName);
                Console.WriteLine(classDay.SecondClass.Name);
                Console.WriteLine("--");
                Console.WriteLine(classDay.ThirdClass.NickName);
                Console.WriteLine(classDay.ThirdClass.Name);
                Console.WriteLine("--");
                Console.WriteLine(classDay.FourthClass.NickName);
                Console.WriteLine(classDay.FourthClass.Name);
                Console.WriteLine("\n \n");
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using NDesk.Options;
using System.Diagnostics;

namespace MPG_CLI
{
    class Program
    {

        

        static int verbosity;

        static void Main(string[] args)
        {
            int show_help = 0;

            
            int minimumNumber = 0;
            int maximumNumber = 0;
            int iteration = 0;
            int decimalNumber = 0;
            int numberOfQuestion = 0;

            string test131234r = "";

            var p = new OptionSet() {
                { "min|minimum=", "the {MINIMUM NUMBER} to generate.",
                  //v => int.TryParse(v, out minimumNumber) },
                  (int v) => minimumNumber = v},
                { "max|maximum=", "the {MAXIMUM NUMBER} to generate.",
                  (int v) => maximumNumber = v },
                { "i|iteration=", "the number of {ITERATION} to generate.",
                  (int v) => iteration = v },
                { "dec|decimals=", "the number of {DECIMALS} for the results. /n Leave this if you want the result to have no decimal.",
                  (int v) => decimalNumber = v },
                { "q|question=", "the number of {question} to generate.",
                  (int v) => iteration = v },
                { "v", "increase debug message verbosity",
                  v => { if (v != null) ++verbosity; } },
                { "h|help",  "show this message and exit", 
                  //v => show_help = v != null},
                  v => show_help++ },
                  
            };

            Debug.WriteLine(show_help);

            if (show_help > 0)
            {
                ShowHelp(p);
                return;
            }

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("MPG: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try using `--help' for more information.");
                return;
            }
            if (minimumNumber == 0)
            {
                Console.WriteLine("Please insert your minimum number to generate");
                int.TryParse(Console.ReadLine(), out minimumNumber);
            }
            if (maximumNumber == 0)
            {
                Console.WriteLine("Please insert your maximum number to generate");
                int.TryParse(Console.ReadLine(), out maximumNumber);
            }
            if (iteration == 0)
            {
                Console.WriteLine("Please insert the iteration value");
                int.TryParse(Console.ReadLine(), out iteration);
            }
            if (decimalNumber == 0)
            {
                Console.WriteLine("Please insert the amount of decimal. (Type 0 if you want the result non-decimal).");
                int.TryParse(Console.ReadLine(), out decimalNumber);
            }
            if (numberOfQuestion == 0)
            {
                Console.WriteLine("Please insert the amount of question do you want. The generated question will be saved to {0}", (AppDomain.CurrentDomain.BaseDirectory + @"GeneratedQuestions.txt"));
                int.TryParse(Console.ReadLine(), out numberOfQuestion);
            }

            if (reverseCheck(minimumNumber, maximumNumber))
            {
                if (batchGen(numberOfQuestion))
                {
                    StringBuilder sb = new StringBuilder(); // Ngeitialize StringBuilder buat naruh string Question ama Result

                    //for (int i = 0; i <= numberOfQuestion; i++)
                    Parallel.For(1, numberOfQuestion, i =>
                    {
                        sb.Append(Generator.QuestionGen(minimumNumber, maximumNumber, iteration, decimalNumber) + Environment.NewLine);
                    });

                    Console.WriteLine(sb.ToString());
                    saveFile(AppDomain.CurrentDomain.BaseDirectory + @"GeneratedQuestions.txt", sb.ToString());
                }
                else
                {
                    Console.WriteLine(Generator.QuestionGen(minimumNumber, maximumNumber, iteration, decimalNumber));
                }


                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("The minimum value should not exceed the maximum value. Please recheck your input.");
            }
        }
        public static bool batchGen(int batchGenerate)
        {
            if (batchGenerate > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static void saveFile(string destination, string strings)
        {
            try
            {
                File.WriteAllText(destination, strings + Environment.NewLine);
            }

            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Cannot write file to this location. It probably caused by the location that may not exist or protected. Please pick another location.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Cannot write file to this location. It probably caused by the location that may not exist or protected. Please pick another location.");
            }

        }

        public static bool reverseCheck(int input1, int input2) //method untuk mengecek jika angka input1 dan input2 terbalik atau tidak.
        {
            return input1 - input2 < 0;
        }

        static void ShowHelp(OptionSet p)
        {
            Debug.WriteLine("SHOWED HELP");
            Console.WriteLine("Usage: MPG.exe [OPTIONS]");
            Console.WriteLine("Generate question(s) and the result. Simply that.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}

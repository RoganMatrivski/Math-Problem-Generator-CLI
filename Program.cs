using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPG_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TestClass.QuestionGen(1, 100, 10, 0));
            Console.ReadKey();
        }
    }
}

using System;
using System.Diagnostics;
using org.mariuszgromada.math.mxparser;
using System.Text;

public class TestClass
{
    public TestClass()
    {

    }

    private static void logAdd(string input)
    {
        StringBuilder sb = new StringBuilder(); // Ngeitialize StringBuilder buat naruh string Question ama Result
        sb.Append("=====================================" + Environment.NewLine + Environment.NewLine);
        sb.Append(input + Environment.NewLine + Environment.NewLine);
        Console.WriteLine(sb.ToString());
    }

    

    

    Random num1Gen = new Random();
    Random num2Gen = new Random();
    Random randOperator = new Random();

    //TestClass questionGenerator = new questionGenerator();
    public static string QuestionGen(int firstNum1, int firstNum2, int iterationValue, int decimalNumbers)
    {
        int retryCount = 0; //Ngitung berapa kali ngeGenerate ulang
        bool decimals = true;
        var stopWatch = System.Diagnostics.Stopwatch.StartNew(); //Ngeinitialize sekaligus mulai ngitung waktu sampai fungsi Stopwatch dihentikan.
        while (decimals == true) //Ngecek jika Decimal bernilai True atau engga. Jika benar, jalankan kode ini secara berulang-ulang sampai Decimal bernilai selain True.
        {
            Random questionTypeGen = new Random();
            double result;
            string questionRandResult = questionGenerator(Convert.ToInt32(iterationValue) - 1, firstNum1, firstNum2); //Manggil Method questionGenerator sambil masukin iterationValue ama range angka random.
            string questionString = questionRandResult.Replace("*", " X "); //Ngeganti string yang bisa dibaca komputer ke tulisan yang bisa dibaca manusia.
            questionString = questionString.Replace("+", " + "); //Ngeganti string yang bisa dibaca komputer ke tulisan yang bisa dibaca manusia.
            questionString = questionString.Replace("-", " - "); //Ngeganti string yang bisa dibaca komputer ke tulisan yang bisa dibaca manusia.
            questionString = questionString.Replace("/", " : "); //Ngeganti string yang bisa dibaca komputer ke tulisan yang bisa dibaca manusia.
            Expression ex = new Expression(questionRandResult);
            result = ex.calculate(); //Ngekalkulasi questionRandResult dengan MathFunction.MathParser.Calculate.
            Console.WriteLine("The number {0} is the result.", result);
            Console.WriteLine("The number {0} is the result and processed.", Math.Abs(result % 1));
            Console.WriteLine("This number {0} is the double epsilon number.", (double.Epsilon));
            Console.WriteLine("This => {0} should have turned on.", (Math.Floor(result)==result));


            if (Math.Floor(result)==result) //Ngecek jika result merupakan angka dengan desimal
            //if (!decimalChecker(result))
            {
                stopWatch.Stop(); //hentikan menghitung waktu kalkulasi
                var elapsedTime = stopWatch.ElapsedMilliseconds; //memasukan waktu kalkulasi ke variabel.
                Console.WriteLine("the number " + result.ToString() + " is not decimal");
                decimals = false;
                logAdd("Succeeded generating a random problem in " + elapsedTime + " millisecond, with each value between " + firstNum1 + " to " + firstNum2 + ", with an iteration number of " + iterationValue + ", regenerated " + retryCount + " times in attempt to search for the non-decimal result.");
                //TODO : Ganti buat return ke function.

                //questionProblem = questionString;
                //resultNum = resultNew.ToString();


                return questionString + " = " + result.ToString() + " This one is without decimal number";
            }
            else
            {
                //if (noDecimalCheckbox.Checked == false) //TODO : Ganti buat ngecek kalo decimalNumbers ada ato engga.
                if (decimalNumbers >= 1)
                {
                    stopWatch.Stop(); //hentikan menghitung waktu kalkulasi
                    var elapsedTime = stopWatch.ElapsedMilliseconds; //memasukan waktu kalkulasi ke variabel.
                    logAdd("Succeeded generating a random problem in " + elapsedTime + " millisecond, with each value between " + firstNum1 + " to " + firstNum2 + ", with an iteration number of " + iterationValue + ", regenerated " + retryCount + " times in attempt to search for the non-decimal result.");
                    Console.WriteLine("the number " + result.ToString() + " is decimal");
                    decimals = false;
                    double resultNew = Math.Round(result, Convert.ToInt32(decimalNumbers), MidpointRounding.AwayFromZero); //Jika noDecimalCheckbox tidak dicentang, maka hasil penghitungan tidak dicek dan langsung ditampilkan.

                    //TODO : Ganti buat return ke function.

                    //questionProblem = questionString;
                    //resultNum = resultNew.ToString();

                    return questionString + " = " + resultNew.ToString() + " This one is with decimal number";

                }
                else
                {
                    Console.WriteLine("the number " + result.ToString() + " is decimal");
                    decimals = true; //mengulang kalkulasi sambil menambahkan retryCount dengan 1
                    retryCount++;

                }
                return null;
            }
        }
        return null;
    }

    public bool reverseCheck(int input1, int input2) //method untuk mengecek jika angka input1 dan input2 terbalik atau tidak.
    {
        return input1 - input2 < 0;
    }

    
    public static string questionGenerator(int numIteration, int num1, int num2) //fungsi untuk mengenerate pertanyaan.
    {
        Random numGen = new Random();
        string problem = "";
        problem = numGen.Next(num1, num2).ToString();

        for (int i = 0; i < numIteration; i++)
        {
            problem = problem + operatorPick(numGen.Next(0, 100)) + numGen.Next(num1, num2).ToString();
        }
        return problem;
    }

    public static string operatorPick(int input1) //memberikan string operator sesuai dengan input1.
    {
        string operatorSymbol = "";
        if (input1 >= 0 && input1 <= 25)
        {
            operatorSymbol = "+";
        }

        if (input1 > 25 && input1 <= 50)
        {
            operatorSymbol = "-";
        }

        if (input1 > 50 && input1 <= 75)
        {
            operatorSymbol = "/";
        }

        if (input1 > 75 && input1 <= 100)
        {
            operatorSymbol = "*";
        }
        return operatorSymbol;
    }

    public static bool decimalChecker(double Input)
    {
        return (Input % 1) == 0;
    }
}

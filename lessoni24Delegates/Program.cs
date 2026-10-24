using System;

namespace lessoni24Delegates
{
    class Program
    {
        delegate int SumOfTwoNumbers(int x, int y); // delegate
        static void Main()
        {
            SumOfTwoNumbers sumOfTwoNumbers; // create variable delegate
            sumOfTwoNumbers = Sum;           // assign to this variable reference to function 

            static int Sum(int aa, int bb) => aa + bb;
            Func<int, Func<int,int>> add(Func<int,int,int> function) => a => b => function(a,b);
            var add10 = add(Sum)(10);
            var result = add10(5);
            System.Console.WriteLine(result);


            int result1 = sumOfTwoNumbers(1,2);
            System.Console.WriteLine(result1);
        }
    }
}
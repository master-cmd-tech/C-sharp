using System;
using System.Runtime.InteropServices;

namespace lessoni11
{
    class Program
    {
        static void Main()
        {
            // try {
            // int num = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine(num);
            // } catch(FormatException) {
            //     System.Console.WriteLine("You entered the wrong format!");
            // }

            try{
               int a, b , res;
               Console.Write("Input first number: ");
               a = Convert.ToInt32(Console.ReadLine());
               Console.Write("Input second number: ");
               b = Convert.ToInt32(Console.ReadLine());
               res = a/b;
               Console.WriteLine("Result: " + res);
            } catch(DivideByZeroException)
            {
                System.Console.WriteLine("Division by zero!");
            } catch(FormatException)
            {
                System.Console.WriteLine("You entered the wrong format!");
            } finally
            {
                System.Console.WriteLine("Finally!");
            }
 
        }
    }
}
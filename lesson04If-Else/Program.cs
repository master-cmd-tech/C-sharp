using System;

namespace lesson4 {

    class Program {
        static void Main()
        {
            int a = 5;

            if(a > 7)
            {
                System.Console.WriteLine("Number > 7");
            } else if(a < 5)
            {
                System.Console.WriteLine("Number < 5");
            } else
            {
                System.Console.WriteLine("Number is unrecognized!");
            }
        }
    }
    
}
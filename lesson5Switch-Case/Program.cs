using System;

namespace lesson5
{
    class Program
    {
        static void Main()
        {
            short  user_input = Convert.ToInt16(System.Console.ReadLine());

            switch(user_input)
            {
                case 1: System.Console.WriteLine("1"); break;
                case 5: System.Console.WriteLine("5"); break; 
                default: System.Console.WriteLine("default"); break;
            }
        }
    }
}
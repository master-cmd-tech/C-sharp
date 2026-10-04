using System;

namespace lesson3
{
    class Programm
    {
        static void Main()
        {
            // user_input = Convert.ToDouble(Console.ReadLine());
            // float user_input = float.Parse(Console.ReadLine());

            // float result;
            // result = user_input + 10f;
            // result = user_input - 10f;
            // result = user_input / 10f;
            // result = user_input % 10f;
            // result *= 2f;
            // result--;
            // Console.WriteLine("result: " + result);
            // System.Console.WriteLine(Math.PI);
            // Console.WriteLine(Math.Abs(-20));
            // Console.WriteLine(Math.Ceiling(4.11f));
            // Console.WriteLine(Math.Floor(4.99f));
            // Console.WriteLine(Math.Round(4.51000000f));
            // Console.WriteLine(Math.Min(5, 0));
            // Console.WriteLine(Math.Max(5, 0));
            // Console.WriteLine(Math.Pow(5, 2));
            
            System.Console.WriteLine("Input the radius of circle:" );
            short radius = Convert.ToInt16(Console.ReadLine());
            double result = Math.PI * Math.Pow(radius, 2);
            System.Console.WriteLine("Area of circle with radius {0} equal to {1}", radius, result);
            

        }
    }
}

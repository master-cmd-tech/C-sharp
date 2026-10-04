using System;
using System.Collections.Generic;

namespace lesson8
{
    class Program
    {
        static void Main()
        {
            // short[,] nums =
            // {
            //     {15, 26, 47},
            //     {51, 62, 74},
            //     {65, 76, 57}
            // };

            // foreach(short el in nums)
            // {
            //     System.Console.WriteLine("El: " + el);
            // }

            List<int> numbers = new List<int> ()
            {
                4,6,7
            };
            numbers.Add(40);
            numbers.Add(100);
            numbers.Add(4);

            numbers.Remove(100);
            numbers.Sort();
            numbers.Reverse();
            

            foreach(int el in numbers)
                System.Console.WriteLine("El: " + el);

        }
    }
}

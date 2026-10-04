using System;
using System.Globalization;

namespace lesson7 {
    class Program{
    static void Main()
        {
            // byte[] nums = new byte[5];
            // nums[0] = 250;
            // nums[1] = 50;
            // nums[2] = 20;
            // nums[3] = 100;
            // nums[4] = 25;

            // string[] words = new string[] {"John", "Bob", "Alex"};

            // words[1] = "Josh";

            // for(byte i = 0; i < nums.Length; i++) 
            //     System.Console.WriteLine("El: " + nums[i]);

            // short[] numbers = new short[10];
            // short sum = 0;


            // Random random = new Random();
            // for(byte i = 0; i < numbers.Length; i++) {
            //     numbers[i] = Convert.ToInt16(random.Next(-15, 15));
            //     System.Console.WriteLine("El: " + numbers[i]);
            //     sum += numbers[i];
            // }
            // System.Console.WriteLine(sum);

            char[,] symbols = new char[2,3];
            symbols[0,0] = 'H';
            System.Console.WriteLine(symbols[0,0]);

            int[,] nums =
            {
                {4,6,7},
                {5,3,2},
                {3,2,1}
            };
            nums[1,2] = 9;

        }
    }
}
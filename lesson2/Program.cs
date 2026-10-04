using System;

namespace lesson2
{
    class Program
{
    static void  Main()
        {
            // int number = 10;
            // number = -10;
            // number = 5;
            // Console.WriteLine("number: " + number);
            // uint number1 = 10;
            // byte number2 = 255;
            // short number3 = 32767;
            // long number4 = 10000000000;
            // float number5 = 10.5f; 
            // double number6 = 10.5d;
            // string en = "number: ";
            // Console.WriteLine(en + number);
            // char symbol = 'S';
            // bool isTrue = true;
            int num_1 = 0, num_2 = 0;
            num_1 = Convert.ToInt32(Console.ReadLine());
            num_2 = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine(num_1 + num_2);
        }
}
}
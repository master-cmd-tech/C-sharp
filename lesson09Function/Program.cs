using System;

namespace lesson9
{
    class Program
    {
        static void Main()
        {
            // Print("Hello");
            // string words = "Hello World";
            // Print(words);

            int res1 = Summa(5,6);
            int a = 7, b = 8;
            int res2 = Summa(a,b);

            Print(res1.ToString());
            Print(res2.ToString());
        }

        public static void Print(string word)
        {
            System.Console.WriteLine(word);
        }

        // public static void Summa(int x,int y) {
        //     int res = x + y;
        //     Print("Result: " + res); }

        public static int Summa(int x,int y){
            return x + y; }

    }
}
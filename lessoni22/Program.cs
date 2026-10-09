using System;
using System.Xml.Serialization;

namespace lessoni22
{
    class Program
    {
        static void Main()
        {
            // Bot bot = new Bot("Bot", 800,new byte[] {0,0,0});
            // bot.printValues();

            Killer killer = new Killer("Killer", 1000 , new byte[] {10,10,10}, 100, Type.Hero);
            killer.printValues();
            killer.Lazer();

            // Bot bot1 = new Bot();
            //bot1.setValues();
            // bot1.Weight = -100;


            // Multiply(5.3f ,6.3f);

        }

        public static void Multiply(int a, int b)
        {
            int res = a*b;
            System.Console.WriteLine("Result: "+ res);
        }

        public static void Multiply(float a, float b)
        {
            float res = a*b;
            System.Console.WriteLine("Result: "+ res);
        }
    }
}
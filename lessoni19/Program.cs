using System;

namespace lessoni19
{
    class Program
    {
        static void Main()
        {
            Bot bot = new Bot("Bot", 800,new byte[] {0,0,0});
            bot.printValues();

            Killer killer = new Killer("Killer", 1000 , new byte[] {10,10,10}, 100);
            killer.printValues();
            killer.Lazer();

            Bot bot1 = new Bot();
            bot1.Weight = -100;
            
        }
    }
}
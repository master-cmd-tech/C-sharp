using System;
using System.Collections.Generic;

namespace lessoni18
{
    class Program
    {
        static void Main()
        {
            Robot bot = new Robot("Bot", 800,new byte[] {0,0,0});
            bot.printValues();

            Killer killer = new Killer("Killer", 1000 , new byte[] {10,10,10}, 100);
            killer.printValues();
            killer.Lazer();

            Robot bot1 = new Robot("Bot1");
            bot1.Weight = -100;
            
        }
    }
}
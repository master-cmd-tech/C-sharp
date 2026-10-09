using System;

namespace lessoni13
{
    class Program
    {
        static void Main()
        {
            Robot bot = new Robot("Bot", 800,new byte[] {0,0,0});
            bot.printValues();

            Robot killer = new Robot();
            bot.setValues("Killer", 1000 , new byte[] {10,10,10} );
            bot.printValues();

            Robot bot1 = new Robot("Bot1");
            
            Robot.Print();
        }
    }
}
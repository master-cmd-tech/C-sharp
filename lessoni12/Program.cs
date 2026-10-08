using System;

namespace lessoni12
{
    class Program
    {
        static void Main()
        {
            Robot bot = new Robot();
            bot.setValues("Bot", 800,new byte[] {0,0,0} );
            

            bot.printValues();
        }
    }
}
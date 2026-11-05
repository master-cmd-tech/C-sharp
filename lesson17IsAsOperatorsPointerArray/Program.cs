using System;

namespace lessoni17
{
    class Program
    {
        static void Main()
        {
            // Robot bot = new Robot("Bot", 800,new byte[] {0,0,0});
            // bot.printValues();

            // Killer killer = new Killer("Killer", 1000 , new byte[] {10,10,10}, 100);
            // killer.printValues();
            // killer.Lazer();

            // Robot bot1 = new Robot("Bot1");
            // bot1.Weight = -100;
            // System.Console.WriteLine(bot1.Weight);
            
            // Robot.Print();

            List<Killer> robots = new List<Killer>();
            robots.Add(new Killer("Alex", 400, new byte[] {0,0,10}, 100));
            robots.Add(new Killer("Bob", 600, new byte[] {0,10,10}, 100));
            robots.Add(new Killer("John", 800, new byte[] {10,0,10}, 100));
            robots.Add(new Killer("Josh", 600, new byte[] {0,50,10}, 100));

            Robot newRobot = null;

            foreach(Robot obj in robots)
            {
                if(obj.Name == "John")
                {
                    newRobot = obj as Robot;
                }

                Console.WriteLine(obj is Killer);
            }

        }
    }
}
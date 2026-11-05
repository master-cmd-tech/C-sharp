using System;

namespace lessoni18
{
    class Killer : Robot
    {
        public int Health {get; set;}

        public Killer() {}

        public Killer(string name, int weight, byte[] coordinates, int health) : base(name,weight,coordinates)
        {
            this.Health = health;
        }

        public override void printValues() {
            base.printValues();

            Console.WriteLine("Health: " + this.Health);
        }

        public void Lazer()
        {
            System.Console.WriteLine("Lazer shooting");
            this.surname = "Doe";
        }

    }
}
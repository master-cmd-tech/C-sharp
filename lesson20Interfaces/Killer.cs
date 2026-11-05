using System;

namespace lessoni20
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
            System.Console.WriteLine(this.Name + " weight: " + this.Weight + ".");
            Console.WriteLine("Health: " + this.Health);
            // foreach(byte el in this.Coordinates)
            //     Console.WriteLine(el);
        }

        public void Lazer()
        {
            System.Console.WriteLine("Lazer shooting");
            this.surname = "Doe";
        }

    }
}
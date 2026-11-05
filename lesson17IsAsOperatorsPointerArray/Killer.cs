using System;
using System.Collections.Generic;

namespace lessoni17
{
    class Killer : Robot
    {
        public int Health {get; set;}

        public Killer() {}

        public Killer(string name, int weight, byte[] coordinates, int health) : base(name,weight,coordinates)
        {
            this.Health = health;
        }


        public void Lazer()
        {
            System.Console.WriteLine("Lazer shooting");
            this.surname = "Doe";
        }

    }
}
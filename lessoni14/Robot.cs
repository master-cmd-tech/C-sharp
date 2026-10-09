using System;

namespace lessoni14
{
    class Robot
    {

        private static int count;

        private string name;
        private int weight;
        private byte[] coordinates;

        public Robot(string name, int weight, byte[] coordinates)
        {
          System.Console.WriteLine("Object has been created");
          this.setValues(name, weight, coordinates);
          count++;
        }

        public Robot(string name)
        {
            System.Console.WriteLine("Object has been created");
            this.name = name;
            count++;
        }

        public Robot()
        {
            count++;
        }

        public void setValues(string name, int weight, byte[] coordinates)
        {
            this.name = name;
            this.weight = weight;
            this.coordinates = coordinates;
        }

        public void printValues()
        {
            System.Console.WriteLine(this.name + " weight: " + this.weight + ".Coordinates: ");
            foreach(byte el in this.coordinates)
                Console.WriteLine(el);
        }
        
        public static void Print()
        {
            System.Console.WriteLine("Count: " + count);
            
        }

    }
}
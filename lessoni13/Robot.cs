using System;

namespace lessoni13
{
    class Robot
    {

        private static int count;

        private string name;
        private int weight;
        private byte[] coordinates;

        public Robot(string _name, int _weight, byte[] _coordinates)
        {
          System.Console.WriteLine("Object has been created");
          setValues(_name, _weight, _coordinates);
          count++;
        }

        public Robot(string _name)
        {
            System.Console.WriteLine("Object has been created");
            name = _name;
            count++;
        }

        public Robot()
        {
            count++;
        }

        public void setValues(string _name, int _weight, byte[] _coordinates)
        {
            name = _name;
            weight = _weight;
            coordinates = _coordinates;
        }

        public void printValues()
        {
            System.Console.WriteLine(name + " weight: " + weight + ".Coordinates: ");
            foreach(byte el in coordinates)
                Console.WriteLine(el);
        }
        
        public static void Print()
        {
            System.Console.WriteLine("Count: " + count);
            
        }

    }
}
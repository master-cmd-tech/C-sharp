using System;

namespace lessoni12
{
    class Robot
    {
        private string name;
        private int weight;
        private byte[] coordinates;

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
        
    }
}
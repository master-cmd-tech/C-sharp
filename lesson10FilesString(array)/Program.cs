using System;
using System.IO;
using Microsoft.VisualBasic;

namespace lessoni10
{
    class Program
    {
        static void Main()
        {
            // string word = "Hello";
            // word += "!";

            // Console.WriteLine(word.Length);
            // word = String.Concat(word, "!!");
            // Console.WriteLine(String.Compare(word, "Hello"));

            // string people = "Alex,Bob,John";
            // string[] names = people.Split(',');
            // people = String.Join(" ", names);
            // System.Console.WriteLine(people);

            // foreach(string el in names)
            //     System.Console.WriteLine(el);

            // System.Console.WriteLine(word.Trim());
            // System.Console.WriteLine(word.Substring(0, word.Length - 1));

            // System.Console.WriteLine("Input the text: ");
            // string text = Console.ReadLine();
            // using(FileStream stream = new FileStream("info.txt", FileMode.OpenOrCreate))
            // {
            //     byte[] array = System.Text.Encoding.Default.GetBytes(text);

            //     stream.Write(array, 0, array.Length);
            // }

            using(FileStream stream1 = File.OpenRead("info.txt"))
            {
                byte[] array = new byte[stream1.Length];
                stream1.Read(array,0,array.Length);

                string textFromFile = System.Text.Encoding.Default.GetString(array);
                System.Console.WriteLine(textFromFile);
            }

        }
    }
}
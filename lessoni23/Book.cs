using System;

namespace lessoni23
{
    struct Book
    {
        private string title, avtor, intro;

        private short page;

        public void setValues(string title, string avtor)
        {
            this.title = title;
            this.avtor = avtor;
        }
        public void printValues()
        {
            System.Console.WriteLine(this.avtor + " write the book: " + this.title );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
public record Book
{
    public string Title { get; init; }
    public string Isbn { get; init; }
    public decimal Price { get; init; }
    public int StockCount { get; private init; }

    public override bool Equals(object? obj)
    {
        if (obj is Book other)
        {
            return Isbn == other.Isbn;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Isbn.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        List<Book> catalog = new()
        {
            new Book
            {
                Title = "Refactoring",
                Isbn = "111",
                Price = 45.00m,
                StockCount = 4
            },

            new Book
            {
                Title = "Clean Code",
                Isbn = "222",
                Price = 35.50m,
                StockCount = 2
            },

            new Book
            {
                Title = "The Pragmatic Programmer",
                Isbn = "333",
                Price = 40.00m,
                StockCount = 6
            }
        };

        Book featured = catalog[0];
        Book display = featured;

        Console.WriteLine("Before change:");
        Console.WriteLine($"Featured stock: {featured.StockCount}");


        Console.WriteLine("\nShared reference demonstration:");
        Console.WriteLine($"featured and display refer to the same object: {ReferenceEquals(featured, display)}");

        Book sameIsbn = new Book
        {
            Title = "Another Refactoring",
            Isbn = "111",
            Price = 50.00m,
            StockCount = 10
        };

        Book differentIsbn = new Book
        {
            Title = "Another Book",
            Isbn = "999",
            Price = 50.00m,
            StockCount = 10
        };

        Console.WriteLine("\nEquality tests:");
        Console.WriteLine($"Same ISBN: {featured.Equals(sameIsbn)}");
        Console.WriteLine($"Different ISBN: {featured.Equals(differentIsbn)}");

        Console.WriteLine("\nBook information:");
        Console.WriteLine($"{featured.Title}, ISBN: {featured.Isbn}, Price: {featured.Price}, Stock: {featured.StockCount}");
    }
}
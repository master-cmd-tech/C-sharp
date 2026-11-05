using System;
using System.Collections.Generic;
using System.Linq;

record Book : IComparable<Book>
{
    public string Title { get; init; }
    public string Isbn { get; init; }
    public decimal Price { get; init; }
    public int StockCount { get; private init; }

    public Book(string title, string isbn, decimal price, int stockCount)
    {
        Title = title;
        Isbn = isbn;
        Price = price;
        StockCount = stockCount;
    }

    public int CompareTo(Book? other)
    {
        return Price.CompareTo(other?.Price);
    }

    public Book Restock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        return this with
        {
            StockCount = StockCount + quantity
        };
    }

    public Book Sell(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        if (quantity > StockCount)
            throw new InvalidOperationException("Not enough stock.");

        return this with
        {
            StockCount = StockCount - quantity
        };
    }

    public virtual bool Equals(Book? other)
    {
        return other != null && Isbn == other.Isbn;
    }

    public override int GetHashCode()
    {
        return Isbn.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Title} - ${Price} - Stock: {StockCount}";
    }
}

class Program
{
    static void Main()
    {
        List<Book> catalog = new()
        {
            new Book("Refactoring", "111", 45.00m, 4),
            new Book("Clean Code", "222", 35.50m, 2),
            new Book("The Pragmatic Programmer", "333", 40.00m, 6)
        };

        List<Book> sorted = new(catalog);
        sorted.Sort();

        Console.WriteLine("SORTED:");
        foreach (Book b in sorted)
            Console.WriteLine(b);

        List<Book> copy = new(sorted);

        Book duplicate = new(
            "Another Refactoring",
            "111",
            99.99m,
            100
        );

        copy.Add(duplicate);

        Console.WriteLine();
        Console.WriteLine("COUNT:");
        Console.WriteLine($"Before: {copy.Count}");

        var equalsResult = copy
            .Where(b => !b.Equals(duplicate))
            .ToList();

        Console.WriteLine($"After Equals: {equalsResult.Count}");

        var referenceResult = copy
            .Where(b => !ReferenceEquals(b, duplicate))
            .ToList();

        Console.WriteLine($"After ReferenceEquals: {referenceResult.Count}");

        Book book = catalog[0];

        Console.WriteLine();
        Console.WriteLine("RESTOCK:");

        Console.WriteLine($"Before: {book.StockCount}");

        book = book.Restock(5);

        Console.WriteLine($"After: {book.StockCount}");

        Console.WriteLine();
        Console.WriteLine("SELL:");

        Console.WriteLine($"Before: {book.StockCount}");

        book = book.Sell(2);

        Console.WriteLine($"After: {book.StockCount}");

        Console.WriteLine();
        Console.WriteLine("OVERSELL:");

        try
        {
            book = book.Sell(100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Rejected: {ex.Message}");
        }
    }
}

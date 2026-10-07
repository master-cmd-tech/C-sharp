using System;
using System.Collections.Generic;

public record Book : IComparable<Book>
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
        if (other is null)
            return 1;

        return Price.CompareTo(other.Price);
    }

    public Book Restock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Restock quantity must be positive.",
                nameof(quantity));

        return this with
        {
            StockCount = StockCount + quantity
        };
    }

    public Book Sell(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "Sell quantity must be positive.",
                nameof(quantity));

        if (quantity > StockCount)
            throw new InvalidOperationException(
                "Cannot sell more books than are currently in stock.");

        return this with
        {
            StockCount = StockCount - quantity
        };
    }

    public virtual bool Equals(Book? other)
    {
        if (other is null)
            return false;

        return Isbn == other.Isbn;
    }

    public override int GetHashCode()
    {
        return Isbn.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Title} | ISBN: {Isbn} | Price: {Price:C} | Stock: {StockCount}";
    }
}

class Program
{
    static void Main()
    {
        List<Book> catalog = new()
        {
            new Book(
                "Refactoring",
                "111",
                45.00m,
                4
            ),

            new Book(
                "Clean Code",
                "222",
                35.50m,
                2
            ),

            new Book(
                "The Pragmatic Programmer",
                "333",
                40.00m,
                6
            )
        };


        List<Book> sortedCatalog = new List<Book>(catalog);
        sortedCatalog.Sort();

        Console.WriteLine("Original catalog:");

        foreach (Book book in catalog)
        {
            Console.WriteLine(book);
        }

        Console.WriteLine();

        Console.WriteLine("Sorted by price:");

        foreach (Book book in sortedCatalog)
        {
            Console.WriteLine(book);
        }


        Book original = catalog[0];

        Book restocked = original.Restock(5);

        Console.WriteLine();
        Console.WriteLine("Restock:");
        Console.WriteLine($"Original: {original}");
        Console.WriteLine($"Restocked: {restocked}");

        Book sold = original.Sell(2);

        Console.WriteLine();
        Console.WriteLine("Sell:");
        Console.WriteLine($"Original: {original}");
        Console.WriteLine($"After selling: {sold}");
    }
}

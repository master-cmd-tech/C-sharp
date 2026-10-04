using System;
using System.Collections.Generic;

public record Book : IComparable<Book>
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

    public int CompareTo(Book? other)
    {
        if (other == null)
        {
            return 1;
        }

        return Price.CompareTo(other.Price);
    }

    public Book Restock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive.");
        }

        return this with
        {
            StockCount = StockCount + quantity
        };
    }

    public Book Sell(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive.");
        }

        if (quantity > StockCount)
        {
            throw new InvalidOperationException("Not enough stock.");
        }

        return this with
        {
            StockCount = StockCount - quantity
        };
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

        // Create a copy so the original catalogue is not changed.
        List<Book> sortedCatalog = new List<Book>(catalog);

        sortedCatalog.Sort();

        Console.WriteLine("Books sorted by price:");

        foreach (Book book in sortedCatalog)
        {
            Console.WriteLine(
                $"{book.Title} - ${book.Price:F2} - Stock: {book.StockCount}"
            );
        }

        // Restock
        Book originalBook = catalog[0];

        Console.WriteLine("\nRestock:");
        Console.WriteLine($"Original stock: {originalBook.StockCount}");

        Book restockedBook = originalBook.Restock(3);

        Console.WriteLine($"New book stock: {restockedBook.StockCount}");
        Console.WriteLine($"Original book stock: {originalBook.StockCount}");

        // Sell
        Console.WriteLine("\nSell:");

        Book soldBook = restockedBook.Sell(2);

        Console.WriteLine($"Before selling: {restockedBook.StockCount}");
        Console.WriteLine($"After selling: {soldBook.StockCount}");
        Console.WriteLine($"Original book still: {originalBook.StockCount}");

        // Oversell test
        Console.WriteLine("\nOversell test:");

        try
        {
            soldBook.Sell(100);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Sale rejected: {ex.Message}");
        }

        // Invalid quantity test
        Console.WriteLine("\nInvalid quantity test:");

        try
        {
            soldBook.Restock(0);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Restock rejected: {ex.Message}");
        }
    }
}

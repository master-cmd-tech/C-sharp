using System;
using System.Collections.Generic;
using System.Linq;

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

        // 1. Print sorted catalogue
        Console.WriteLine("1. Sorted catalogue:");

        List<Book> sortedCatalog = new List<Book>(catalog);
        sortedCatalog.Sort();

        foreach (Book book in sortedCatalog)
        {
            Console.WriteLine(
                $"{book.Title} - ${book.Price:F2} - Stock: {book.StockCount}"
            );
        }

        // 2. Create a copy and add a duplicate ISBN
        Console.WriteLine("\n2. Duplicate ISBN test:");

        List<Book> copy = new List<Book>(sortedCatalog);

        Book duplicate = new Book
        {
            Title = "Another Refactoring",
            Isbn = "111",
            Price = 100.00m,
            StockCount = 20
        };

        copy.Add(duplicate);

        Console.WriteLine($"Copy count after adding duplicate: {copy.Count}");

        // Check equality
        Book original = catalog[0];

        Console.WriteLine(
            $"original.Equals(duplicate): {original.Equals(duplicate)}"
        );

        Console.WriteLine(
            $"ReferenceEquals(original, duplicate): {ReferenceEquals(original, duplicate)}"
        );

        // 3. Wrong solution using Equals
        Console.WriteLine("\n3. Removing duplicate using Equals:");

        var withoutDuplicate = copy
            .Where(book => !book.Equals(duplicate))
            .ToList();

        Console.WriteLine(
            $"Count after Equals filter: {withoutDuplicate.Count}"
        );

        Console.WriteLine(
            "Both ISBN 111 books are removed because they are equal by ISBN."
        );

        // 4. Correct solution using ReferenceEquals
        Console.WriteLine("\n4. Removing exact duplicate using ReferenceEquals:");

        var fixedList = copy
            .Where(book => !ReferenceEquals(book, duplicate))
            .ToList();

        Console.WriteLine($"Count after ReferenceEquals filter: {fixedList.Count}");

        Console.WriteLine(
            $"Original Refactoring price: ${original.Price:F2}"
        );

        Console.WriteLine(
            $"Original Refactoring stock: {original.StockCount}"
        );

        // 5. Restock
        Console.WriteLine("\n5. Restock:");

        Book restockedBook = original.Restock(3);

        Console.WriteLine($"Before restock: {original.StockCount}");
        Console.WriteLine($"After restock: {restockedBook.StockCount}");
        Console.WriteLine($"Original book: {original.StockCount}");

        // 6. Sell
        Console.WriteLine("\n6. Sell:");

        Book soldBook = restockedBook.Sell(2);

        Console.WriteLine($"Before sale: {restockedBook.StockCount}");
        Console.WriteLine($"After sale: {soldBook.StockCount}");
        Console.WriteLine($"Original book: {original.StockCount}");

        // 7. Oversell
        Console.WriteLine("\n7. Oversell test:");

        try
        {
            soldBook.Sell(10);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Sale rejected: {ex.Message}");
        }
    }
}
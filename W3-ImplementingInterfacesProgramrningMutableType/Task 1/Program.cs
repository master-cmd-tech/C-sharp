using System;

public record Book
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

    public Book Restock(int amount)
    {
        return new Book(
            Title,
            Isbn,
            Price,
            StockCount + amount
        );
    }

    public Book Sell()
    {
        if (StockCount <= 0)
            return this;

        return new Book(
            Title,
            Isbn,
            Price,
            StockCount - 1
        );
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
}

class Program
{
    static void Main()
    {
        Book book1 = new Book(
            "Refactoring",
            "111",
            45.00m,
            4
        );

        Book book2 = new Book(
            "Refactoring",
            "111",
            50.00m,
            10
        );

        Book book3 = new Book(
            "Clean Code",
            "222",
            35.50m,
            2
        );


        Book featured = book1;
        Book display = featured;

        Console.WriteLine("Shared reference:");
        Console.WriteLine(featured.StockCount);


        Book changed = display.Sell();

        Console.WriteLine();
        Console.WriteLine("Immutable update:");
        Console.WriteLine($"Original stock: {book1.StockCount}");
        Console.WriteLine($"New stock: {changed.StockCount}");

        Console.WriteLine();
        Console.WriteLine("Equality:");

        Console.WriteLine(book1 == book2);
        Console.WriteLine(book1 == book3);
    }
}

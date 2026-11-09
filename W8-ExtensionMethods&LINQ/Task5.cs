
using System;
using System.Collections.Generic;
using System.Linq;

public record Transaction(string Channel, decimal Amount, decimal Fee);

public static class MyLinq
{
    public static IEnumerable<T> Filter<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var transactions = new List<Transaction>
        {
            new("Card", 200m, 4m),
            new("Transfer", 500m, 5m),
            new("Card", 50m, 1m),
            new("Card", 1200m, 24m),
            new("Transfer", 150m, 5m),
            new("Wallet", 80m, 0.8m)
        };

        int calls = 0;

        var big = transactions.Filter(t =>
        {
            calls++;
            return t.Amount > 100;
        });

        foreach (var t in big)
        {
            Console.WriteLine($"{t.Channel}: {t.Amount}");
        }

        Console.WriteLine($"Count: {big.Count()}");
        Console.WriteLine($"Calls: {calls}");


        transactions.Add(new Transaction("Crypto", 1000m, 20m));


        Console.WriteLine("\nSecond enumeration:");

        Console.WriteLine($"Count: {big.Count()}");
        Console.WriteLine($"Calls: {calls}");


        Console.WriteLine("\nThird enumeration:");

        Console.WriteLine($"Count: {big.Count()}");
        Console.WriteLine($"Calls: {calls}");


        var cached = big.ToList();

        Console.WriteLine("\nAfter ToList():");

        Console.WriteLine($"Cached count 1: {cached.Count}");
        Console.WriteLine($"Cached count 2: {cached.Count}");
        Console.WriteLine($"Calls: {calls}");
    }
}

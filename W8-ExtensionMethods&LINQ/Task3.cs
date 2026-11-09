
using System;
using System.Collections.Generic;

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

    public static IEnumerable<R> Transform<T, R>(
        this IEnumerable<T> source,
        Func<T, R> selector)
    {
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }
}

class Task3
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

        var fees = transactions
            .Filter(t => t.Amount > 100)
            .Transform(t => t.Fee);

        foreach (var fee in fees)
        {
            Console.WriteLine(fee);
        }
    }
}

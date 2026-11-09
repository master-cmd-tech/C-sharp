
using System;
using System.Collections.Generic;
using System.Linq;

public record Transaction(string Channel, decimal Amount, decimal Fee);

public static class TransactionExtensions
{
    public static IEnumerable<Transaction> ForChannel(
        this IEnumerable<Transaction> transactions,
        string channel)
    {
        return transactions.Where(t => t.Channel == channel);
    }

    public static decimal TotalFees(
        this IEnumerable<Transaction> transactions)
    {
        return transactions.Sum(t => t.Fee);
    }
}

class Task4
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

        decimal cardFees = transactions
            .ForChannel("Card")
            .TotalFees();

        Console.WriteLine($"Total Card fees: {cardFees:F2}");
    }
}


using System;
using System.Collections.Generic;
using System.Linq;

public record Transaction(string Channel, decimal Amount, decimal Fee);

class Task1
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
            new("Wallet", 80m, 0.8m),
        };

        var feeByChannel = transactions
            .GroupBy(t => t.Channel)
            .Select(g => new
            {
                Channel = g.Key,
                TotalFee = g.Sum(t => t.Fee)
            });

        Console.WriteLine("Total fees per channel:");

        foreach (var item in feeByChannel)
        {
            Console.WriteLine($"{item.Channel}: {item.TotalFee:F2}");
        }

        var largestTransactions = transactions
            .OrderByDescending(t => t.Amount)
            .Take(3);

        Console.WriteLine("\nThree largest transactions:");

        foreach (var t in largestTransactions)
        {
            Console.WriteLine($"{t.Channel}: {t.Amount}");
        }

        var cardAverage = transactions
            .Where(t => t.Channel == "Card")
            .Average(t => t.Amount);

        Console.WriteLine($"\nCard average: {cardAverage:F2}");
    }
}

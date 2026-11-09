
using System;
using System.Collections.Generic;
using System.Linq;

public record Transaction(string Channel, decimal Amount, decimal Fee);

class Task2
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

        var cryptoTransactions = transactions
            .Where(t => t.Channel == "Crypto");

        if (cryptoTransactions.Any())
        {
            var cryptoAverage = cryptoTransactions.Average(t => t.Amount);
            Console.WriteLine($"Crypto average: {cryptoAverage:F2}");
        }
        else
        {
            Console.WriteLine("No Crypto transactions found.");
        }
    }
}

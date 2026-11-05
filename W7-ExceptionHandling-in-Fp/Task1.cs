
using System;

public record PaymentBalance(string Owner, decimal Amount);

public class Task1
{
    static decimal ParseAmount(string text)
    {
        return decimal.Parse(text);
    }

    static void EnsurePositive(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");
    }

    static PaymentBalance Withdraw(PaymentBalance balance, decimal amount)
    {
        if (amount > balance.Amount)
            throw new InvalidOperationException("Insufficient funds.");

        return balance with
        {
            Amount = balance.Amount - amount
        };
    }

    static string Process(PaymentBalance balance, string input)
    {
        try
        {
            var amount = ParseAmount(input);
            EnsurePositive(amount);

            var after = Withdraw(balance, amount);

            return $"OK: balance is now {after.Amount:F2}";
        }
        catch (Exception ex)
        {
            return $"ERROR: {ex.GetType().Name} - {ex.Message}";
        }
    }

    public static void Main()
    {
        var balance = new PaymentBalance("Aigerim", 1000m);

        var inputs = new[] { "250", "abc", "-5", "99999" };

        foreach (var input in inputs)
        {
            Console.WriteLine($"{input,-6} -> {Process(balance, input)}");
        }
    }
}

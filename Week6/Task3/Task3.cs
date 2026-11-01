
using System;

public record Draft(decimal Amount);

public record Validated(decimal Amount);

public record Priced(decimal Amount);

public record Receipt(string TransactionId, decimal Amount);

public class Task3
{
    static Validated Validate(Draft draft)
    {
        if (draft.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }

        return new Validated(draft.Amount);
    }

    static Priced ApplyDiscount(Validated validated, decimal percent)
    {
        decimal discountedAmount = validated.Amount - validated.Amount * percent;

        return new Priced(discountedAmount);
    }

    static Receipt Charge(Priced priced, Func<decimal, string> gateway)
    {
        string transactionId = gateway(priced.Amount);

        return new Receipt(transactionId, priced.Amount);
    }

    static string RealGateway(decimal amount)
    {
        string transactionId = Guid.NewGuid().ToString();

        Console.WriteLine($"Processing payment of {amount:F2}");

        return transactionId;
    }

    public static void Main()
    {
        Draft draft = new Draft(1000m);
        Validated validated = Validate(draft);
        Priced priced = ApplyDiscount(validated, 0.10m);

        Console.WriteLine("Real Gateway");

        Receipt receipt1 = Charge(priced, RealGateway);

        Console.WriteLine($"Transaction ID: {receipt1.TransactionId}");
        Console.WriteLine($"Amount: {receipt1.Amount:F2}");

        Console.WriteLine("\nFake Gateway");

        int calls = 0;

        Func<decimal, string> fakeGateway = amount =>
        {
            calls++;

            Console.WriteLine($"Fake payment: {amount:F2}");

            return "TEST-123";
        };

        Receipt receipt2 = Charge(priced, fakeGateway);

        Console.WriteLine($"Transaction ID: {receipt2.TransactionId}");
        Console.WriteLine($"Amount: {receipt2.Amount:F2}");
        Console.WriteLine($"Gateway calls: {calls}");
    }
}

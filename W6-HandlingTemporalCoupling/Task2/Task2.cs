using System;

public record Draft(decimal Amount);
public record Validated(decimal Amount);
public record Priced(decimal Amount);
public record Receipt(string TransactionId, decimal Amount);
public class Task2
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

    static Receipt Charge(Priced priced)
    {
        string transactionId = Guid.NewGuid().ToString();

        Console.WriteLine($"Charged {priced.Amount:F2}");

        return new Receipt(transactionId, priced.Amount);
    }

    public static void Main()
    {
        Draft draft = new Draft(1000m);

        Validated validated = Validate(draft);

        Priced priced = ApplyDiscount(validated, 0.10m);

        Receipt receipt = Charge(priced);

        Console.WriteLine($"Transaction ID: {receipt.TransactionId}");
        Console.WriteLine($"Receipt amount: {receipt.Amount:F2}");
    }
}


using System;

public record Draft(decimal Amount);
public record Validated(decimal Amount);
public record Priced(decimal Amount);
public record Receipt(string TransactionId, decimal Amount);

public class Task4
{
    static Validated Validate(Draft draft)
    {
        if (draft.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        return new Validated(draft.Amount);
    }

    static Priced ApplyDiscount(Validated validated, decimal percent)
    {
        decimal amount = validated.Amount * (1 - percent);
        return new Priced(amount);
    }

    static Receipt Charge(Priced priced, Func<decimal, string> gateway)
    {
        string id = gateway(priced.Amount);
        return new Receipt(id, priced.Amount);
    }

    static string FakeGateway(decimal amount)
    {
        Console.WriteLine($"Payment processed: {amount:F2}");
        return "TEST-123";
    }

    public static void Main()
    {
        var draft = new Draft(1000m);
        var validated = Validate(draft);
        var priced = ApplyDiscount(validated, 0.10m);

        Console.WriteLine("Correct sequence");

        var receipt = Charge(priced, FakeGateway);
        Console.WriteLine($"Receipt: {receipt.TransactionId}, {receipt.Amount:F2}");


        // 1. Discount applied twice:
        // var pricedAgain = ApplyDiscount(priced, 0.10m);
        // ERROR CS1503: Priced cannot be used where Validated is expected.

        // 2. Discount forgotten:
        // var receiptWithoutDiscount = Charge(validated, FakeGateway);
        // ERROR CS1503: Validated cannot be used where Priced is expected.

        // 3. Charge called with Receipt:
        // var secondReceipt = Charge(receipt, FakeGateway);
        // ERROR CS1503: Receipt cannot be used where Priced is expected.


        Console.WriteLine("\nRepeated charge test");

        var receipt1 = Charge(priced, FakeGateway);
        var receipt2 = Charge(priced, FakeGateway);

        Console.WriteLine($"First receipt: {receipt1.TransactionId}");
        Console.WriteLine($"Second receipt: {receipt2.TransactionId}");
        Console.WriteLine("Both calls compiled and executed.");
    }
}

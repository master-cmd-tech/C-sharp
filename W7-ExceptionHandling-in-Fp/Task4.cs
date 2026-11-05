using System;

public abstract record Result<T>;
public sealed record Ok<T>(T Value) : Result<T>;
public sealed record Fail<T>(string Error) : Result<T>;

public static class ResultExtensions
{
    public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> func) =>
        result switch
        {
            Ok<T> ok => func(ok.Value),
            Fail<T> fail => new Fail<U>(fail.Error),
            _ => throw new InvalidOperationException()
        };

    public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> func) =>
        result switch
        {
            Ok<T> ok => new Ok<U>(func(ok.Value)),
            Fail<T> fail => new Fail<U>(fail.Error),
            _ => throw new InvalidOperationException()
        };
}

public record PaymentBalance(string Owner, decimal Amount);

public class Program
{

    static Result<decimal> ParseAmount(string text) =>
        decimal.TryParse(text, out var amount)
            ? new Ok<decimal>(amount)
            : new Fail<decimal>("Failed to parse decimal.");

    static Result<decimal> EnsurePositive(decimal amount) =>
        amount > 0
            ? new Ok<decimal>(amount)
            : new Fail<decimal>("Amount must be positive.");

    static Result<PaymentBalance> Withdraw(PaymentBalance balance, decimal amount) =>
        amount <= balance.Amount
            ? new Ok<PaymentBalance>(balance with { Amount = balance.Amount - amount })
            : new Fail<PaymentBalance>("Insufficient funds.");

    static string Process(PaymentBalance balance, string input)
    {
        Result<string> finalResult = ParseAmount(input)
            .Bind(EnsurePositive)
            .Bind(amount => Withdraw(balance, amount))
            .Map(after => $"OK: balance is now {after.Amount:F2}"); 

        return finalResult switch
        {
            Ok<string> ok => ok.Value,
            Fail<string> fail => $"ERROR: {fail.Error}",
            _ => throw new InvalidOperationException("Unexpected result state.")
        };
    }

    public static void Main()
    {
        var balance = new PaymentBalance("Aigerim", 1000m);

        foreach (var input in new[] { "250", "abc", "-5", "99999" })
        {
            Console.WriteLine($"{input,-6} -> {Process(balance, input)}");
        }
    }
}
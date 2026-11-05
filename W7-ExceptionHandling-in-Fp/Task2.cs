using System;

public abstract record Result<T>;

public sealed record Ok<T>(T Value) : Result<T>;

public sealed record Fail<T>(string Error) : Result<T>;

public static class ResultExtensions
{
    public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> func) => result switch
        {
            Ok<T> ok => func(ok.Value), 
            Fail<T> fail => new Fail<U>(fail.Error), 
            _ => throw new InvalidOperationException("Unknown Result state.")
        };

    public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> func) =>
        result switch
        {
            Ok<T> ok => new Ok<U>(func(ok.Value)), 
            Fail<T> fail => new Fail<U>(fail.Error), 
            _ => throw new InvalidOperationException("Unknown Result state.")
        };
}

public class Task2
{
    public static void Main()
    {   
        Result<int> start = new Ok<int>(21);

        Result<string> result = start.Map(x => x * 2).Bind(x => x > 40 ? (Result<string>)new Ok<string>($"Number {x} suitable!") 
                : new Fail<string>("Too low number"));

        string output = result switch
        {
            Ok<string> ok => ok.Value,
            Fail<string> fail => $"Error: {fail.Error}",
            _ => "Unknown condition"
        };

        Console.WriteLine(output);
    }
}
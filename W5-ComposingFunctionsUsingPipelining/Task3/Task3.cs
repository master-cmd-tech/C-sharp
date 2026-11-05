
using System;

namespace Task3
{
    class Task3
    {
        static decimal ApplyDiscount(decimal rate, decimal amount) => amount - amount * rate;

        static decimal AddFee(decimal fee, decimal amount) => amount + fee;

        static decimal RoundToCents(decimal amount) => Math.Round(amount, 2, MidpointRounding.AwayFromZero);

        static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> function) => a => b => function(a, b);

        static void Main()
        {
            Func<decimal, decimal> discount10 = Curry<decimal, decimal, decimal>(ApplyDiscount)(0.10m);

            Func<decimal, decimal> fee5 = Curry<decimal, decimal, decimal>(AddFee)(5m);

            decimal price = 1234.56m.Pipe(discount10).Pipe(fee5).Pipe(RoundToCents);

            Console.WriteLine($"Final price: {price}");
        }
    }

    static class PipeExtensions
    {
        public static R Pipe<T, R>( this T value, Func<T, R> function) => function(value);
    }
}


using System;
using System.Linq;

namespace Task4
{
    class Task4
    {
        static decimal ApplyDiscount(decimal rate, decimal amount) => amount - amount * rate;

        static decimal AddFee(decimal fee, decimal amount) => amount + fee;

        static decimal RoundToCents(decimal amount) => Math.Round(amount, 2, MidpointRounding.AwayFromZero);

        static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> function) => a => b => function(a, b);

        static Func<decimal, decimal> Pipeline(params Func<decimal, decimal>[] steps) 
        => steps.Aggregate((Func<decimal, decimal>)(x => x),(current, next) => x => next(current(x)));
        

        static void Main()
        {
            Func<decimal, decimal> discount10 = Curry<decimal, decimal, decimal>(ApplyDiscount)(0.10m);

            Func<decimal, decimal> fee5 = Curry<decimal, decimal, decimal>(AddFee)(5m);

            var normalPipeline = Pipeline(discount10,fee5,RoundToCents);

            var swappedPipeline = Pipeline(fee5,discount10,RoundToCents);

            var roundingFirstPipeline = Pipeline(RoundToCents,discount10,fee5);

            decimal amount = 1234.56m;

            Console.WriteLine($"Normal pipeline: {normalPipeline(amount)}");
            Console.WriteLine($"Fee first: {swappedPipeline(amount)}");
            Console.WriteLine($"Rounding first: {roundingFirstPipeline(amount)}");

            var emptyPipeline = Pipeline();

            Console.WriteLine($"Empty pipeline: {emptyPipeline(amount)}");
        }
    }
}

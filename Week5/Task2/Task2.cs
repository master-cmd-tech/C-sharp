using System;

namespace Task2
{
    class Task2
    {
        static decimal ApplyDiscount(decimal rate, decimal amount) => amount - amount * rate;
        static decimal AddFee(decimal fee, decimal amount)         => amount + fee;
        static decimal RoundToCents(decimal amount) => Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> function) => a => b => function(a, b);
        static Func<A, C> Compose<A, B, C>( Func<A, B> f, Func<B, C> g) => x => g(f(x));
        static void Main() {

            Func<decimal, decimal> discount10 = Curry<decimal, decimal, decimal>(ApplyDiscount)(0.10m);

            Func<decimal, decimal> fee5 = Curry<decimal, decimal, decimal>(AddFee)(5m);

            Func<decimal, decimal> checkout = Compose<decimal, decimal, decimal>(
                Compose<decimal, decimal, decimal>(discount10, fee5),RoundToCents);

            decimal price = checkout(1234.56m);

            Console.WriteLine($"Final price: {price}");
        }
    }   
}
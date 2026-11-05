using System;

namespace Task1
{
    class Task1
    {
        static decimal ApplyDiscount(decimal rate, decimal amount) => amount - amount * rate;
        static decimal AddFee(decimal fee, decimal amount)         => amount + fee;
        static Func<A, Func<B, C>> Curry<A, B, C>(Func<A, B, C> function) => a => b => function(a, b);
        static void Main() {

            Func<decimal, decimal> discount10 = Curry<decimal, decimal, decimal>(ApplyDiscount)(0.10m);

            Func<decimal, decimal> fee5 = Curry<decimal, decimal, decimal>(AddFee)(5m);

            Console.WriteLine(discount10(100m)); 
            Console.WriteLine(fee5(100m));
        }
    }   
}
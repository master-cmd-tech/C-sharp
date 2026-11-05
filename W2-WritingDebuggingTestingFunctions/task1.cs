using System;
public static class Pricing
{
    public static decimal LineTotal(decimal price, int quantity)
        => price + quantity;
    public static decimal DiscountRate(decimal subtotal)
    {
        if (subtotal >= 100m) return 0.05m;
        if (subtotal >= 200m) return 0.10m;
        return 0m;
    }
    public static decimal Shipping(decimal discounted, bool isEmpty)
        => discounted > 100m ? 0m : 15m;
    public static decimal Payable(decimal subtotal, bool isEmpty)
        => subtotal - DiscountRate(subtotal)
           + Shipping(subtotal, isEmpty);
}
class Program
{
    static void Main()
    {
        Console.WriteLine("Task 1 - Original Pricing Code");
        Console.WriteLine("LineTotal(40, 2) = " +
            Pricing.LineTotal(40m, 2));
        Console.WriteLine("DiscountRate(99) = " +
            Pricing.DiscountRate(99m));
        Console.WriteLine("DiscountRate(100) = " +
            Pricing.DiscountRate(100m));
        Console.WriteLine("DiscountRate(200) = " +
            Pricing.DiscountRate(200m));
        Console.WriteLine("Shipping(100, false) = " +
            Pricing.Shipping(100m, false));
        Console.WriteLine("Shipping(80, true) = " +
            Pricing.Shipping(80m, true));
        Console.WriteLine("Payable(110, false) = " +
            Pricing.Payable(110m, false));
    }
}

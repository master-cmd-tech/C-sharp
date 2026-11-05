using System;
using System.Collections.Generic;
public record OrderLine(decimal Price, int Quantity);
public delegate decimal DiscountRule(decimal subtotal);
public static class Pricing
{
    public static decimal LineTotal(decimal price, int quantity)
        => price * quantity;
    public static decimal Subtotal(List<OrderLine> lines)
    {
        decimal total = 0;
        foreach (var line in lines)
            total += LineTotal(line.Price, line.Quantity);
        return total;
    }
    public static decimal StandardDiscount(decimal subtotal)
        => subtotal >= 200 ? 0.10m :
           subtotal >= 100 ? 0.05m : 0m;

    public static decimal Discounted(decimal subtotal, DiscountRule rule)
        => subtotal - subtotal * rule(subtotal);
    public static decimal Shipping(decimal discounted, bool empty)
        => empty ? 0m :
           discounted >= 100 ? 0m : 15m;
    public static decimal Payable(
        decimal subtotal,
        bool empty,
        DiscountRule rule)
    {
        decimal discounted = Discounted(subtotal, rule);
        return discounted + Shipping(discounted, empty);
    }
}
class Program
{
    static void Main()
    {
        var order = new List<OrderLine>
        {
            new(40m, 2),
            new(30m, 1)
        };
        decimal subtotal = Pricing.Subtotal(order);
        DiscountRule standard = Pricing.StandardDiscount;
        DiscountRule noDiscount = _ => 0m;
        Show("Standard discount", subtotal, false, standard);
        Show("No discount", subtotal, false, noDiscount);
        // Second example: subtotal below 100
        var smallOrder = new List<OrderLine>
        {
            new(40m, 1),
            new(30m, 1)
        };
        decimal smallSubtotal = Pricing.Subtotal(smallOrder);
        Show("Small order", smallSubtotal, false, standard);
    }
    static void Show(
        string title,
        decimal subtotal,
        bool empty,
        DiscountRule rule)
    {
        decimal rate = rule(subtotal);
        decimal discount = subtotal * rate;
        decimal discounted = Pricing.Discounted(subtotal, rule);
        decimal shipping = Pricing.Shipping(discounted, empty);
        decimal payable = discounted + shipping;

        Console.WriteLine($"\n{title}");
        Console.WriteLine($"Subtotal: {subtotal:F2}");
        Console.WriteLine($"Discount: {discount:F2}");
        Console.WriteLine($"Discounted subtotal: {discounted:F2}");
        Console.WriteLine($"Shipping: {shipping:F2}");
        Console.WriteLine($"Payable: {payable:F2}");
    }
}
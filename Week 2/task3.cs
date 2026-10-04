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
        => empty ? 0m : discounted >= 100 ? 0m : 15m;

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
    static int passed = 0;
    static int failed = 0;

    static void Main()
    {
        Test("LineTotal", 
            Pricing.LineTotal(40m, 2), 80m);

        Test("LineTotal quantity 1", 
            Pricing.LineTotal(30m, 1), 30m);

        var order = new List<OrderLine>
        {
            new(40m, 2),
            new(30m, 1)
        };

        Test("Subtotal", 
            Pricing.Subtotal(order), 110m);

        Test("Discount below 100", 
            Pricing.StandardDiscount(99m), 0m);

        Test("Discount at 100", 
            Pricing.StandardDiscount(100m), 0.05m);

        Test("Discount below 200", 
            Pricing.StandardDiscount(199.99m), 0.05m);

        Test("Discount at 200", 
            Pricing.StandardDiscount(200m), 0.10m);

        Test("Shipping below 100", 
            Pricing.Shipping(99m, false), 15m);

        Test("Shipping at 100", 
            Pricing.Shipping(100m, false), 0m);

        Test("Empty order shipping", 
            Pricing.Shipping(0m, true), 0m);

        DiscountRule standard = Pricing.StandardDiscount;
        DiscountRule noDiscount = _ => 0m;

        Test("Standard discount amount", 
            Pricing.Discounted(110m, standard), 104.50m);

        Test("No discount rule", 
            Pricing.Discounted(110m, noDiscount), 110m);

        int originalCount = order.Count;
        decimal firstSubtotal = Pricing.Subtotal(order);
        decimal secondSubtotal = Pricing.Subtotal(order);

        Test("Collection preserved", 
            order.Count, originalCount);

        Test("Repeated calls consistent", 
            secondSubtotal, firstSubtotal);

        Test("Payable standard", 
            Pricing.Payable(110m, false, standard), 104.50m);

        Test("Payable no discount", 
            Pricing.Payable(110m, false, noDiscount), 110m);

        Console.WriteLine();
        Console.WriteLine("===== TEST SUMMARY =====");
        Console.WriteLine($"Passed: {passed}");
        Console.WriteLine($"Failed: {failed}");
        Console.WriteLine($"Total:  {passed + failed}");
    }

    static void Test(string name, decimal actual, decimal expected)
    {
        if (actual == expected)
        {
            Console.WriteLine($"PASS: {name}");
            passed++;
        }
        else
        {
            Console.WriteLine(
                $"FAIL: {name} | Expected: {expected} | Actual: {actual}");

            failed++;
        }
    }

    static void Test(string name, int actual, int expected)
    {
        if (actual == expected)
        {
            Console.WriteLine($"PASS: {name}");
            passed++;
        }
        else
        {
            Console.WriteLine(
                $"FAIL: {name} | Expected: {expected} | Actual: {actual}");

            failed++;
        }
    }
}
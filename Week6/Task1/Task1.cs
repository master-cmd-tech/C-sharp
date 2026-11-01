
using System;

public class Checkout
{
    private decimal _total;
    private bool _validated;

    public void Start(decimal amount)          { _total = amount; }
    public void Validate()                     { _validated = _total > 0; }
    public void ApplyDiscount(decimal percent) { _total -= _total * percent; }

    public void Charge()
    {
        if (!_validated)
            throw new InvalidOperationException("Not validated.");

        Console.WriteLine($"Charged {_total:F2}");
    }
}

public class Task1
{
    public static void Main()
    {
        Console.WriteLine("1. Discount applied twice:");

        var c1 = new Checkout();
        c1.Start(1000m);
        c1.Validate();
        c1.ApplyDiscount(0.10m);
        c1.ApplyDiscount(0.10m);
        c1.Charge();

        Console.WriteLine("\n2. Discount forgotten:");

        var c2 = new Checkout();
        c2.Start(1000m);
        c2.Validate();
        c2.Charge();

        Console.WriteLine("\n3. Charge called twice:");

        var c3 = new Checkout();
        c3.Start(1000m);
        c3.Validate();
        c3.ApplyDiscount(0.10m);
        c3.Charge();
        c3.Charge();

        Console.WriteLine("\n4. Charge before Validate:");

        var c4 = new Checkout();
        c4.Start(1000m);

        try
        {
            c4.Charge();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

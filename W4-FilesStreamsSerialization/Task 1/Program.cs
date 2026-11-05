using System;
using System.IO;

public static class AttendanceLog
{
    public static void Record(string path, string name)
    {
        string line = name + "," + DateTime.Now;

        File.WriteAllText(path, line);
    }

    public static string ReadAll(string path)
    {
        return File.ReadAllText(path);
    }
}

class Program
{
    static void Main()
    {
        string path = "attendance.txt";

        if (File.Exists(path))
        {
            File.Delete(path);
        }


        Console.WriteLine("1. Record overwriting problem:");

        AttendanceLog.Record(path, "Alice");
        AttendanceLog.Record(path, "Bob");

        string result = File.ReadAllText(path);

        Console.WriteLine("Expected:");
        Console.WriteLine("Alice and Bob should both be stored.");

        Console.WriteLine("Actual:");
        Console.WriteLine(result);

        Console.WriteLine();

        Console.WriteLine("2. Missing file problem:");

        string missingPath = "missing.txt";

        if (File.Exists(missingPath))
        {
            File.Delete(missingPath);
        }

        try
        {
            AttendanceLog.ReadAll(missingPath);
            Console.WriteLine("Expected: empty result.");
            Console.WriteLine("Actual: no exception.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Expected: missing file should be handled safely.");
            Console.WriteLine($"Actual: {ex.GetType().Name} - {ex.Message}");
        }

        Console.WriteLine();

        Console.WriteLine("3. Data loss after multiple records:");

        AttendanceLog.Record(path, "Charlie");
        AttendanceLog.Record(path, "David");
        AttendanceLog.Record(path, "Emma");

        string finalResult = File.ReadAllText(path);

        Console.WriteLine("Expected:");
        Console.WriteLine("Charlie, David and Emma should all exist.");

        Console.WriteLine("Actual:");
        Console.WriteLine(finalResult);

        Console.WriteLine();

        Console.WriteLine("Root cause:");
        Console.WriteLine(
            "File.WriteAllText(path, line) replaces the entire file every time."
        );
    }
}

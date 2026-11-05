using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public record AttendanceEntry(string Name, DateTime CheckInTime);

public static class AttendanceFormat
{
    public static string ToLine(AttendanceEntry entry)
    {
        return entry.Name + "," + entry.CheckInTime.ToString("O");
    }

    public static AttendanceEntry FromLine(string line)
    {
        string[] parts = line.Split(',', 2);

        if (parts.Length != 2)
        {
            throw new FormatException("Invalid attendance line.");
        }

        string name = parts[0];
        DateTime checkInTime = DateTime.Parse(parts[1]);

        return new AttendanceEntry(name, checkInTime);
    }
}

public interface IAttendanceStore
{
    void Append(IEnumerable<AttendanceEntry> entries);

    IEnumerable<AttendanceEntry> Load();
}

public class TextLogStore : IAttendanceStore
{
    private readonly string path;

    public TextLogStore(string path)
    {
        this.path = path;
    }

    public void Append(IEnumerable<AttendanceEntry> entries)
    {
        using StreamWriter writer = new StreamWriter(path, append: true);

        foreach (AttendanceEntry entry in entries)
        {
            writer.WriteLine(AttendanceFormat.ToLine(entry));
        }
    }

    public IEnumerable<AttendanceEntry> Load()
    {
        if (!File.Exists(path))
        {
            return Enumerable.Empty<AttendanceEntry>();
        }

        List<AttendanceEntry> result = new List<AttendanceEntry>();

        foreach (string line in File.ReadLines(path))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                result.Add(AttendanceFormat.FromLine(line));
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("TASK 2");
        Console.WriteLine();

        AttendanceEntry entry = new AttendanceEntry(
            "Alice",
            new DateTime(2026, 10, 4, 10, 30, 0)
        );

        string line = AttendanceFormat.ToLine(entry);
        AttendanceEntry restored = AttendanceFormat.FromLine(line);

        Console.WriteLine("In-memory conversion:");
        Console.WriteLine($"Original: {entry}");
        Console.WriteLine($"Text: {line}");
        Console.WriteLine($"Restored: {restored}");

        Console.WriteLine();

        string path = "attendance_task2.txt";

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        IAttendanceStore store = new TextLogStore(path);

        store.Append(new[]
        {
            entry,
            new AttendanceEntry(
                "Bob",
                new DateTime(2026, 10, 4, 10, 35, 0)
            )
        });

        Console.WriteLine("Loaded from TextLogStore:");

        foreach (AttendanceEntry item in store.Load())
        {
            Console.WriteLine(
                $"{item.Name} - {item.CheckInTime:O}"
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

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

        return new AttendanceEntry(
            parts[0],
            DateTime.Parse(parts[1])
        );
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

        List<AttendanceEntry> result = new();

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

public class JsonStore : IAttendanceStore
{
    private readonly string path;

    private readonly JsonSerializerOptions options =
        new JsonSerializerOptions
        {
            WriteIndented = true,
            TypeInfoResolver =new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
        };

    public JsonStore(string path)
    {
        this.path = path;
    }

    public void Append(IEnumerable<AttendanceEntry> entries)
    {
        List<AttendanceEntry> existing =
            Load().ToList();

        existing.AddRange(entries);

        string json =
            JsonSerializer.Serialize(existing, options);

        using StreamWriter writer =
            new StreamWriter(path, append: false);

        writer.Write(json);
    }

    public IEnumerable<AttendanceEntry> Load()
    {
        if (!File.Exists(path))
        {
            return Enumerable.Empty<AttendanceEntry>();
        }

        string json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
        {
            return Enumerable.Empty<AttendanceEntry>();
        }

        return JsonSerializer.Deserialize<List<AttendanceEntry>>(json)
               ?? new List<AttendanceEntry>();
    }
}

class Program
{
    static List<AttendanceEntry> RunAttendance(
        IAttendanceStore store,
        IEnumerable<AttendanceEntry> newEntries)
    {
        store.Append(newEntries);

        return store.Load().ToList();
    }

    static void PrintEntries(
        string storeName,
        List<AttendanceEntry> entries)
    {
        Console.WriteLine(storeName);
        Console.WriteLine($"Count: {entries.Count}");

        foreach (AttendanceEntry entry in entries)
        {
            Console.WriteLine(
                $"{entry.Name} - {entry.CheckInTime:O}"
            );
        }

        Console.WriteLine();
    }

    static void Main()
    {
        string textPath = "attendance_task4.txt";
        string jsonPath = "attendance_task4.json";

        // Start with empty files.
        if (File.Exists(textPath))
        {
            File.Delete(textPath);
        }

        if (File.Exists(jsonPath))
        {
            File.Delete(jsonPath);
        }

        IAttendanceStore textStore =
            new TextLogStore(textPath);

        IAttendanceStore jsonStore =
            new JsonStore(jsonPath);

        Console.WriteLine("PROGRAM RUN 1");

        List<AttendanceEntry> run1 = new()
        {
            new AttendanceEntry(
                "Alice",
                new DateTime(2026, 10, 4, 10, 00, 0)
            ),

            new AttendanceEntry(
                "Bob",
                new DateTime(2026, 10, 4, 10, 05, 0)
            )
        };

        List<AttendanceEntry> textAfterRun1 =
            RunAttendance(textStore, run1);

        List<AttendanceEntry> jsonAfterRun1 =
            RunAttendance(jsonStore, run1);

        PrintEntries("TextLogStore:", textAfterRun1);
        PrintEntries("JsonStore:", jsonAfterRun1);

        Console.WriteLine(
            $"Stores identical: " +
            $"{textAfterRun1.SequenceEqual(jsonAfterRun1)}"
        );

        Console.WriteLine();


        Console.WriteLine("PROGRAM RUN 2");

        List<AttendanceEntry> run2 = new()
        {
            new AttendanceEntry(
                "Charlie",
                new DateTime(2026, 10, 4, 10, 10, 0)
            ),

            new AttendanceEntry(
                "David",
                new DateTime(2026, 10, 4, 10, 15, 0)
            )
        };

        List<AttendanceEntry> textAfterRun2 =
            RunAttendance(textStore, run2);

        List<AttendanceEntry> jsonAfterRun2 =
            RunAttendance(jsonStore, run2);

        PrintEntries("TextLogStore:", textAfterRun2);
        PrintEntries("JsonStore:", jsonAfterRun2);

        Console.WriteLine(
            $"Stores identical: " +
            $"{textAfterRun2.SequenceEqual(jsonAfterRun2)}"
        );

        Console.WriteLine();

        Console.WriteLine("FINAL CHECKS");

        bool sameCount =
            textAfterRun2.Count == jsonAfterRun2.Count;

        bool sameValues =
            textAfterRun2.SequenceEqual(jsonAfterRun2);

        bool noDuplicates =
            textAfterRun2.Distinct().Count()
            == textAfterRun2.Count;

        Console.WriteLine($"Same count: {sameCount}");
        Console.WriteLine($"Same values: {sameValues}");
        Console.WriteLine($"No duplicates: {noDuplicates}");
    }
}

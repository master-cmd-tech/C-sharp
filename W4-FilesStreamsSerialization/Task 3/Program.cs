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

public class JsonStore : IAttendanceStore
{
    private readonly string path;

    private readonly JsonSerializerOptions options =
        new JsonSerializerOptions
        {
            WriteIndented = true
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

        string json = JsonSerializer.Serialize(existing, options);

        using StreamWriter writer = new StreamWriter(path, append: false);

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

        List<AttendanceEntry>? entries =
            JsonSerializer.Deserialize<List<AttendanceEntry>>(json);

        return entries ?? new List<AttendanceEntry>();
    }
}

class Program
{
    static void DemonstrateStore(
        IAttendanceStore store,
        string storeName)
    {
        Console.WriteLine(storeName);

        List<AttendanceEntry> newEntries = new()
        {
            new AttendanceEntry(
                "Alice",
                new DateTime(2026, 10, 4, 10, 30, 0)
            ),

            new AttendanceEntry(
                "Bob",
                new DateTime(2026, 10, 4, 10, 35, 0)
            )
        };

        store.Append(newEntries);

        List<AttendanceEntry> loaded =
            store.Load().ToList();

        Console.WriteLine($"Count: {loaded.Count}");

        foreach (AttendanceEntry entry in loaded)
        {
            Console.WriteLine(
                $"{entry.Name} - {entry.CheckInTime:O}"
            );
        }

        Console.WriteLine();
    }

    static void Main()
    {
        string textPath = "attendance_task3.txt";
        string jsonPath = "attendance_task3.json";

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

        DemonstrateStore(textStore, "TextLogStore");
        DemonstrateStore(jsonStore, "JsonStore");

        Console.WriteLine("Both stores use the same demonstration function.");
    }
}

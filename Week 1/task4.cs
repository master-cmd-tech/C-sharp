using System;

string[] inputs = { "85", " 70 ", "49", "100", "0",
                    "abc", "", "72.5", "-1", "101" };

Console.WriteLine("Given inputs:");
ProcessScores(inputs);

Console.WriteLine();
Console.WriteLine("Interactive mode");
Console.WriteLine("Enter scores one per line. Type 'done' to finish.");

while (true)
{
    string? input = Console.ReadLine();

    if (input == null || input.ToLower() == "done")
        break;

    Console.WriteLine(Describe(input, 50));
}

void ProcessScores(string[] values)
{
    int validCount = 0;
    int rejectedCount = 0;
    int passedCount = 0;
    int sum = 0;

    foreach (string input in values)
    {
        if (ValidateScore(input, out int score, out string error))
        {
            int finalScore = AddBonus(score);

            Console.WriteLine($"{input}: {DescribeScore(finalScore, 50)}");

            validCount++;
            sum += finalScore;

            if (finalScore >= 50)
                passedCount++;
        }
        else
        {
            Console.WriteLine($"{input}: {error}");
            rejectedCount++;
        }
    }

    decimal average = CalculateAverage(sum, validCount);
    Console.WriteLine();
    Console.WriteLine($"Valid: {validCount}");
    Console.WriteLine($"Rejected: {rejectedCount}");
    Console.WriteLine($"Passed: {passedCount}");
    Console.WriteLine($"Average: {average}");
}
bool ValidateScore(string text, out int score, out string error)
{
    if (!int.TryParse(text, out score))
    {
        error = "Invalid integer";
        return false;
    }
    if (score < 0 || score > 100)
    {
        error = "Out of range";
        return false;
    }
    error = "";
    return true;
}
string ClassifyScore(int score, int passThreshold)
{
    return score switch
    {
        >= 90 => "Excellent",
        >= 70 => "Good",
        _ when score >= passThreshold => "Satisfactory",
        _ => "Fail"
    };
}
string Describe(string text, int passThreshold)
{
    if (!ValidateScore(text, out int score, out string error))
        return error;
    int finalScore = AddBonus(score);
    return $"{finalScore}: {ClassifyScore(finalScore, passThreshold)}";
}
string DescribeScore(int score, int passThreshold)
{
    return $"{score}: {ClassifyScore(score, passThreshold)}";
}
decimal ToDecimal(int value)
{
    return value;
}
decimal CalculateAverage(int sum, int count)
{
    if (count == 0)
        return 0;
    return ToDecimal(sum) / count;
}
decimal ToFraction(int score)
{
    return ToDecimal(score) / 100;
}
int AddBonus(int score)
{
    if (score > 80)
        return score + 2;
    return score;
}

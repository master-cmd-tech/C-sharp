using System;
string[] inputs = { "85", " 70 ", "49", "100", "0",
                    "abc", "", "72.5", "-1", "101" };
int sum = 0;
int validCount = 0;
foreach (string input in inputs)
    {
        if (ValidateScore(input, out int score, out string error))
        {
            sum += score;
            validCount++;
        }
    }
decimal average = CalculateAverage(sum, validCount);
Console.WriteLine($"Average: {average}");
Console.WriteLine($"85 as fraction : {ToFraction(85)}");
Console.WriteLine($"70 as fraction : {ToFraction(70)}");

bool ValidateScore(string text, out int score, out string error)
{
    if(!int.TryParse(text, out score))
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
decimal ToDecimal(int value)
{
    return value;
}
decimal CalculateAverage(int sum, int count)
{
    return ToDecimal(sum) / count;
}
decimal ToFraction(int score)
{
    return ToDecimal(score) / 100;
}

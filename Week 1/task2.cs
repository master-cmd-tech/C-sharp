using System;
string[] inputs = { "85", " 70 ", "49", "100", "0",
                    "abc", "", "72.5", "-1", "101" };
foreach (string input in inputs)
    Console.WriteLine(Describe(input, 50));
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
    if (!ValidateScore(text,out int score, out string error))
        return error;
    string label = ClassifyScore(score, passThreshold);
    return $"{score}: {label}";
}

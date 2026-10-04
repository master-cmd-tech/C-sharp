using System;
int passThreshold = 50;
string[] inputs = { "85", " 70 ", "49", "100", "0",
                    "abc", "", "72.5", "-1", "101" };
foreach (string input in inputs)
    Console.WriteLine(Describe(input));
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
string Describe(string text)
{
    if (!ValidateScore(text, out int score,out string error)) return error;
    string label;
    if (score >= 90)
        label = "Excellent";
    else if (score >= 70)
        label = "Good";
    else if (score >= passThreshold)
        label = "Satisfactory";
    else
        label = "Fail";
    return $"{score}: {label}";
}

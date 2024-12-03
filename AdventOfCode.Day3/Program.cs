using System.Text.RegularExpressions;

var input = File.ReadAllText("input");
var mull = ComputeMull(input);
Console.WriteLine($"Mull: {mull}");

var cleanedInput = Regex.Replace(input, @"don't\(\).*?do\(\)", "", RegexOptions.Singleline);
cleanedInput = Regex.Replace(cleanedInput, @"don't\(\).*$", "");
var mullCleaned = ComputeMull(cleanedInput);
Console.WriteLine($"Mull Cleaned: {mullCleaned}");

return;


static long ComputeMull(string input)
{
    const string pattern = @"mul\((\d+),(\d+)\)";
    var matches = Regex.Matches(input, pattern);
    return matches.Sum(match =>
    {
        var num1 = long.Parse(match.Groups[1].Value);
        var num2 = long.Parse(match.Groups[2].Value);
        return num1 * num2;
    });
}
using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

Console.WriteLine(PartOne(input));
Console.WriteLine(PartTwo(input));

return;

static int PartOne(string input) => 
    RegularExpressions.Mul.Matches(input)
    .Select(m => int.Parse(m.Groups["a"].Value) * int.Parse(m.Groups["b"].Value))
    .Sum();

static int PartTwo(string input) => 
    PartOne(RegularExpressions.BetweenDontAndDo.Replace(input, string.Empty));
    
internal class RegularExpressions
{
    public static Regex Mul { get; } = new(
        @"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)", 
        RegexOptions.Multiline | RegexOptions.Compiled);
    
    public static Regex BetweenDontAndDo { get; } = new(
        @"don't\(\).*?do\(\)|don't\(\).*", 
        RegexOptions.Singleline | RegexOptions.Compiled);
}
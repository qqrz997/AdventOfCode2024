var input = File.ReadAllLines("input.txt").Select(ParseLine).ToArray();

Console.WriteLine(PartOne(input));
Console.WriteLine(PartTwo(input));

return;

static int PartOne(int[][] lines) => lines
    .Count(IsSafe);

static int PartTwo(int[][] lines) => lines
    .Count(line => line.Where((_, i) => IsSafe(RemoveElementAt(line, i))).Any());

static int[] ParseLine(string line) =>
    line.Split(' ').Select(int.Parse).ToArray();

static int[] RemoveElementAt(int[] array, int i) => 
    [..array[..i], ..array[(i + 1)..]];

static bool IsSafe(int[] line)
{
    if (line[0] == line[1])
    {
        return false;
    }
    
    int previous = line[0];
    bool increasing = line[0] < line[1];
    
    foreach (var number in line.Skip(1))
    {
        if (Math.Abs(number - previous) is > 3 or < 1
            || (previous > number && increasing) 
            || (previous < number && !increasing))
        {
            return false;
        }
        previous = number;
    }
    
    return true;
}
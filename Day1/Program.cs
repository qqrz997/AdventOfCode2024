var input = File.ReadAllText("input.txt").AsSpan();

List<int> left = [];
List<int> right = [];

for (int i = 0; i < input.Length; i += 15)
{
    left.Add(int.Parse(input[i..(i+5)]));
    right.Add(int.Parse(input[(i+8)..(i+13)]));
}

left.Sort();
right.Sort();

Console.WriteLine(PartOne(left, right));
Console.WriteLine(PartTwo(left, right));

Console.ReadLine();
return;

static int PartTwo(List<int> left, List<int> right)
{
    var rightGroup = right
        .GroupBy(v => v)
        .ToDictionary(g => g.Key, g => g.Count());

    return left.Sum(i => i * rightGroup.GetValueOrDefault(i, 0));
}

static int PartOne(List<int> left, List<int> right) => left
    .Select((val, i) => Math.Abs(val - right[i]))
    .ToList()
    .Sum();
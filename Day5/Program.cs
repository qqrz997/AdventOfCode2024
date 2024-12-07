var input = File.ReadAllLines("input.txt");

var updates = input
    .Where(s => s.Contains(','))
    .Select(s => s.Split(',').Select(int.Parse).ToArray())
    .ToArray();

var rules = new Dictionary<int, List<int>>();

foreach ((int first, int second) in input
    .Where(s => s.Contains('|'))
    .Select(s => 
        (int.Parse($"{s[0]}{s[1]}"),
         int.Parse($"{s[3]}{s[4]}"))))
{
    if (rules.TryGetValue(first, out var value)) value.Add(second);
    else rules.Add(first, [second]);
}

Console.WriteLine(PartOne(updates, rules));

return;

static int PartOne(int[][] updates, Dictionary<int, List<int>> rules) => updates
    .Where(update =>
    {
        for (int i = 0; i < update.Length - 1; i++)
        {
            if (!rules[update[i]].Contains(update[i + 1])) return false;
        }
        return true;
    })
    .Select(arr => arr[arr.Length / 2])
    .Sum();
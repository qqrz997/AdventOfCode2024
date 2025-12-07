using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day7;

public class Day7 : IProblemSolution
{
    private enum Entity { Empty, Start, Beam, Splitter }
    
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();
        var grid = CreateGrid(input);
        var activeSplitters = FindActiveSplitters(grid);
        
        Console.WriteLine(activeSplitters.Count);
        Console.WriteLine(PartTwo(grid, activeSplitters));
    }

    private static long PartTwo(Entity[][] grid, List<(int x, int y)> activeSplitters)
    {
        var splitterValues = activeSplitters.ToDictionary(tuple => tuple, _ => 0L);

        for (int y = grid.Length - 2; y >= 0; y -= 2)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x] is Entity.Splitter && splitterValues.ContainsKey((x, y)))
                {
                    long left = FindBelowSplitterValue(x - 1, y);
                    long right = FindBelowSplitterValue(x + 1, y);
                    splitterValues[(x, y)] = left + right;
                }
            }
        }
        return splitterValues.Values.First();
        
        long FindBelowSplitterValue(int startX, int startY)
        {
            for (int y = startY; y < grid.Length; y++)
            {
                if (grid[y][startX] is Entity.Splitter) return splitterValues[(startX, y)];
            }
            return 1;
        }
    }

    private static Entity[][] CreateGrid(string[] input) => input
        .Select(str => str.Select(CharToEntity).ToArray())
        .ToArray();
    
    private static Entity CharToEntity(char c) => c switch
    {
        'S' => Entity.Start,
        '^' => Entity.Splitter,
        '|' => Entity.Beam,
        _ => Entity.Empty
    };

    private static List<(int x, int y)> FindActiveSplitters(Entity[][] grid)
    {
        var activeSplitters = new List<(int x, int y)>();
        grid[1][Array.IndexOf(grid.First(), Entity.Start)] = Entity.Beam;
        for (int y = 1; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x] is Entity.Empty && grid[y - 1][x] is Entity.Beam)
                {
                    grid[y][x] = Entity.Beam;
                }
                else if (grid[y][x] is Entity.Splitter && grid[y - 1][x] is Entity.Beam)
                {
                    activeSplitters.Add((x, y));
                    grid[y][x - 1] = Entity.Beam;
                    grid[y][x + 1] = Entity.Beam;
                }
            }
        }
        return activeSplitters;
    }
}
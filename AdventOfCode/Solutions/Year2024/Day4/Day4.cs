using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2024.Day4;

internal class Day4 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();

        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }
    private static int PartOne(string[] lines)
    {
        int count = 0;
        int yMax = lines.Length;
        int xMax = lines[0].Length;

        for (int y = 0; y < yMax; y++)
        {
            string line = lines[y];
            for (int x = 0; x < xMax; x++)
            {
                if (line[x] != 'X') continue;

                // Up
                if (y - 3 >= 0 
                    && lines[y - 1][x] == 'M' 
                    && lines[y - 2][x] == 'A'
                    && lines[y - 3][x] == 'S') count++;

                // Left
                if (x - 3 >= 0 
                    && lines[y][x - 1] == 'M'
                    && lines[y][x - 2] == 'A'
                    && lines[y][x - 3] == 'S') count++;

                // Right
                if (x + 3 < xMax
                    && lines[y][x + 1] == 'M'
                    && lines[y][x + 2] == 'A'
                    && lines[y][x + 3] == 'S') count++;

                // Down
                if (y + 3 < yMax
                    && lines[y + 1][x] == 'M' 
                    && lines[y + 2][x] == 'A' 
                    && lines[y + 3][x] == 'S') count++;

                // Up-Left
                if (y - 3 >= 0
                    && x - 3 >= 0
                    && lines[y - 1][x - 1] == 'M'
                    && lines[y - 2][x - 2] == 'A'
                    && lines[y - 3][x - 3] == 'S') count++;

                // Up-Right
                if (y - 3 >= 0
                    && x + 3 < xMax
                    && lines[y - 1][x + 1] == 'M'
                    && lines[y - 2][x + 2] == 'A' 
                    && lines[y - 3][x + 3] == 'S') count++;

                // Down-Left
                if (y + 3 < yMax
                    && x - 3 >= 0
                    && lines[y + 1][x - 1] == 'M' 
                    && lines[y + 2][x - 2] == 'A' 
                    && lines[y + 3][x - 3] == 'S') count++;

                // Down-Right
                if (y + 3 < yMax
                    && x + 3 < xMax
                    && lines[y + 1][x + 1] == 'M' 
                    && lines[y + 2][x + 2] == 'A' 
                    && lines[y + 3][x + 3] == 'S') count++;
            }
        }
        
        return count;
    }

    private static int PartTwo(string[] lines)
    {
        int count = 0;
        int yMax = lines.Length;
        int xMax = lines[0].Length;
        
        for (int y = 1; y < yMax - 1; y++)
        {
            string line = lines[y];
            for (int x = 1; x < xMax - 1; x++)
            {
                if (line[x] != 'A') continue;
                
                if (lines[y - 1][x - 1] == 'M'
                    && lines[y - 1][x + 1] == 'M'
                    && lines[y + 1][x + 1] == 'S'
                    && lines[y + 1][x - 1] == 'S')
                {
                    count++;
                }
                else if (lines[y - 1][x - 1] == 'S'
                    && lines[y - 1][x + 1] == 'M'
                    && lines[y + 1][x + 1] == 'M'
                    && lines[y + 1][x - 1] == 'S')
                {
                    count++;
                }
                else if (lines[y - 1][x - 1] == 'S'
                    && lines[y - 1][x + 1] == 'S'
                    && lines[y + 1][x + 1] == 'M'
                    && lines[y + 1][x - 1] == 'M')
                {
                    count++;
                }
                else if (lines[y - 1][x - 1] == 'M'
                    && lines[y - 1][x + 1] == 'S'
                    && lines[y + 1][x + 1] == 'S'
                    && lines[y + 1][x - 1] == 'M')
                {
                    count++;
                }
            }
        }
        
        return count;
    }
}
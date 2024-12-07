var input = File.ReadAllLines("input.txt");

Console.WriteLine(PartOne(input));

return;

static int PartOne(string[] lines)
{
    int count = 0;

    for (int y = 0; y < lines.Length; y++)
    {
        string line = lines[y];
        for (int x = 0; x < line.Length; x++)
        {
            var character = line[x];

            if (character != 'X') continue;

            // Up
            if (y - 1 >= 0)
            {
                if (lines[y - 1][x] == 'M')
                {
                    if (y - 2 >= 0)
                    {
                        if (lines[y - 2][x] == 'A')
                        {
                            if (y - 3 >= 0)
                            {
                                if (lines[y - 3][x] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Left
            if (x - 1 >= 0)
            {
                if (lines[y][x - 1] == 'M')
                {
                    if (x - 2 >= 0)
                    {
                        if (lines[y][x - 2] == 'A')
                        {
                            if (x - 3 >= 0)
                            {
                                if (lines[y][x - 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Right
            if (x + 1 < line.Length)
            {
                if (lines[y][x + 1] == 'M')
                {
                    if (x + 2 < line.Length)
                    {
                        if (lines[y][x + 2] == 'A')
                        {
                            if (x + 3 < line.Length)
                            {
                                if (lines[y][x + 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Down
            if (y + 1 < lines.Length)
            {
                if (lines[y + 1][x] == 'M')
                {
                    if (y + 2 < lines.Length)
                    {
                        if (lines[y + 2][x] == 'A')
                        {
                            if (y + 3 < lines.Length)
                            {
                                if (lines[y + 3][x] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Up-Left
            if (y - 1 >= 0 && x - 1 >= 0)
            {
                if (lines[y - 1][x - 1] == 'M')
                {
                    if (y - 2 >= 0 && x - 2 >= 0)
                    {
                        if (lines[y - 2][x - 2] == 'A')
                        {
                            if (y - 3 >= 0 && x - 3 >= 0)
                            {
                                if (lines[y - 3][x - 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Up-Right
            if (y - 1 >= 0 && x + 1 < line.Length)
            {
                if (lines[y - 1][x + 1] == 'M')
                {
                    if (y - 2 >= 0 && x + 2 < line.Length)
                    {
                        if (lines[y - 2][x + 2] == 'A')
                        {
                            if (y - 3 >= 0 && x + 3 < line.Length)
                            {
                                if (lines[y - 3][x + 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Down-Left
            if (y + 1 < lines.Length && x - 1 >= 0)
            {
                if (lines[y + 1][x - 1] == 'M')
                {
                    if (y + 2 < lines.Length && x - 2 >= 0)
                    {
                        if (lines[y + 2][x - 2] == 'A')
                        {
                            if (y + 3 < lines.Length && x - 3 >= 0)
                            {
                                if (lines[y + 3][x - 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Down-Right
            if (y + 1 < lines.Length && x + 1 < line.Length)
            {
                if (lines[y + 1][x + 1] == 'M')
                {
                    if (y + 2 < lines.Length && x + 2 < line.Length)
                    {
                        if (lines[y + 2][x + 2] == 'A')
                        {
                            if (y + 3 < lines.Length && x + 3 < line.Length)
                            {
                                if (lines[y + 3][x + 3] == 'S')
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    return count;
}
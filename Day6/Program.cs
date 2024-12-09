using System.Numerics;

var grid = File.ReadAllLines("input.txt")
    .Select(str => str.ToCharArray())
    .ToArray();

int guardX = 0;
int guardY = 0;

for (int y = 0; y < grid.Length; y++)
{
    for (int x = 0; x < grid[y].Length; x++)
    {
        if (grid[y][x] != '^') continue;
        guardX = x;
        guardY = y;
    }
}

Console.WriteLine(PartOne(grid, guardX, guardY));
Console.WriteLine(PartTwo(grid, guardX, guardY));

return;

static int PartOne(char[][] input, int guardX, int guardY) => Go(input, guardX, guardY);

static int PartTwo(char[][] grid, int guardX, int guardY)
{
    int count = 0;
    
    foreach (char[] row in grid)
    {
        for (int x = 0; x < row.Length; x++)
        {
            if (row[x] is '^' or '#') continue;
            char tempChar = row[x];
            row[x] = '#';
            if (Go(grid, guardX, guardY) == -1) count++;
            row[x] = tempChar;
        }
    }
    
    return count;
}

static int Go(char[][] grid, int guardX, int guardY)
{
    var currentDirection = Direction.Up;
    var visitedPositions = new Dictionary<Vector2, Direction>
    {
        { new(guardX, guardY), currentDirection }
    };

    while (!IsFacingOutside())
    {
        if (!IsFacingObstacle())
        {
            currentDirection = Rotate(currentDirection);
            continue;
        }
        
        MoveForward();
        var currentPos = new Vector2(guardX, guardY);

        if (visitedPositions.TryGetValue(currentPos, out var direction))
        {
            if (direction == currentDirection) return -1;

            visitedPositions[currentPos] = direction;
        }
        else
        {
            visitedPositions.Add(currentPos, currentDirection);
        }
    }
    
    return visitedPositions.Count;

    bool IsFacingOutside() => currentDirection switch
    {
        Direction.Up => guardY == 0,
        Direction.Down => guardY == grid.Length - 1,
        Direction.Left => guardX == 0,
        Direction.Right => guardX == grid[0].Length - 1,
        _ => throw new("A direction was unaccounted for")
    };
    
    bool IsFacingObstacle() => currentDirection switch
    {
        Direction.Up => grid[guardY - 1][guardX] != '#',
        Direction.Down => grid[guardY + 1][guardX] != '#',
        Direction.Left => grid[guardY][guardX - 1] != '#',
        Direction.Right => grid[guardY][guardX + 1] != '#',
        _ => throw new("A direction was unaccounted for")
    };

    void MoveForward()
    {
        switch (currentDirection)
        {
            case Direction.Up: guardY--; return;
            case Direction.Down: guardY++; return;
            case Direction.Left: guardX--; return;
            case Direction.Right: guardX++; return;
            default: throw new("A direction was unaccounted for");
        };
    }

    Direction Rotate(Direction direction) => direction switch
    {
        Direction.Up => Direction.Right,
        Direction.Right => Direction.Down,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        _ => throw new("A direction was unaccounted for")
    };
}

internal enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 4
}
var lines = File.ReadAllLines("input.txt");

char[,] grid = new char[lines.Length, lines[0].Length];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        grid[i, j] = lines[i][j];
    }
}
var starts = FindStart(grid);
Console.WriteLine($"Start positions: {string.Join(", ", starts)}");

BeamTravel(starts, grid);

void BeamTravel(Stack<(int, int)> starts, char[,] grid)
{
    (int, int) down = (1, 0);
    (int, int) downLeft = (1, -1);
    (int, int) downRight = (1, 1);              

    while (starts.Count > 0)
    {
        var start = starts.Pop();   
        int newX = start.Item1 + down.Item1;
        int newY = start.Item2 + down.Item2;

        // Check bounds
        if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
        {

            if (grid[newX, newY] == '.')
            {
                grid[newX, newY] = '|';
                starts.Push((newX, newY));
            }
            if (grid[newX, newY] == '^')
            {
                // Split left
                int downLeftX = newX + downLeft.Item1;
                int downLeftY = newY + downLeft.Item2;

                // Check bounds
                if (downLeftX >= 0 && downLeftX < grid.GetLength(0) && downLeftY >= 0 && downLeftY < grid.GetLength(1))
                {
                    grid[downLeftX, downLeftY] = '|';
                    starts.Push((downLeftX, downLeftY));
                }

                // Split right
                int downRightX = newX + downRight.Item1;
                int downRightY = newY + downRight.Item2;

                // Check bounds
                if (downRightX >= 0 && downRightX < grid.GetLength(0) && downRightY >= 0 && downRightY < grid.GetLength(1))
                {
                    grid[downRightX, downRightY] = '|';
                    starts.Push((downRightX, downRightY));
                }
            }
        }

        PrintCharGrid(grid);
    }

}

void PrintCharGrid(char[,] grid)
{
    for (int i = 0; i < grid.GetLength(0); i++)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            Console.Write(grid[i, j]);
        }
        Console.WriteLine();
    }
}

Stack<(int, int)> FindStart(char[,] grid)
{
    for (int i = 0; i < grid.GetLength(0); i++)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            if (grid[i, j] == 'S') // Assuming 'S' marks the start
            {
                return new Stack<(int, int)>(new List<(int, int)> { (i, j) });
            }
        }
    }
    return new Stack<(int, int)>(); // Return an empty stack if start not found
}
var lines = File.ReadAllLines("input_real.txt");

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

var answer = BeamTravel(starts, grid);

PrintCharGrid(grid);
Console.WriteLine($"The number of beam splits is: {answer}");

var startsForPaths = FindStart(grid);
if (startsForPaths.Count > 0)
{
    var startPos = startsForPaths.Pop();
    grid[startPos.Item1, startPos.Item2] = '|';
    var totalPaths = MemoizedDFS(grid, startPos, new Dictionary<(int, int), long>());
    Console.WriteLine($"The number of unique paths is: {totalPaths}");
}

int BeamTravel(Stack<(int, int)> starts, char[,] grid)
{
    int answer = 0;
    (int, int) down = (1, 0);
    (int, int) left = (0, -1);
    (int, int) right = (0, 1);              

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
                
                answer++;
                // Split left
                int downLeftX = newX + left.Item1;
                int downLeftY = newY + left.Item2;

                // Check bounds
                if (downLeftX >= 0 && downLeftX < grid.GetLength(0) && downLeftY >= 0 && downLeftY < grid.GetLength(1))
                {

                    grid[downLeftX, downLeftY] = '|';
                    starts.Push((downLeftX, downLeftY));
                }

                // Split right
                int downRightX = newX + right.Item1;
                int downRightY = newY + right.Item2;

                // Check bounds
                if (downRightX >= 0 && downRightX < grid.GetLength(0) && downRightY >= 0 && downRightY < grid.GetLength(1))
                {
                    grid[downRightX, downRightY] = '|';
                    starts.Push((downRightX, downRightY));
                }

            
            }
        }
    }

    return answer;

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

long MemoizedDFS(char[,] grid, (int, int) pos, Dictionary<(int, int), long> memo)
{
    int x = pos.Item1;
    int y = pos.Item2;
    if (x == grid.GetLength(0) - 1)
        return 1;
    if (memo.TryGetValue(pos, out var cached))
        return cached;
    long total = 0;
    if (grid[x, y] == '|')
    {
        int downX = x + 1;
        int downY = y;
        if (downX >= 0 && downX < grid.GetLength(0) && downY >= 0 && downY < grid.GetLength(1))
        {
            if (grid[downX, downY] == '|')
            {
                total += MemoizedDFS(grid, (downX, downY), memo);
            }
            else if (grid[downX, downY] == '^')
            {
                // Split left
                int leftX = downX;
                int leftY = downY - 1;
                if (leftX >= 0 && leftX < grid.GetLength(0) && leftY >= 0 && leftY < grid.GetLength(1))
                {
                    if (grid[leftX, leftY] == '|')
                    {
                        total += MemoizedDFS(grid, (leftX, leftY), memo);
                    }
                }
                // Split right
                int rightX = downX;
                int rightY = downY + 1;
                if (rightX >= 0 && rightX < grid.GetLength(0) && rightY >= 0 && rightY < grid.GetLength(1))
                {
                    if (grid[rightX, rightY] == '|')
                    {
                        total += MemoizedDFS(grid, (rightX, rightY), memo);
                    }
                }
                // No split continue down
                int downX2 = downX + 1;
                int downY2 = downY;
                if (downX2 >= 0 && downX2 < grid.GetLength(0) && downY2 >= 0 && downY2 < grid.GetLength(1))
                {
                    if (grid[downX2, downY2] == '|')
                    {
                        total += MemoizedDFS(grid, (downX2, downY2), memo);
                    }
                }
            }
        }
    }
    memo[pos] = total;
    return total;
}
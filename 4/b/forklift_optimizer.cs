var lines = File.ReadAllLines("input.txt");
const int ACCESSIBLE_LIMIT = 4;
int totalRollsRemoved = 0;

char[,] floor = BuildFloorArray(lines);

int accessibleRolls;
do
{
    accessibleRolls = 0;

    for (int i = 0; i < floor.GetLength(0); i++)
    {
        for (int j = 0; j < floor.GetLength(1); j++)
        {
            if (floor[i, j] == '@') 
            {
                int rollsAround = CalcRollsAroundLocation(floor, (i, j));
                if (rollsAround < ACCESSIBLE_LIMIT)
                {
                    floor[i, j] = '.';
                    totalRollsRemoved++;
                    accessibleRolls++;
                }
            }
        }
    }
}
while (accessibleRolls > 0);

Console.WriteLine($"The number of removed forklift rolls is: {totalRollsRemoved}");

int CalcRollsAroundLocation(char[,] floor, (int, int) loc)
{
    int rollsAround = 0;
    var directions = new (int, int)[]
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),  /*loc*/  (0, 1),   
        (1, -1),  (1, 0),  (1, 1)
    };

    foreach (var (dx, dy) in directions)
    {
        int newX = loc.Item1 + dx;
        int newY = loc.Item2 + dy;

        // Check bounds
        if (newX >= 0 && newX < floor.GetLength(0) && newY >= 0 && newY < floor.GetLength(1))
        {
            if (floor[newX, newY] == '@')
            {
                rollsAround++;
            }
        }
    }

    return rollsAround;
}

char[,] BuildFloorArray(string[] lines)
{
    int rows = lines.Length;
    int cols = lines[0].Length;
    char[,] floor = new char[rows, cols];
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            floor[i, j] = lines[i][j];
        }   
    }

    return floor;
}
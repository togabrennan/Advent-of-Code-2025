var lines = File.ReadAllLines("input.txt");

string[][] grid = new string[lines.Length][];

for (int i = 0; i < lines.Length; i++)
{
    grid[i] = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
}

long answer = 0;

Console.WriteLine($"Rows: {grid.Length}, Cols: {grid[0].Length}");


for (int col = 0; col < grid[0].Length; col++)
{
    int operands = grid.Length - 1;
    string op = grid[grid.Length - 1][col];

    Console.WriteLine($"Operands: {operands}, Operator: {op}");
    Queue<string> numbers = new Queue<string>();
    for (int i = 0; i < operands; i++)
    {
        numbers.Enqueue(grid[i][col]);
    }

    long temp = 0;
    switch (op)
    {
        case "*":
            temp = long.Parse(numbers.Dequeue());
            while (numbers.Count > 0)
                temp *= long.Parse(numbers.Dequeue());
            answer += temp;
            break;
        case "+":
            temp = long.Parse(numbers.Dequeue());
            while (numbers.Count > 0)
                temp += long.Parse(numbers.Dequeue());
            answer += temp;
            break;
    }
}

Console.WriteLine($"Final answer: {answer}");
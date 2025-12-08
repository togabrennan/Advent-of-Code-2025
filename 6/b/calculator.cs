var lines = File.ReadAllLines("input.txt");
string[] ops = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
Stack<string> operations = new Stack<string>(ops);
foreach (var op in ops)
{
    operations.Push(op);
}

char[][] grid = new char[lines.Length][];

for (int i = 0; i < lines.Length - 1; i++)
{
    lines[i] = lines[i].Replace("\n", "").Replace("\r", "");
    grid[i] = lines[i].ToArray();
}
ulong answer = 0;

Queue<ulong> operands = new Queue<ulong>();
for (int i = lines[0].Length - 1; i >= 0; i--)
{
    var mostSignificantDigit = lines[0][i];
    var secondDigit = lines[1][i];
    var thirdDigit = lines[2][i];
    var leastSignificantDigit = lines[3][i];

    if (mostSignificantDigit == ' ' && secondDigit == ' ' && thirdDigit == ' ' && leastSignificantDigit == ' ')
    {
        // time to do math
        string op = operations.Pop();

        switch (op)
        {
            case "+":
                {
                    ulong temp = operands.Dequeue();
                    while (operands.Count > 0)
                    {
                        temp += operands.Dequeue();
                    }
                    answer += temp;
                    Console.WriteLine("Adding result: " + temp);
                    break;
                }
            case "*":
                {
                    ulong temp = operands.Dequeue();
                    while (operands.Count > 0)
                    {
                        temp *= operands.Dequeue();
                    }
                    answer += temp;
                    Console.WriteLine("* result: " + temp);
                    break;
                }
        }
        
        continue;
    }
    
    string output = $"{mostSignificantDigit}{secondDigit}{thirdDigit}{leastSignificantDigit}";
    operands.Enqueue(ulong.Parse(output));

    Console.WriteLine(output);
    
}
Console.WriteLine($"Final Answer: {answer}");

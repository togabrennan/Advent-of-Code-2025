using System;
using System.IO;

Main();

// Correct answer 969.

void Main()
{
    var inputs = ParseInput("input.txt");
    var rotaryLock = PopulateRotaryLock();

    var result = CalculatePassword(rotaryLock, inputs);
    Console.WriteLine($"The generated password is: {result}");
}

int CalculatePassword(int[] rotaryLock, string[] inputs)
{
    int password = 0;
    var pointer = 50;
    Console.WriteLine(rotaryLock.Length);
    foreach (var input in inputs)
    {
        var direction = input.Substring(0, 1);
        int steps = int.Parse(input.Substring(1, input.Length - 1));
        
        switch (direction)
        {
            case "R":
                pointer = (pointer + steps) % 100;
                break;
            case "L":
                pointer = (pointer - steps + 100) % 100;
                break;
            default:
                throw new InvalidDataException("Invalid direction in input.");
        }
        if (pointer == 0)
            password++;
    }
    return password;
}

int[] PopulateRotaryLock()
{
    var rotaryLock = new int[100];
    for (int i = 0; i < 100; i++)
    {
        rotaryLock[i] = i;
    }
    return rotaryLock;
}

string[] ParseInput(string path)
{
    return System.IO.File.ReadAllLines(path);
}

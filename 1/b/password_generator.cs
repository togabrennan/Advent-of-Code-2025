var inputs = File.ReadAllLines("input.txt");
var result = CalculatePassword(inputs);

Console.WriteLine($"The generated password is: {result}");

int CalculatePassword(string[] inputs)
{
    int password = 0;
    var pointer = 50;
    foreach (var input in inputs)
    {
        var direction = input.Substring(0, 1);
        int clicks = int.Parse(input.Substring(1, input.Length - 1));
        int laps = 0;

        switch (direction)
        {
            case "R":
                var start = pointer;
                laps = (start + clicks) / 100; // counts zero hits including landing at 0
                pointer = (start + clicks) % 100;
                break;
            case "L":
                start = pointer;
                if (start == 0)
                {
                    laps = clicks / 100; // starting at 0: only every full 100 steps hits 0
                }
                else
                {
                    if (clicks < start)
                    {
                        laps = 0;
                    }
                    else
                    {
                        laps = 1 + ((clicks - start) / 100);
                    }
                }
                pointer = (start - (clicks % 100) + 100) % 100; 
                break;
            default:
                throw new InvalidDataException("Invalid direction in input.");
        }
        password += laps;
    }
    return password;
}
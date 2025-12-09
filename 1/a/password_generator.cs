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
        int steps = int.Parse(input.Substring(1, input.Length - 1));
        
        pointer = direction switch
        {
            "R" => (pointer + steps) % 100,
            "L" => (pointer - steps + 100) % 100,
            _ => throw new InvalidDataException("Invalid direction in input.")
        };

        if (pointer == 0)
            password++;
    }
    return password;
}
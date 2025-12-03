var input = File.ReadAllText("input.txt");
string[] ranges = input.Split(',');

ulong result = 0;

foreach (var range in ranges)
{
    ulong lowerBound = ulong.Parse(range.Split('-')[0]);
    ulong upperBound = ulong.Parse(range.Split('-')[1]);

    for (ulong i = lowerBound; i <= upperBound; i++)
    {
        result += DetectDuplication(i);
    }
}

Console.WriteLine($"The sum of invalid ids is: {result}");

ulong DetectDuplication(ulong input)
{
    string inputStr = input.ToString();

    if (inputStr.Length % 2 != 0)
    {
        return 0;
    }

    string firstHalf = inputStr.Substring(0, inputStr.Length / 2).ToString();
    string secondHalf = inputStr.Substring((inputStr.Length + 1) / 2).ToString();

    Console.WriteLine($"{firstHalf} - {secondHalf} - { firstHalf == secondHalf}");
    if (firstHalf == secondHalf)
        return input;

    return 0;
}
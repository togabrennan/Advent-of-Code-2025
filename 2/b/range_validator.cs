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
    ulong result = 0; 
    string inputStr = input.ToString();

    if (string.IsNullOrEmpty(inputStr))
    {
        return 0;
    }

    int n = inputStr.Length;

    // NAIVE APPROACH ALERT but it works :/ 
    // Check if the input number is made by repeating a substring multiple times.
    // For each possible substring length, see if repeating it reconstructs the original number.
    for (int len = 1; len <= n / 2; len++)
    {
        if (n % len == 0)
        {
            string substring = inputStr.Substring(0, len);
            string repeated = string.Concat(Enumerable.Repeat(substring, n / len));
            if (repeated == inputStr)
            {
                result = input;
            }
        }
    }

    return result;
}
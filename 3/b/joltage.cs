var batteries = File.ReadAllLines("input.txt");

ulong joltage = 0;

foreach (var bank in batteries)
{
    joltage += ulong.Parse(FindMaxJoltage(bank));
}

Console.WriteLine($"The max joltage is: {joltage}");


string FindMaxJoltage(string bank)
{
    int maxDigits = 12; 

    while (bank.Length > maxDigits)
    {
        bool removed = false;
        for (int i = 0; i < bank.Length - 1; i++)
        {
            if (bank[i] < bank[i + 1])
            {
                bank = bank.Remove(i, 1);
                removed = true;
                break;
            }
        }
        // If no digit was removed (all digits are non-increasing), remove the last digit
        if (!removed)
        {
            bank = bank.Remove(bank.Length - 1, 1);
        }
    }
    
    return bank;
}
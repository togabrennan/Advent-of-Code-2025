var batteries = File.ReadAllLines("input.txt");

var joltage = 0;

foreach (var bank in batteries)
{
    joltage += FindMaxJoltage(bank);
}

Console.WriteLine($"The max joltage is: {joltage}");


int FindMaxJoltage(string bank)
{
    int maxJoltage = 0;
   
    for (int i = 0; i < bank.Length; i++)
    {
        for (int j = 0; j < bank.Length; j++)
        {
            if (i != j && i < j)
            {
                int voltage = int.Parse($"{bank[i]}{bank[j]}");
                if (voltage > maxJoltage)
                {
                    maxJoltage = voltage;
                }
            }
        }
    }
    return maxJoltage;
}
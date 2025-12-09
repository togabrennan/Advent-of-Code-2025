var lines = File.ReadAllLines("input.txt");

var ranges = new  List<string>();
var foods = new List<string>();

foreach (var l in lines)
{
    if (l.Contains('-'))
    {
        ranges.Add(l);
    }
    else if (!string.IsNullOrWhiteSpace(l))
    {
        foods.Add(l);
    }    
}

long freshFoodCount = 0;

foreach (var food in foods)
{
    long foodId = long.Parse(food);

    foreach (var range in ranges)
    {
        var parts = range.Split('-');
        long start = long.Parse(parts[0]);
        long end = long.Parse(parts[1]);

        if (foodId >= start && foodId <= end)
        {
            freshFoodCount++;
            break;
        }
    }
}

Console.WriteLine($"The number of fresh foods is: {freshFoodCount}");


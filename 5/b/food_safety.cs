var lines = File.ReadAllLines("input.txt");
List<(long, long)> ranges = new  List<(long, long)>();
foreach (var line in lines)
{
    var parts = line.Split('-');
    long start = long.Parse(parts[0]);
    long end = long.Parse(parts[1]);
    ranges.Add((start, end));
}

// Ranges are not in order, so sort them first
ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));
ranges = MergeRanges(ranges);

long result = 0;
foreach (var range in ranges)
{
    result += (long)(range.Item2 - range.Item1 + 1);
}
Console.WriteLine($"The total number of fresh ingredient IDs is: {result}");

// Merge overlapping ranges
List<(long, long)> MergeRanges(List<(long, long)> ranges)
{

    List<(long, long)> merged = new List<(long, long)>();
    foreach (var range in ranges)
    {
        if (merged.Count == 0 || merged.Last().Item2 < range.Item1 - 1)
        {
            merged.Add(range);
        }
        else
        {
            merged[merged.Count - 1] = (merged.Last().Item1, Math.Max(merged.Last().Item2, range.Item2));
        }
    }
    return merged;
}

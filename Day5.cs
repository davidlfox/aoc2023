using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

public static class Day5
{
    public static void Run()
    {
        var lines = File.ReadAllLines("./day5in.txt");
        long[] seeds = Regex.Replace(lines[0], @"seeds: ", string.Empty).Split(' ').Select(long.Parse).ToArray();
        // sequential list of mappings
        var mappings = new List<RangeMapper>();
        RangeMapper mapping = null;
        foreach(var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Contains("map"))
            {
                if (mapping != null) mappings.Add(mapping);
                mapping = new RangeMapper();
                continue;
            }

            var parts = line.Split(' ').Select(long.Parse).ToArray();
            mapping.AddMapping(
                parts[1], parts[1] + parts[2] - 1, 
                parts[0], parts[0] + parts[2] - 1);
        }
        mappings.Add(mapping);

        Console.WriteLine($"debug: {(seeds, mappings)}");

        long lowest = long.MaxValue;
        long? mappedIndex;
        foreach(long seed in seeds)
        {
            mappedIndex = seed;
            for(int i = 0;i < mappings.Count;i++)
            {
                var mapped = mappings[i].GetMapping(mappedIndex.Value);
                if (mapped != null)
                {
                    mappedIndex = mapped.Value;
                }

                // Console.WriteLine($"debug: {seed} -> {mappedIndex}");

                // finally, if its the last mapping
                if (i == mappings.Count - 1)
                {
                    if (mappedIndex < lowest)
                    {
                        // check and potentially set lowest
                        lowest = (long)mappedIndex;
                    }
                }
            }
            // Console.WriteLine($"after: {seed} -> {mappedIndex} [{lowest}]");
        }

        Console.WriteLine($"Part 1: {lowest}");
    }

    class RangeMapping
    {
        public long SourceStart { get; set; }
        public long SourceEnd { get; set; }
        public long TargetStart { get; set; }
        public long TargetEnd { get; set; }

        public RangeMapping(long sourceStart, long sourceEnd, long targetStart, long targetEnd)
        {
            SourceStart = sourceStart;
            SourceEnd = sourceEnd;
            TargetStart = targetStart;
            TargetEnd = targetEnd;
        }
    }

    class RangeMapper
    {
        private List<RangeMapping> mappings = new List<RangeMapping>();

        public void AddMapping(long sourceStart, long sourceEnd, long targetStart, long targetEnd)
        {
            mappings.Add(new RangeMapping(sourceStart, sourceEnd, targetStart, targetEnd));
            mappings = mappings.OrderBy(m => m.SourceStart).ToList(); // Keep the list sorted
        }

        public long? GetMapping(long key)
        {
            int index = mappings.BinarySearch(new RangeMapping(key, key, 0, 0), new RangeComparer());
            if (index < 0)
                return null; // Key not in any range

            var mapping = mappings[index];
            return mapping.TargetStart + (key - mapping.SourceStart);
        }
    }

    class RangeComparer : IComparer<RangeMapping>
    {
        public int Compare(RangeMapping x, RangeMapping y)
        {
            if (x.SourceStart <= y.SourceStart && x.SourceEnd >= y.SourceStart)
                return 0; // Overlapping or adjacent ranges
            return x.SourceStart.CompareTo(y.SourceStart);
        }
    }

}
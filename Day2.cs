using System.Text.RegularExpressions;

public static partial class Day2
{
    const int redMax = 12;
    const int greenMax = 13;
    const int blueMax = 14;

    public static void Run()
    {
        List<int> gamePowers = [];
        foreach(var game in File.ReadAllLines("./day2in.txt"))
        {
            int gameNum = int.Parse(GameNum().Match(game).Groups[1].Value);
            var pulls = Pull().Matches(game);
            Dictionary<string, int> colors = [];
            foreach(Match pull in pulls)
            {
                int count = int.Parse(pull.Groups[2].Value);
                string color = pull.Groups[3].Value;
                if (colors.TryGetValue(color, out int value))
                {
                    if (count > colors[color])
                    {
                        colors[color] = count;
                    }
                }
                else
                {
                    colors[color] = count;
                }
            }
            // write out the game number and the colors from the colors dictionary
            Console.WriteLine($"Game {gameNum}: {string.Join(", ", colors.Select(kvp => $"{kvp.Value} {kvp.Key}"))}");
            gamePowers.Add(colors.Values.Aggregate(1, (acc, val) => acc * val));
        }
        Console.WriteLine($"Part 2: {gamePowers.Sum()}");
        // 3483 is too low, because i was dumb and getting min
        // changing the check to max fixes it and yields: 78669
    }

    [GeneratedRegex(@"Game (\d+): ")]
    private static partial Regex GameNum();
    [GeneratedRegex(@"Game \d+: ")]
    private static partial Regex RemovePrefix();
    [GeneratedRegex(@"((\d+) (red|green|blue))+")]
    private static partial Regex Pull();
}
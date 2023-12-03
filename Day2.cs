using System.Text.RegularExpressions;

public static partial class Day2
{
    const int redMax = 12;
    const int greenMax = 13;
    const int blueMax = 14;

    public static void Run()
    {
        List<int> possibleGames = [];
        for(int i = 1; i <= 100; i++)
        {
            possibleGames.Add(i);
        }
        foreach(var game in File.ReadAllLines("./day2in.txt"))
        {
            int gameNum = int.Parse(GameNum().Match(game).Groups[1].Value);
            string[] pulls = RemovePrefix().Replace(game, string.Empty).Split(';');
            foreach(var pull in pulls)
            {
                var matches = Pull().Matches(pull);
                foreach(Match match in matches)
                {
                    int count = int.Parse(match.Groups[2].Value);
                    string color = match.Groups[3].Value;
                    if((color == "red" && count > redMax)
                        || (color == "blue" && count > blueMax)
                        || (color == "green" && count > greenMax))
                        {
                            possibleGames.Remove(gameNum);
                        }
                }
            }
        }
        Console.WriteLine($"Possible games: {string.Join(", ", possibleGames)}");
        Console.WriteLine($"Sum: {possibleGames.Sum()}");
    }

    [GeneratedRegex(@"Game (\d+): ")]
    private static partial Regex GameNum();
    [GeneratedRegex(@"Game \d+: ")]
    private static partial Regex RemovePrefix();
    [GeneratedRegex(@"((\d+) (red|green|blue))+")]
    private static partial Regex Pull();
}
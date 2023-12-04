using System.Text.RegularExpressions;

public class Day4
{
    public static void Run()
    {
        int points = 0;

        foreach(var line in File.ReadAllLines("./day4in.txt"))
        {
            var matches = Regex.Matches(line, @"Card\s+(\d+):\s+((\d+\s+)+)\|\s+((\d+\s*)+)");
            int card = int.Parse(matches[0].Groups[1].Value);
            int[] wins = matches[0].Groups[2].Value.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();
            int[] mine = matches[0].Groups[4].Value.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToArray();
            // Console.WriteLine(matches);
            IEnumerable<int> winningNums = wins.Intersect(mine);
            if (winningNums.Count() > 0)
            {
                int i = 0;
                int score = 1;
                while(i < winningNums.Count()-1)
                {
                    score *= 2;
                    i++;
                }
                points += score;

            }
        }

        Console.WriteLine($"Points: {points}");
    }
}
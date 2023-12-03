using System.Text.RegularExpressions;

public static class Day1
{
    static readonly Dictionary<string, string> mappings = new Dictionary<string, string>()
    {
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"},
        {"1", "1"},
        {"2", "2"},
        {"3", "3"},
        {"4", "4"},
        {"5", "5"},
        {"6", "6"},
        {"7", "7"},
        {"8", "8"},
        {"9", "9"},
    };

    // this gave me 54412, answer is 54418 (https://github.com/RieBi/AdventOfCode/blob/master/Year2023/Day1.cs)
    public static void Run()
    {
        var lines = File.ReadAllLines("./day1in.txt");
        int total = 0;
        int count = 0;
        string regex = @"(one|two|three|four|five|six|seven|eight|nine|\d)"; // gives 54412
        regex = @"(?=(one|two|three|four|five|six|seven|eight|nine|\d))"; // gives 54418 wtf
        foreach(var line in lines)
        {
            Console.WriteLine($"Line: {line}");
            // Console.WriteLine($"Regex: {regex}");
            var matches = Regex.Matches(line, regex);
            // Console.WriteLine(matches[0].ToString());
            // string firstNum = matches[0].ToString();
            string firstNum = mappings[matches.First().Groups[1].Value];
            // string lastNum = matches[^1].ToString();
            string lastNum = mappings[matches.Last().Groups[1].Value];
            string theNum = firstNum + lastNum;
            if(theNum.Length != 2) throw new NotImplementedException();
            
            Console.WriteLine($"First: {firstNum}, Last: {lastNum}, Num: {theNum}");
            
            total += int.Parse(theNum);
            count++;
        }

        Console.WriteLine($"Total: {total, 10}, Count: {count, 10}");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day3
{
    internal class Day3_2 : IAssigment
    {
        public int Run()
        {
            string example = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            string test = "don't()do()";
            string input = File.ReadAllText("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day3\\input.txt");

            Regex mulRegex = new Regex("mul\\(\\d{1,3},\\d{1,3}\\)", RegexOptions.IgnoreCase);
            var mulMatches = mulRegex.Matches(input);

            Regex dontRegex = new Regex(@"(do|don\'t)\(\)");
            var dontMatches = dontRegex.Matches(input);
            Console.WriteLine(dontMatches.Count);

            Regex combinedRegex = new Regex(@"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\))", RegexOptions.IgnoreCase);
            var combinedMatches = combinedRegex.Matches(input);
            int sum = 0;
            bool active = true;
            foreach(Match match in combinedMatches)
            {
                if (match.Value == "don't()") active = false;
                else if (match.Value == "do()") active = true;
                else
                {
                    if (active)
                    {
                        Regex newRegex = new Regex("\\d{1,3}");
                        var numbers = newRegex.Matches(match.ToString()).Select(x => int.Parse(x.ToString())).ToList();
                        sum += numbers[0] * numbers[1];
                    }
                }
            }
            return sum;
        }
    }
}

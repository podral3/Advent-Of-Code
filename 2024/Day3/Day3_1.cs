using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day3
{
    public class Day3_1 : IAssigment
    {
        public int Run()
        {
            string example = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
            string test = "mul(123,123)";
            string input = File.ReadAllText("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day3\\input.txt");

            string pattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
            Regex regex = new Regex("mul\\(\\d{1,3},\\d{1,3}\\)", RegexOptions.IgnoreCase);
            var matches = regex.Matches(input);

            int sum = 0;
            foreach(var match in matches)
            {
                Regex newRegex = new Regex("\\d{1,3}");
                var numbers = newRegex.Matches(match.ToString()).Select(x => int.Parse(x.ToString())).ToList();
                sum += numbers[0] * numbers[1];
            }

            return sum;
        }
    }
}

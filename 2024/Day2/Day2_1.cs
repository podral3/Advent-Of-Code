using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day2
{
    public class Day2_1 : IAssigment
    {
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day2\\test.txt");

            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int[] splitted = lines[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                if (IsSafe(splitted)) sum++;
                

            }
            return sum ;
        }

        //My own not very clever solution.
        public bool IsSafe(int[] numbers)
        {
            Trend trend = Trend.idk;
            for (int i = 0; i < numbers.Length-1; i++)
            {
                int curr = numbers[i]; int next = numbers[i + 1];
                if (next > curr && Math.Abs(curr - next) <= 3)
                {
                    if (trend == Trend.decreasing) { return false; }
                    trend = Trend.increasing;
                    continue;
                }
                if (next < curr && Math.Abs(curr - next) <= 3)
                {
                    if(trend == Trend.increasing) return false;
                    trend = Trend.decreasing;
                    continue;
                }
                return false;
            }
            return true;
        }
    }

    enum Trend { increasing, decreasing, idk};
}

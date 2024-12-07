using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent6_2.Day7
{
    public class Day7_1and2
    {
        public long Run()
        {
            long sum = 0;
            string[] lines = File.ReadAllLines("D:\\Repos\\Advent6_2\\Advent6_2\\Day7\\input.txt");
            foreach (string line in lines)
            {
                string[] splitted = line.Split(':');
                long result = long.Parse(splitted[0]) ;

                splitted = line.Split(" ");
                long[] numbers = new long[splitted.Length-1];
                for (int i = 1; i < splitted.Length; i++)
                {
                    numbers[i-1] = long.Parse(splitted[i]);
                }
                if (Solvable(numbers[0], numbers, result, 0)) sum+= result;
            }
            return sum;
        }

        private bool Solvable(long curr, long[] items, long result, int i)
        {
            if (i + 1 >= items.Length) return curr == result;
            else
            {
                i++;
                return (Solvable(curr + items[i], items, result, i) ||
                    Solvable(curr * items[i], items, result, i)) ||
                    Solvable(CombineNumbers(curr, items[i]), items, result, i); //remove this line to solve part 1
            }
        }
        private long CombineNumbers(long x, long y)
        {
            string sXY = $"{x}{y}";
            return long.Parse(sXY) ;
        }
    }
}

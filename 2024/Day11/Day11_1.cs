using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day11
{
    public class Day11_1
    {
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
        Dictionary<long, long[]> longDict = new Dictionary<long, long[]>();
        public long Run()
        {
            
            Stopwatch stopwatch = new Stopwatch();
            dict.Add("0", new List<string>() { "1" });

            // Start measuring time
            stopwatch.Start();
            List<string> input = "41078 18 7 0 4785508 535256 8154 447".Split(' ').ToList();
            //input = new List<string>() {"125", "17"};
            List<long> list = input.Select(e => long.Parse(e)).ToList();
            List<long> output = MyRecursion(list, 1);
            stopwatch.Stop();

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // Print the elapsed time in milliseconds
            Console.WriteLine("Execution Time: " + elapsedMilliseconds + " ms");
            return output.Count;
        }

        private List<long> MyRecursion(List<long> list, int iteration)
        {
            if (iteration == 26)
                return list;
            else
            {
                List<long> newList = new List<long>();
                foreach (long item in list)
                {
                    if(HandleStone1(item, out long result))
                    {
                        newList.Add(result);
                    }
                    else
                    {
                        var x = DivideLong(item);
                        newList.AddRange(new List<long>() { x.Item1, x.Item2});
                    }
                }
                return MyRecursion(newList, iteration+1);
            }
        }
        private bool HandleStone1(long number, out long result)
        {
            if (number == 0) { result = 1; return true; }
            if(number == 1) { result = 2024; return true; }
            int digitCount = CountDigits(number);
            if((digitCount % 2 != 0) || number > 0 && number < 10)
            {
                result = number * 2024; return true;
            }
            result = -1;
            return false;
        }
        private List<string> HandleStone(string stone)
        {
            long stoneIne = long.Parse(stone);
            if (stoneIne == 0)
            {
                return dict[stone];
            }
            else if (stone == "1") return new List<string>() { "2024" };
            else if ((stone.Length % 2 != 0) || stoneIne > 0 && stoneIne < 10)
            {
                string stone2024String = (stoneIne * 2024).ToString();
                return new List<string>() { stone2024String };
            }
            else
            {
                return Divide(stone);
            }

        }
        private List<string> Divide(string input)
        {
            if (this.dict.ContainsKey(input))
            {
                return this.dict[input];
            }
            int mid = input.Length / 2;
            string firstPart = input.Substring(0, mid);
            string secondPart = input.Substring(mid);
            var result = new List<string>() { int.Parse(firstPart).ToString(), int.Parse(secondPart).ToString() };
            dict.Add(input, result);
            return result;
        }

        private (long,long) DivideLong(long number)
        {
            long numDigits = (int)Math.Floor(Math.Log10(number) + 1);

            // Step 2: Calculate the split point
            long splitPoint = numDigits / 2;

            // Step 3: Calculate the divisor for splitting
            long divisor = (long)Math.Pow(10, numDigits - splitPoint);

            // Step 4: Get the two parts
            long part1 = number / divisor; // First part
            long part2 = number % divisor; // Second part

            return (part1, part2);
        }
        private int CountDigits(long num)
        {
            // Handle the case for zero
            if (num == 0) return 1;

            // Handle negative numbers
            num = Math.Abs(num);

            int count = 0;
            while (num > 0)
            {
                num /= 10; // Divide by 10
                count++;   // Increment count
            }

            return count;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day11
{
    public class Day11_2
    {
        Dictionary<long,long> dict = new Dictionary<long,long>();
        public long Run()
        {
            long count = 0;
            List<string> input = "41078 18 7 0 4785508 535256 8154 447".Split(' ').ToList();
            //input = new List<string>() {"125", "17"};
            List<long> list = input.Select(e => long.Parse(e)).ToList();
            Dictionary<long, long> numberCounts = input
            .Select(num => long.Parse(num)) // Convert each string to long
            .GroupBy(num => num) // Group by each number
            .ToDictionary(group => group.Key, group => (long)group.Count());
            //foreach (long num in list)
            //{
            //    if (HandleStone1(num))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        DivideLong(num);
            //    }
            //}
            var dic = MyRecursion(numberCounts, 1);
            foreach(var kvp in dic)
            {
                count += kvp.Value;
            }
            return count;
        }

        private bool HandleStone1(long number, out long result)
        {
            result = -1; 
            if (number == 0) { result = 1; }
            if (number == 1) { result = 2024; }
            int digitCount = CountDigits(number);
            if ((digitCount % 2 != 0) || number > 0 && number < 10)
            {
                result = number * 2024;
            }
            if (result == -1) 
                return false;
            return true;
        }
        private Dictionary<long,long> MyRecursion(Dictionary<long,long>dic, int iteration)
        {
            if (iteration == 76)
                return dic;
            else
            {
                var shallowCopy = new Dictionary<long, long>();
                foreach (var item in dic)
                {
                    if (HandleStone1(item.Key, out long result))
                    {
                        if (shallowCopy.ContainsKey(result))
                        {
                            shallowCopy[result] += dic[item.Key];
                        }
                        else
                        {
                            shallowCopy.Add(result, dic[item.Key]);
                        }
                    }
                    else
                    {
                        DivideLong(item.Key, out long part1, out long part2);
                        if (shallowCopy.ContainsKey(part1))
                        {
                            shallowCopy[part1] += dic[item.Key];
                        }
                        else
                        {
                            shallowCopy.Add(part1, dic[item.Key]);
                        }
                        if (shallowCopy.ContainsKey(part2))
                        {
                            shallowCopy[part2] += dic[item.Key];
                        }
                        else
                        {
                            shallowCopy.Add(part2, dic[item.Key]);
                        }
                    }
                }
                return MyRecursion(shallowCopy, iteration + 1);
            }
        }
        
        private void DivideLong(long number, out long part1, out long part2)
        {
            long numDigits = (int)Math.Floor(Math.Log10(number) + 1);

            // Step 2: Calculate the split point
            long splitPoint = numDigits / 2;

            // Step 3: Calculate the divisor for splitting
            long divisor = (long)Math.Pow(10, numDigits - splitPoint);

            // Step 4: Get the two parts
            part1 = number / divisor; // First part
            part2 = number % divisor; // Second part

            
        }
        private short CountDigits(long num)
        {
            short count = 0;
            while (num > 0)
            {
                num /= 10; // Divide by 10
                count++;   // Increment count
            }

            return count;
        }
    }
}

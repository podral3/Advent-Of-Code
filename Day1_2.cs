using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day1
{
    public class Day1_2 : IAssigment
    {
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day1\\input.txt");

            int[] list1 = new int[lines.Length];
            int[] list2 = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var quick = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                list1[i] = int.Parse(quick[0]);
                list2[i] = int.Parse(quick[1]);

            }
            Console.WriteLine("finish");
            GnomeSort(list1);
            GnomeSort(list2);

            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 1; i < list1.Length; i++)
            {
                if (dict.ContainsKey(list1[i])) continue;

                int sum = 0;
                foreach (var item in list2)
                {
                    
                    if (item == list1[i]) sum++;
                    
                }
                dict[list1[i]] = sum;
            }
            int bigSum = 0;
            foreach (var item in list1)
            {
                if (!dict.ContainsKey(item)) continue;
                bigSum += item * dict[item];
            }
            return bigSum;
        }

        protected void GnomeSort(int[] array)
        {
            int currPos = 0;
            while (currPos < array.Length)
            {
                if (currPos == 0) currPos++;
                int prevPos = currPos - 1;

                if (array[prevPos] <= array[currPos]) currPos++;
                else
                {
                    Swap(array, currPos, prevPos);
                    currPos -= 1;
                }
            }
        }

        protected void Swap(int[] array, int idxOne, int idxTwo)
        {
            (array[idxTwo], array[idxOne]) = (array[idxOne], array[idxTwo]);
        }
    }
}

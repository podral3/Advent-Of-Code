using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day5
{
    public class Day5_1 : IAssigment
    {
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day5\\input.txt");
            (Dictionary<int, List<int>> dict, string[] pages) = ReadManulas(lines);
            int sum = 0;
            for (int i = 0; i < pages.Length; i ++)
            {
                int[] pageNums = pages[i].Split(',').Select(e => int.Parse(e.Trim())).ToArray(); //splitted and parsed to int
                bool positive = true;
                for (int j = pageNums.Length-1; j > 0; j--) //left
                {

                    List<int> list = new List<int>();
                    if (dict.ContainsKey(pageNums[j])) list = dict[pageNums[j]];
                    else { continue; };
                    for (int k = j-1; k >= 0; k--) //pages that shouldnt be in dict
                    {
                        int curr = pageNums[k];

                        if (list.Contains(curr)) { Console.WriteLine("False"); positive = false; break; };
                        
                    }
                    if(!positive) break;
                    
                }
                if (positive)
                {
                    Console.WriteLine("True");
                    int middleIndex = pageNums.Length / 2;
                    sum += pageNums[middleIndex];

                }
            }
            return sum;
        }

        public (Dictionary<int, List<int>>, string[])ReadManulas(string[] lines)
        {
            int i = 0;
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            while (lines[i].Contains('|'))
            {
                var splittd = lines[i].Split('|');
                int left = int.Parse(splittd[0]);
                int right = int.Parse(splittd[1]);
                if (!dict.ContainsKey(left))
                    dict.Add(left, new List<int>() { right});
                else dict[left].Add(right);
                i++;
            }
            string[] pages = new string[lines.Length - i-1];
            int k = 0;
            for (int j = i+1; j < lines.Length; j++)
            {
                pages[k] = lines[j];
                k++;
            }
            return (dict, pages);
        }

    }
}

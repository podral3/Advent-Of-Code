using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Advent_of_Code_2024.Day2
{
    internal class Day2_2 : IAssigment
    {
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day2\\input.txt");
            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int[] splitted = lines[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                if (!IsSafePrecomp(splitted))
                {
                    bool didnotpas = false;
                    for (int j = 0; j < splitted.Length; j++)
                    {
                        var without = splitted.ToList();
                        without.RemoveAt(j);
                        if (IsSafePrecomp(without.ToArray()))
                        {
                            didnotpas = true;
                            break;
                        }
                    }
                    if (didnotpas) sum++;

                }
                else sum++;
                //if (IsSafe(splitted)) sum++;
                Console.WriteLine($"{i+1}: {IsSafePrecomp(splitted)}");

            }
            return sum;
        }
        
        //Hashset from reddit enchanced by my own brute force
        public bool IsSafePrecomp(int[] numbers)
        {
            HashSet<int> safePlus = new HashSet<int>() { 1, 2, 3 };
            HashSet<int> safeMinus = new HashSet<int>() { -3, -2, -1 };
            for (int i = 0; i < numbers.Length-1; i++)
            {
                safePlus.Add(numbers[i+1] - numbers[i]);
                safeMinus.Add(numbers[i+1] - numbers[i]);

            }
            if (safePlus.Count == 3 || safeMinus.Count == 3) return true;
            return false;
             
        }
        //My own working on a tests set but not on main input solution
        public bool IsSafe(int[] numbers)
        {
            bool skipped = false;
            bool isDecreasing = false;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int curr = numbers[i]; int next = numbers[i + 1];
                if (IsDistanceOk(curr, next))
                {
                    if (i == 0) isDecreasing = next < curr;
                    else if (next < curr != isDecreasing) continue;
                }
                else if (i + 2 > numbers.Length && IsDistanceOk(curr, numbers[i + 2])  && !skipped)
                {
                    next = numbers[i + 2];
                    skipped = true;

                    if (i == 0) isDecreasing = next < curr;
                    else if (next < curr != isDecreasing) return false;
                    i += 2;
                }
                else return false;

            }
            return true;
        }
        bool IsDistanceOk(int curr, int next) => Math.Abs(curr - next) <= 3 && Math.Abs(curr - next) >= 1;
    }
}
    


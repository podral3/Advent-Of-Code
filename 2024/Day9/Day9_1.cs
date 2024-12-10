using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day9
{
    public class Day9_1 
    {
        public long Run()
        {
            long sum = 0;
            string input = "2333133121414131402";
            input = File.ReadAllText("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day9\\input.txt");
            
            int[] result = input.Select(c => (int)char.GetNumericValue(c)).ToArray(); //last item is a file
            Console.WriteLine(IsAFile(result.Length-1));
            int left = 0; int right = result.Length-1; int indexer = 0;
            while (left <= right)
            {
                if (!IsAFile(left)) //if free space
                {
                    for (int i = indexer; i < indexer + result[left]; i++) //fill it
                    {
                        if (result[right] <= 0) //if whole file is moved, move right pointer to next file
                        {
                            right -= 2;
                        }
                        sum += (right / 2 * i);
                        result[right]--;
                    }
                    indexer += result[left];
                    left++;
                    
                }
                else
                {
                    for (int i = indexer; i < indexer + result[left]; i++) //fill it
                    {
                        if (result[left] <= 0) //if whole file is moved, move right pointer to next file
                        {
                            break;
                        }
                        sum += (left / 2 * i);
                        result[left]--;
                        indexer++;
                    }
                    left++;
                }
                
            }
            Console.WriteLine(sum);
            return sum;
        }

        public bool IsAFile(int idx) => idx % 2 == 0; 

    }
}

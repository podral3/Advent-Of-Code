using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day4
{
    public class Day4_2 : IAssigment
    {
        private char[] mas = { 'M', 'A', 'S' };
        private char[] sam = { 'S', 'A', 'M' };
        public int Run()
        {
            char[,] array = ReadFile("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day4\\input.txt");

            int sum = 0; int arrLenth = array.GetLength(0); int arrHeigth = array.GetLength(1);
            for (int i = 0; i <= arrLenth - 3; i++)
            {
                for (int j = 0; j <= arrHeigth - 3; j++)
                {
                    var kernel = GetKernel(array, i, j);
                    if (CheckKernelOne(kernel)) sum++;
                }
            }
            return sum;

        }
        public char[,] GetKernel(char[,] arr, int row, int col)
        {
            char[,] kernel = new char[3, 3];
            int rowCounter = 0, colCounter = 0;
            for (int i = row; i < row + 3; i++)
            {
                for (int j = col; j < col + 3; j++)
                {
                    kernel[rowCounter, colCounter] = arr[i, j];
                    colCounter++;
                }
                rowCounter++;
                colCounter = 0;
            }
            return kernel;
        }
        public bool CheckKernelOne(char[,] kernel)
        {
            char[] diagonal1 = { kernel[0, 0], kernel[1, 1], kernel[2, 2] };
            char[] diagonal2 = { kernel[2, 0], kernel[1, 1], kernel[0, 2] };
            return ((diagonal1.SequenceEqual(sam) || diagonal1.SequenceEqual(mas)) && (diagonal2.SequenceEqual(sam) || diagonal2.SequenceEqual(mas)));
        }

        public char[,] ReadFile(string path)
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(path);

            // Assuming the first two lines contain height and width
            int height = lines[0].Length;
            int width = lines.Length;

            // Create a 2D char array
            char[,] charArray = new char[height, width];

            // Populate the 2D array
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    charArray[i, j] = lines[i][j]; // Take the first character of each string
                }
            }

            // Output the 2D array for verification
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Console.Write(charArray[i, j] + " ");
                }
                //Console.WriteLine();
            }

            return charArray;
        }
    }
}

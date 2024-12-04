using System.Threading.Channels;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Advent_of_Code_2024.Day4
{
    //I peeked at someone else's solution for this on, part 2 is my own 
    public class Day4_1 : IAssigment
    {
        private char[] xmas = new char[4] { 'X', 'M', 'A', 'S' };
        private char[] samx = new char[4] { 'S', 'A', 'M', 'X' };
        private int kernelDim = 4;
        private List<int[]> directions = new List<int[]>
{
            new int[] { 0, 1 },
            new int[] { 1, 0 },
            new int[] { 0, -1 },
            new int[] { -1, 0 },
            new int[] { 1, 1 },
            new int[] { -1, 1 },
            new int[] { 1, -1 },
            new int[] { -1, -1 }
};

        public int Run()
        {
            char[,] array = ReadFile("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day4\\input.txt");

            int sum = 0; int arrLenth = array.GetLength(0); int arrHeigth = array.GetLength(1);
            for(int i = 0; i < arrLenth; i++)
            {
                for (int j = 0;  j < arrHeigth; j++)
                {
                    foreach( var dir in directions)
                    {
                        if (MatchDirection(array, dir, new int[] { i, j })) sum++; 
                    }
                }
                
            }
            return sum;
        }
        public bool MatchDirection(char[,] array, int[] dir, int[] currPos)
        {
            int x = dir[0]; int y = dir[1];
            int currX = currPos[0]; int currY = currPos[1];
            foreach (char c in xmas)
            {
                currX = currPos[0] + x * Array.IndexOf(xmas,c);
                currY = currPos[1] + y * Array.IndexOf(xmas,c);
                if (currX >= array.GetLength(1) ||
                    currY >= array.GetLength(0) ||
                    currX < 0 || currY < 0 ||
                    c != array[currX, currY]) return false;

            }
            return true;
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

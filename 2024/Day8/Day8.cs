using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Advent6_2.Day8
{
    public class Day8
    {
        List<List<(char, Point)>> ListOfAntenaPositions = new List<List<(char, Point)>>();
        HashSet<Point> antiNodes = new HashSet<Point>();
        public int Run()
        {
            Point test = new Point(2,4);
            var z = 2 * test;
            char[,] array = ReadFile("D:\\Repos\\Advent6_2\\Advent6_2\\Day8\\input.txt");
            foreach (var listOfAntenas in ListOfAntenaPositions)
            {
                CheckForAntinodes(listOfAntenas, array);
                
            }
            return antiNodes.Count;
        }

        char[,] ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            int height = lines[0].Length;
            int width = lines.Length;

            char[,] charArray = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (lines[i][j] == '#')
                        charArray[i, j] = '.';
                    else
                        charArray[i, j] = lines[i][j];
                    Point point = new Point(j, i);
                    MarkAntenaLocation(charArray[i, j], point);
                    // charArray[i,j] = lines[i][j]; //use for normal reading
                }
            }
            return charArray;
        }

        public void MarkAntenaLocation(char type, Point location)
        {
            if (type == '.') return;
            foreach (var hashSet in ListOfAntenaPositions)
            {
                if (hashSet.Any(e => e.Item1 == type))
                {
                    hashSet.Add((type, location));
                    return;
                }
            }
            ListOfAntenaPositions.Add(new List<(char, Point)>() { (type, location) });
        }

        public void CheckForAntinodes(List<(char, Point)> antenas, char[,] array)
        {
            for (int i = 0; i < antenas.Count; i++) //tutaj chyba -1
            {
                for (int j = i+1; j < antenas.Count; j++)
                {
                    (char, Point) currAntena = antenas[i];
                    (char, Point) neighbourAntena = antenas[j];
                    Point distance = Distance(currAntena.Item2, neighbourAntena.Item2);
                    //Part 1
                    //if(CheckIfCanBePlaced(array, currAntena.Item2-distance))
                    //    antiNodes.Add(currAntena.Item2 - distance);
                    //if (CheckIfCanBePlaced(array, currAntena.Item2 + (2*distance)))
                    //    antiNodes.Add(currAntena.Item2 + (2 * distance));

                    //Part2
                    Part2Solution(currAntena.Item2, neighbourAntena.Item2, distance, array);
                }
            }
        }

        private void Part2Solution(Point currAntenaPos, Point neighboutAntenaPos, Point distance, char[,] array)
        {
            int n = 0;
            while(CheckIfCanBePlaced(array, currAntenaPos - n*distance))
            {
                antiNodes.Add(currAntenaPos - n * distance);
                    n++;
            }
            n = 0;
            while (CheckIfCanBePlaced(array, currAntenaPos + n*distance))
            {
                antiNodes.Add(currAntenaPos + n * distance);
                n++;
            }
        }

        private bool CheckIfCanBePlaced(char[,] array, Point point)
        {
            int xMax = array.GetLength(0); int yMax = array.GetLength(1);
            if (point.X < 0 || point.Y < 0 ||
                point.X >= xMax||
                point.Y >= yMax) return false;
            return true;
        }

        private Point Distance(Point p1, Point p2) => new (p2.X-p1.X, p2.Y-p1.Y);
    }
    public record struct Point(int X, int Y)
    {
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static Point operator *(Point p1, int num)
        {
            return new Point(num*p1.X, num*p1.Y);
        }
        public static Point operator *(int num, Point p1)
        {
            return new Point(num * p1.X, num * p1.Y);
        }
    }
}

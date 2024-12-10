namespace Advent_of_Code_2024.Day10
{
    public class Day10_1
    {
        List<Point> directions = new List<Point>()
        {
            new (1,0),
            new (-1,0),
            new (0,1),
            new (0,-1)
        };
        Dictionary<Point, HashSet<Point>> NinesForOneZero = new Dictionary<Point, HashSet<Point>>();
        public int Run()
        {
            int sum2 = 0;
            int[,] array = ReadFile("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day10\\input.txt");
            int heigth = array.GetLength(0); int length = array.GetLength(1);
            for (int i = 0; i < heigth; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    if (array[i,j] == 0)
                    {
                        Point startingPoint = new(i, j);
                        NinesForOneZero.Add(startingPoint, new HashSet<Point>());
                        sum2 += HowManyTrails(array, i, j, startingPoint);
                    }
                }
            }
            int sum = 0;
            foreach(var kvp in NinesForOneZero)
            {
                sum += kvp.Value.Count;
            }
            return sum2;
        }

        int HowManyTrails(int[,] array, int i, int j, Point startingPoint)
        {
            var z = array[i, j];
            if (array[i, j] == 9)
            {
                NinesForOneZero[startingPoint].Add(new Point(i, j));
                return 1;
            }
            var newDirs = GetPossibleNextRoutes(array, i, j);
            if (newDirs.Count <= 0) return 0;
            int sum = 0;
            foreach(var dir in newDirs)
            {
                sum += HowManyTrails(array, dir.X, dir.Y, startingPoint);
            }
            return sum;
            
        }
        List<Point> GetPossibleNextRoutes(int[,] array, int i, int j)
        {
            List<Point> dirs = new List<Point>();
            foreach(var dir in  directions)
            {
                Point currPos = new (i, j);
                Point newPos = currPos + dir;
                if(InBounds(array, newPos.X, newPos.Y))
                {
                    if (array[newPos.X, newPos.Y] == array[currPos.X, currPos.Y]+1)
                        dirs.Add(newPos);
                }
                
            }
            return dirs;
        }
        bool InBounds(int[,] array, int i, int j)
        {
            return i >= 0 && j >= 0 &&
                i < array.GetLength(0) &&
                j < array.GetLength(1);
        }
        public int[,] ReadFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int height = lines[0].Length;
            int width = lines.Length;

            int[,] charArray = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    charArray[i, j] = int.Parse(lines[i][j].ToString()); 
                }
            }
            return charArray;
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
                return new Point(num * p1.X, num * p1.Y);
            }
            public static Point operator *(int num, Point p1)
            {
                return new Point(num * p1.X, num * p1.Y);
            }
        }
    }
}

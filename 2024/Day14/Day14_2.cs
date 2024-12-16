using System.Runtime.InteropServices;

namespace Advent_of_Code_2024.Day14
{
    public class Day14_2
    {
        private int input_width = 101; private int input_height = 103;
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day14\\input.txt");
            List<Robot> robots = new List<Robot>();
            int second = 0;
            foreach (string line in lines)
            {
                Robot robot = new Robot(line);
                robots.Add(robot);
            }
            while (true)
            {
                foreach (Robot robot in robots)
                {
                    robot.Move2(input_width, input_height);
                }
                if ((second - 11) % 101 == 0) //I found out that there are reocurring patterns at 11, 112...617, 718 etc
                {
                    Console.WriteLine((second + 1).ToString());
                    Print(input_width, input_height, robots, second); //and then I just waited for the tree to form
                }
                second++;
            }
        }

        public int GetNeigbours(Robot robot_to_search, List<Robot> robots, int input_width, int input_height)
        {
            int sum = 0;
            foreach(Robot robot in robots)
            {
                if (robot == robot_to_search) continue;
                if (IsInegbour(robot_to_search, robot)) sum++;
            }
            return sum;
        }

        private bool IsInegbour(Robot robot1, Robot robot2)
        {
            if (robot1.Position == robot2.Position) return true;
            if (robot1.Position.X == robot2.Position.X + 1) return true;
            if (robot1.Position.X == robot2.Position.X - 1) return true;
            if (robot1.Position.Y == robot2.Position.Y + 1) return true;
            if (robot1.Position.Y == robot2.Position.Y - 1) return true;
            return false;
        }

        private void Print(int width, int heigth, List<Robot>robots, int seconds)
        {
            char[,] arr = new char[width+2, heigth+2];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = '.';
                }
            }
            
            foreach(Robot robot in robots)
            {
                arr[robot.Position.Y, robot.Position.X] = '#';
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

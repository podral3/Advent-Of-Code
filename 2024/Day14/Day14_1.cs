using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day14
{
    public class Day14_1
    {
        private int test_width = 11; private int test_height = 7;
        private int input_width = 101; private int input_height = 103;
        private Dictionary<string, int> dict = new Dictionary<string, int>();
        public int Run()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day14\\input.txt");
            int sum = 1;
            foreach (string line in lines)
            {
                Robot robot = new Robot(line);
                robot.Move(input_width, input_height);
                Console.WriteLine(robot.Position);
                string quadrant = robot.GetQuadrant(input_width, input_height);
                if(dict.ContainsKey(quadrant))
                {
                    dict[quadrant]++;
                }
                else
                {
                    dict.Add(quadrant, 1);
                }
            }
            foreach (var kvp in dict)
            {
                if (kvp.Key == string.Empty) continue;
                sum*=kvp.Value;
            }
            return sum;
        }
    }

    public class Robot
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }
        public Robot(string line)
        {
            Parse(line);
        }
        public Robot(int x, int y)
        {
            Position = new Point(x, y);
        }

        public void Move(int width, int heigth) //moves 100 times
        {
            Point border = new Point(width, heigth);
            for (int i = 0; i < 100; i++)
            {
                
                Position = (this.Position + this.Velocity) % border;
            }
        }
        public string GetQuadrant(int width, int heigth)
        {
            Point border = new Point(width, heigth);
            if (this.Position.X == width / 2 || this.Position.Y == heigth / 2) return string.Empty;
            if (this.Position.X < width / 2 && this.Position.Y < heigth / 2) return "q1";
            if (this.Position.X > width / 2 && this.Position.Y < heigth / 2) return "q2";
            if (this.Position.X < width / 2 && this.Position.Y > heigth / 2) return "q3";
            if (this.Position.X > width / 2 && this.Position.Y > heigth / 2) return "q4";
            return string.Empty;
        }
        private void Parse(string line)
        {
            string[] splitOnSpace = line.Split(' ');
            string[] splitOnComa = splitOnSpace[0].Split(',');
            string xCord = splitOnComa[0].Substring(splitOnComa[0].IndexOf('=') + 1);
            int y = int.Parse(splitOnComa[1]);
            this.Position = new Point(int.Parse(xCord), y);

            splitOnComa = splitOnSpace[1].Split(",");
            xCord = splitOnComa[0].Substring(splitOnComa[0].IndexOf('=') + 1);
            y = int.Parse(splitOnComa[1]);
            this.Velocity = new Point(int.Parse(xCord), y);

        }
        public static int Mod(int x, int y)
        {
            int r = x % y;
            return r < 0 ? r + y : r;
        }
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
        public static Point operator %(Point p1, Point p2)
        {
            return new Point(Robot.Mod(p1.X, p2.X), Robot.Mod(p1.Y, p2.Y));
        }
        public static Point operator / (Point p1, Point p2)
        {
            return new Point(p1.X / p2.X, p1.Y / p2.Y);
        }
        public static Point operator / (Point p1, int num)
        {
            return new Point(p1.X / num, p1.Y / num);
        }
    }
}

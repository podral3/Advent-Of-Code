using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day15
{
    public class Day15_1
    {
        private Point RobotPosition;
        public long Run()
        {
            long sum = 0;
            char[,] array = ReadMap("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day15\\input.txt", 50); //input
            //char[,] array = ReadMap("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day15\\test2.txt", 10); //big test
            //char[,] array = ReadMap("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day15\\test1.txt", 8); //small test
            string isnt = this.InputInst;
            RobotPosition = FindRobot(array);
            foreach (char c in isnt)
            {
                //Console.WriteLine(RobotPosition);
                
                if (c == '\r' || c == '\n')
                    continue;

                Move(array, RobotPosition, GetDirection(c), false);
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 'O') 
                        sum += 100 * i + j;
                }
            }
            
            return sum;
        }
        private void Move(char[,] arr, Point curr, Point dir, bool replace)
        {
            Point newPoint = curr + dir;
            char nextPosChar = arr[newPoint.X, newPoint.Y];
            if (!InBounds(newPoint, arr) || nextPosChar == '#')
            {
                //PrintArray(arr);
                return;
            } 
            if (nextPosChar == '.')
            {
                if (replace)
                {
                    Point robotNewPos = RobotPosition + dir;
                    arr[robotNewPos.X, robotNewPos.Y] = '.';
                    arr[newPoint.X, newPoint.Y] = 'O';
                }
                arr[RobotPosition.X, RobotPosition.Y] = '.';
                RobotPosition += dir;
                arr[RobotPosition.X, RobotPosition.Y] = '@';
                
                //PrintArray(arr);
                return;
            }
            if(nextPosChar == 'O')
            {
                Move(arr, newPoint, dir, true);
            }
        }

        private Point FindRobot(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j] == '@') return new Point(i,j);
                }
            }
            return new Point(-1,-1);
        }
        private Point GetDirection(char c)
        {
            switch (c)
            {
                case '>': return new Point(0, 1);
                case 'v': return new Point(1, 0);
                case '<': return new Point(0, -1);
                case '^': return new Point(-1, 0);
            }
            return new Point(0, 0);
        }
        private bool InBounds(Point p, char[,] arr)
        {
            return !(p.X < 0 || p.Y < 0 ||
                p.X > arr.GetLength(0) ||
                p.Y > arr.GetLength(1));
        }
        public char[,] ReadMap(string path, int height)
        {
            string[] lines = File.ReadAllLines(path);
            //int height = lines[0].Length;;
            int width = lines[0].Length;

            char[,] charArray = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    charArray[i, j] = lines[i][j];
                }
            }
            return charArray;
        }
        private void PrintArray(char[,] charArray)
        {
            for (int i = 0; i < charArray.GetLength(0); i++) // Iterate over rows
            {
                for (int j = 0; j < charArray.GetLength(1); j++) // Iterate over columns
                {
                    Console.Write(charArray[i, j] + " "); // Print each character followed by a space
                }
                Console.WriteLine(); // Move to the next line after each row
            }
            Console.WriteLine("\n");
        }
        private string test1Inst = "<^^>>>vv<v>>v<<";
        private string test2Inst = "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\r\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\r\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\r\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\r\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\r\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\r\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\r\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\r\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\r\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^";
        private string InputInst = ">><v<^<^><v<<>^>^v^><<<^<^><^v^^vv>^v<vv^<<>^v>^<<v><>>>>>^^v^>v^>><^>>v>>^<^>>>>>^v<>^v^<<<>>><>>^v^^^^^>^<^<>v<>v>>v<v<>>>v>>>^>v>>v>v^v><<v^<^><>>vvv>>^>^<v^<>^v^><>^<^<^>>v<v<v>>v^<^><^>>vv^<>>v<<vv<>^^^^vvv<^^<>>><vv>>>><v<>^vv^<v>v<>v^><>>v^<^^<<><<v^><>>>v>^>^<>^><<<vv>^<^v<><v<<^<vv<<v^^><<^<>vvv<<><>vvv>>^^^^vv<v><<<v>vv<<<v><v>^v>v^>>>>^<<<<v>v^v^<v<vv<>v<<v>>><^><v>v<>^^>^<vvv^>^^<<<v<<<<<^<^<><>v>^>^<^^<v<<>><v>v>>^<^>>>^<vv^v^^v^^^><^<v<^^^vv^<^>><vv<<<^^v>^v^^><<<^<v^<<^<^>^>>>^>>^<^>^<<<vvv<^^>>>vv<>v><><v>v>v><^^<<^vv^>><<v<>vv>><<vvv^<<<^<^^<>>^>^^^<v>v<<^>>>v<^^><<>>v>v^>vv^<vv^<v^<^v<^>>><<v^v<<v<<vv^<^v^^^^^>>>^v><^v<<><^<<>>>vvv<^^<>^v<v><>>><<^vv^>>^><^>>^vvv^^^^<v^<>^vvv^>^^vv>^><^v<>>vv><v><<<<^v<^^><v^>v<>>^><v^>v^<>v^<>^<^>><^^<^^<^<v^vv>^<<<><<<v<v>>>v<vv>^<v>^<v<><^v^<<>vvv>^^^>v<^>>>v>^>v<>v>^^><>vv>vv<^v<<v^^>>vvv<v<><<<>^v^vv<v^<^vv^^v><<>>v><^^<^vv>>^<<vv^v<>vv>>^><<v>>>><<>^<>>><>><>>^<><v>^^v^<<><^<<v<^v>^^v^^<<>>^>^^vv>>^<^>^<<^v>>^><<v^^>^>>vv^^v>v<<\r\n>vv>^^>>v>^>>v<<<^v>^><<>v^><>^<<><v^v^^vvv<<<<^^^><v^>^<v>>^<<^<^>>v>^^vvv>v<<>>v<<^<v<<<vv<^v<v^>^^^>^<^<v><>>^><>>v^^vvv<vv><<vv^>^^<<>^^v>^^>^<v>v<^<^^^v<>^^vv>vv^v^^v^vvvvv<<>^<^^<^^>^<>>>>><<><<>>^^<>>>>^<<^<<v<>^vvv<v<<>v>v^>^^v>>^<v>^<>vv><v<vv^v>>^^<^<<>^v^^^>^>v^v^>^^>>>v^><>^>v^vv^^>^<^^>^<<vv^>v><>^<v^>v^<<<>>v^^<^<v^v^vv^^<>^><>><v>^^>vv<<v<v>><<><>v>v^^<<^^vv^vv^>><<>>v^v>>>>^v^^>>^<v><^^v>>^><><>>^>v<v^>v<^>v<v>^^^v<^v>>vvv<>>^vv<<>>^^vvv^^<><v<>^>^<>>^v<><vv<><><<<<>>>^<vv^v<v^<vv<v>><><v>>>v^<<v^v^>>v<v<v<>v><<>v>v<^^^><v>^v>^>>v>^>^v<^vv^>>^>>>^<>>v<><v><<>>><<<v^<^<vv>><^>v>v>^vv>^<<<^><^^v>v^><v<<^>vv<<^^^^>^><<>^^>>>v<>v^^>vv^v<><v^^v<<>v^><<<v<<^<^<^^v<><>^^^>v><^><v^<>>v><v^v>^>>v>vvv<v^<<>><v^>>>v^v>^>>^^v<<<><^<^>>v<vvv<^<^><<v>>^<v^^>^v>><>><<^^^^^>^v<v^^>^<<>vvvv^><<v^^>><^v^vv>v^^v<^<<^v><>^<^^>v^>^<v<<^<^^<v<v><<<v^<>^v^^v<><<^^v^><<><>v<<v<<>><vvv^^<vvv<vv><>vv><^>>^^<^<>><<>><>>^<<v>vvv><^^vv^vvv>>><>^^>>v>><><<><><^^^<>>>>>vv^^v>^<>v^>^><v>vv<v<<>^>>>^>>\r\n^v><<^><<v>v^^^>^>>><^^^>v^^v>^v^^<>^^^>>^>><<><<v^>vv<v<<^>^^v^><><>vv<v>><vvv^^><<<^<>vv<<^v^>vvv^v<v^v><>>^v>^^>vv^<>v^<v><v^><><^^<>^^>v><v>>^vvv<>><<^>^>vv<>>^<v^vvv<<<><<v>v<>>>v>vv^>^>vv>v<>^v<><^v>v<<^>^>vv>^<<>^^<<v<v><>^vv<>vv<>>v^^><><^<^<v^>v>>>v^><<v>^<^>^>>><^>>^>^^v>vvv^<>><><v<<>v>v<><>v^>><v>>>vv>^^^v>v^v<>^<<<^v<<<>>><^v<^v>v<^<>><<vv<^^v^<v^>>^^<<>^^>v>vv^<><><<^<>>>vv^^>v>v^<v<^^vv^vv><v^>^^<^>^^<>v>v>>^><<<v>^><<>><v<<>><<<v>^^<>v^<>^>v<<<<<v<^<>v^v^^<<^<vvv<^<>vvv^v><v^>^v^^^>v>vv<^>><<v<><>>>^v<v>v>vv><v>^v><<<<^<^<>>vv^>vv^v>v^v>v>^v><<^v><>^>vvv^v<^vv>v>v^v^^><>v^^vv<^<v><<v<^<^^>>^v^<<^vvvv<^>^^^^v^^^^>^^v<<vv<v>>^>^<>^^<<^<^<^vv><^>^^vvv^>><>>^v>v^^^v><^<v<^<^v^>^^<>>vvv<>><<<<<>^<vv<<<^>^^><><<><^^>v><^>>^<^v<^^v<><^>>v^<>^<<^v><<^<v<^v^^<v<<^vv<>>^v<>vvv<>v>>^<vv<v^^v^>v<><v<^vv^>><v^^<^>>v<>vv>>v^<>^v><><^^^^vvv>>^<^v^^<^^><<v<^>>><^>^v<^><<>v>v>>vv^vv<vv^^vv<>vv^>^v^>>^<>><<v<><<^<v^>^<<<vv^^^^^^^>v^>>v<^>^v^^v>^v>^<>vv^^vvv^><v>>v>>v>vv<v>^<>v<>>>v>>>>^<\r\n<><>^v^<v^^<<>v>>^v><<v<><<v^^<vv>>^v<^v<<^<>v<>v^>>>>^v>>><v^v^<v^>^^^v>^>^^>^<<^^>v>v>>^^<>>>v<<v<<>v>>><^^^>vvv<<v^<v<<v>v^^v^><<>^v>^^>><>v<>^v^>>^v^<vv<^<<^^<v^v^>v^<<<v<>^><<<>^v<>>^>>^<<v^v>^vv>>>v^>vvv<<<v<<vvvv<>v^>v^v^<v<<<v<v><vv^^^>^>><>^^<<v<^v<^<>^^<>v<^v^^^<>vv^<<^^<<<v>v>v^<>^^^^<v><^v<^><v^>><<vv<v^>v<<<<^^^>^^v>v^v^^>><^^^><v<><v<<v^^>>>^^<<v>vvv^><^v^^>>vv^v^>^^vv<><>><<<><>^><v<v>>>^<v>^>v^>v>>>^><>^v<>^<v<<vvv>>>>>>^v<<v^<<<^<<^<^<vv^<v<<^v^<vvv>^^v><vv<^><^v<><^<>^<>^>>>>^>>v>>^^v><>v^<<v<<^>vvv^>^v<^<>^v>^^><v>>vv>^^^<v><><^>><^<<<v>><v<^^^vv^v^>^><v<^>^^><^>>>v<^><v^<<><vv>^^^v<>><^v<^<<<^>v<v><vv<vv^v<<^<<<>^<v>^v<<<^<^>^v^v>v<>v^v>^<^^v<v<><><^<<>^<<vv>v>v^v<v^><>><v^^>>>v<^vv>^><^^<v<>>v^^^<^^<<^<>>v<^>^><<<v>><vvv<>^^^^vv>v>vv^>v>><v^>vv>vv^^^<^^vv>><>^^>>vv<^><v<<v^>^^<vvvvv><v><v<>^>><><<<<v^>>>v><^^v^v^>^vvv<<>>^<v>v^^^>v^>v>v<v^^<><<><<^>><v><v<<v^>vv<v<>>><<>^v>^^<v>v^v<<>v^<^^>v<vv><^<>^>v^^v>>v^<>><^>^v<v^^<>v>>><<<v<^<>><>><<v<^>><<^>>>><<vv^^^v><^^>\r\nv<<<vv>^<>>^^v<v>^^^<<vvv^><<^>><<>>>><v>v^<>^^^^<vv^^><v<><>><^^>^<v<>>>>v<>>><v>vv^>v>>>^<^vvv^^v<<vv>v^>^v<><^>^<v><v><v<<^<<^><v<<v>>^<>^<v^<^<<v>^<<^>v>><<>>v^v>vvvv^vvv>^^>^^v^><vv^>v<>^<>>>v<vv<<v<^>v<v><<<v^^>v<^<^><^<<v>^^>^>>^>vv<<<v^>^vv>>><v^>^v<<v>^<vv>^vvvv^>>><vv<v>v^^v<vv>v<>v>>>>v^^><<><^<><v^>^v^>^^>^^<<^v^v>>vv>vvv>v<^>^^<<><<<<^>^>^><>^>vv>>^v^^<<<^v>>><<<<vv^<<<vv<^<v^^<v<<>^v^<^^>v^><v<v^<<><>v<^<<<<>^^v>^vv><<<^v<<^>^v><>v^^v<^v<<^<^<<>^^^<v>>v<^<^>>^^^v>vv^><<>^v^vvv><^>^v<>vv^^^<vvvv^<v>^>vv<>^^^^v>^<^<<^^>v>v><v<^vv<<<<>^^><><>v^^>>>>vv^^v^^^><v^^<<v^<>^>v>><>>>>^v<>^vv>v<^>^^^>^>v>^^^^v<v><>v^^<^^^v<vvv>^<<v^>>><<v>>v>^vv<><<<vv^vv^^<>><v<^<<vv^^<v><^<<<^>><v^^<^^><><><^^^<^^><^<^v>>vv<v<<>>vv<<vv>>>><>^>^<v>>^<v^><vv>^v^v<^vvv^<>><>^^<>^>vvv^<<>>><vv>^>v>>>>>v><>^><<<v>^^v<<^v<v<^^^vv^>vv>v^<>>><^<<v>v<v<v^v><^v^>v>^^v<^^^<><>v^<>^<<^^<<v<^^v<<<^>^v^v>^^^v<^vv><v>vv>^v<<<v^v^^^v^<><^v<<v^<<<^<^vv<v^^>^<^<<v^>>v>^v<<v>v<v<vv>vv^^v>>>v^^v^<<>>^^<<>v>^^vvv><^<>\r\n>v^v<v><<^^>^<<v<>v^v<<<<<^<v<v^<^<>v<<^v<>v>vv^>>><^vv^v>vv^<<vvvv^^v<vv^v<<^vv^v<v>>v<^v<v<<^<<^<^v^>>v>^<<v^^<^^><v>>^><^><>^>>>^v^^vv^v^<<^><^^v><<vv<><v>^^>^^<v<v>v>^<<vv>^><^^<<<<v><><v>>v^^^^>^>>^^v<v^<v<^^^>>^v>>vv^<v<<<<>v^<>v>v><<^>^<^v<vvv>^<vv<<<<vv>^v<v>v><v>>>><>vvv^>v^v>^^v<v>>>^v>^^<vv><^>v<<^>v>>^>><>^vv>^v^v<^v>^>v<><v><v<^vv>^<><>^v^>^^<<<^>^^v<<v^v^>>^^<v<v<<<><<>>v<^v>>><^vv<><<^v>v^<^v^<<><v>>^v><^v^<>><^vv<v>>>v^<v>^>^<v<^v<^^<^^>>^<>>v^<<^>v<^>^<^>^>>^^v>^vv>^>><^<vv^v>>v<>v^v<>^^^v^<^><<>^^<^<^>v<^vv^><v<v<<><>vv^^<>>>>^v>^>^^vv<<<^<>>>v>^<v^<^vv>^^v>>>vv^>><>><<<<^<vvv<>^^<>^<v<v>v<<><v>><<>v<<^v>vv>>><^v<vv^>>>><<<v>v^vv>><v><>^>^^><v<>v^<^^vvvvvvv>v>v^v>v^<><>v>v<<<>>v^vvv^>v<vv<<^<^^^<v<><^>><v<v<<<vvv><>vv^^v^<v^><<v>^<<>^<>>^vv^<^<v<>v^>^^>^<v>>>^v>v^^<<<><vvv<^>v<v^>>^<^v^^<<vv^^v<>v^v^>v^><>v>v<^<v^^v>vv<<^v^>^v^^v<v<^<<v^>^vv<><v^<vv^>^>>>v^>vv^v<^^v>>^<v>^v<^^<>><>^>>^^v<v^>v>v<>>v^^vv><v^<>vv<^>v>^^>>>v<^v^v<v^<^v>^<v<<>v<<><<<<^>>>^^>^<<^vvv^<>vvv><\r\n<^>>^><^v<>^^>v<^<>v^>^>>>v><^v>v^>vv^>^<v>^<^v^>v^^>v<>><^^^v^<>^v>><v<>^<^vvv<<>^v>v<<vv^<^v^<^><^<><v><^<<<><vv<>>^v<>^><<>vv^v<^>v>>^<>>>>>^<<<>^v^^<^<v>vv^<v^v^^<>^<v>vv><v>^>^^^v^vv<<^^<<><<^>>>v>^<>^>^<>v<^<>v^>v^vv^<v<^^>>^v<<><>^v<<>v^><<^><vv<<v<<v>>^<v^>^<v>^v^>vv<v^^vv>^<<>^<^>^>>^^<>v>^<^<^vv<>vv>v><v^>v^<^^><^v<^^<><<v^<v<v>v<^v>^<>>^^v^>^^v^<v^^^^><<^<<v^><><>v^^v^<>>v>^<<<^vv>^v^>v><>^v<vv<^<>vv^v>v>>v><^<><>^v^<v^^<vv><^vv<vv>>>v<^^v<v^vvv<^v^<^>^><^^<v>v<^vv<>><<v^<>^<v^<vvvv>>><vv^<<^^>>>^v<<v<^^<^^^<>v^v<<^v^>>><>vv^v^^^^v<^^<<>v><>>vv>vv^<^v>v^<^^<>^^>^v<<v>>^vvv>v>vv><^<v<<vv<><<v<v<^^^<>v<^v<>vv>><^v<>>v<<^^<v<<>^v>v^>^<<^<<<<<v<^>vv>v<<<<<^>v>^<>>>v<><<>v>>vv>^^<v><v>^v>><vvv<v>>v<<^vv>^v>vv>v>>v^v<<>^<v>v^^<<v>v^^<<<^v<<<<^^^vv>>>>>v<^v<><v<v^vv^<vv<<<<v>^<vv^>v^<^^^^>^vv<v^>^>><>^<v>^<<^v>>>>vv<^^v><<vvvv><v<>>^<<<>^^^v>v>^^>vv<v>>^>^>>v<<<^v^v^vv<<>^<<>v>vvv>vv<<v^^<^^>^^v><vvv<<^^<v^>vv<^vv<>vvv^vv^^>>^>>^v>^v>>v<><>v^<<^^^v<<<>v>v><<^v>v^^vv>v>^^<>vvvv<>>^v\r\n^vv<^<>vv^<>>>><<v^^^^>v^<v^^v>^v^v<v^v>>^<><<v<<^>>^^v><^^>^v<><^v>vv^>>><<>v>^^v>>><vv^>>^^<<>^<^^<v>>v<<^^^vv<<><^^<<^^<^>><^<>v<>v<>>v<>v^^<<v>>v>^^vv^^<>^>><^v>^^<^<^^>>>^<v>vv^<>^vvv^v><v<v>^^^v^>^>><^<v^<><>^^>>^><v^^^v^>^<>^>>v><v>^<>^^v>>v><><v^>>^^^>v^><>>vv<<^><v>><<^v>>v<v^<>v^v<<v<vv^v<^v>>^<v><vvv^^<v^vv>^v<>v>^v<v<^v<>^<<<<^^>>v><vv^<<><>>>^<^v>vvvv<>^v>^<^^<<vv^<v>vvvv>^vv^<^>v>v<<<<v^<<<vv^<<v>>v>v^<^v^^v<<vv><v>^^^<<<<v^>vv><vvvv<>>>v<^^<><^>>^^>v^v><<<>>>vv<<<v<>><v^<<^>><v^><v<vv<vv><^<v<v<>>^^<v><^^v<^>>>vv>v^^v^v>v>>><>vv>^vv<^^<v>v<<v><><>^v><><<^><v>v>v>>vv^>>vv^>v<v<<<^<<vv>^^v>>>^><^>>>>>>^^>^>^>v^vv>><v^v^<>><<<v<<>^<>^^>>v<^<><^<v^v><^<><><<>><vvv^v>^v><<v<><^<<^<vv<<^^^>>>v<^<^^v^^><v<v>^<^^v^<>^v<^v<><<^^^^^^^<>v<v^^^^v^<<^>^><<^^v>v>v<>^>>^>>^^^<>>^^>v^v<v^^^>^<^<^<>^v^>v<vv<<v>v<v<<<^>>v^><>>v<>^<<v<><>^^^v<^v^^<<vv<>^>><<>^>v<^>v>vv>^<^<>^v>vv^^<<^>v<v^<v>^<v^><>^v<>^^^>v<<v><<<>>>v>>v^<^<vv<^>vv<vvv<>^^^^<vv^^<v^vv<>^v>^^>>v><^>^<v^v^>^>^<^^>><^<v<v<>^\r\n^^vv>>v>v<v<><<<>^<<v<<^<>>^>>>^^>>vvvvv<<v^<<>v>><<><^^>^^<v<>><<>v<v^>^<^<<vvv^<>vv<v<^^<^^v<^v<v>>>^v>^^^>^><<v^<v><<^<<<^>^>><^<^>v<v<>vv^>>^^>>^>^v>v<v><v>v<v<>>^>^^>^^v^^^<v>><^vv^vvv<>vv>vv^<<v>><>>^<v^>^v^^^vvv>v^<<^<<v>>v^^<><<^v><><^^>^<>>^^vv<^<^v<><vv<v^>^>^>>><^^>^vv<v>vv^<^^^v^<^<v<>^^^v>>>>v^v>>v>v>v<<<<>^vvv<>^^v>^v>>v><<v^>^>><v<>^v>><<v<>><><<^>^^vv<>^>^v><v<vv<<>v^v>v>^^^vv>>>v><<^<>><<^<<^v<^v>>v<<v<v>v<^<^<^<>^<^v<^v^v^<v<<v^^v^><^>>>><^<>v<v^><>v>>v<>vv><><^>>>v><^<<v>v<>v>>>v<<v>>><<>v>v^<^<^>>^<>>v>^v><>vv<><<v>>^<v^<<v<^><><^vv^><v>>^v<>^v^v^>v><<v><<>>^>v^><>>^vv>^^^^<<<<^^><v<<v<^>v^>>><v<v>^>>v>^v<^v>v><^v^>v<vv>v<^vv><^^<>>>>^v<>^v^><v>^>^^^^>^>>^v^^<<v>>^v^<<^>>>vvvv>^><vv><vvvv^><<><v^<v>^^>v<vvv^^^v<<v<>>v^<^>^^><<vvv^v^^v>>>v><<<<^^vv^v^<<^<<><>v^^>>>^>><^^v^^<>v>^<<>v^<^<v>^^<<^v<v<^<v>^><<v<v<<vv<>>v>^<v>^vv^v><^^>^<vv<v<^vv<^<>v^>v<^v<^<>^<^<<^^>vv><^<v^v^v^^>^><<v<v^vv>^v><^v^<>^vv^<v^^v>vvv^v><v^^<<^v<^>v<><><^<<^v><<^v>^^^^^>><<>^><><<v^><^>v>>>v<\r\n>>v^>^^<v>^vvv>^^<<^^>>v^<<v<^v>^v<v^>^><^><<v<>v<vv<><>>^>^^>^>><^^>v<<<><>><<<v>>><^^><>^>v>v^v<<^<^^><^><^<>v><<v>^<v><v><^>vv<v>>vvv>v^vv><<v^^vv^v<>vv^><<<v<>^<^<<<^^v^<><^vv><v^^<v<<^v<>v^<>^^<^<<>v^^><^>^>vv>><v<^^>^<v^<<><<v<<<^^vv<<>><^<<v<v>><<>>>v^>^>^^<>^>v^<^<^vvvv>^<<>v<>><^<^^<<^><^^<vv<<^<<^v<v>v<<^^^<>^<^>v<>^v^>^<v<^<^v<vv>>><<^^v>><v^<^^>>v>>v^v<v<v^>v<v<<<><^>^^^^<<<vv>>vv><<<^v^<<v><^>^^vvvv>><>>v<>>^^^>><<vv^v<>>v<v^^v><^<^>>>v>>>>v^><vvvvv^<^<^<v><^<<><<v>v^>v>v<v><>^><^v^>v>><<<v^>vv>^^<<vv>v^>v<^vvv<<>><v^vv<v>^<v<v^^><v<^v^>vv^v^><>>>><>^vvv^^v>^<^^<<<><>^<<vvv^<v>v><^<v^^^v^>><vv^><vv><^v^<v>v^^v<>><<>^^>v^^v<<vv<>>v^^><<vv<^<^><^<v<v<<v<^v><^^v>>vv^>v>vv^v^<>><<><>vv^<<>^>v>>>>^>>vvv<<>^>^<v^<<v^v>>><<v>>v^^v<v>v>^<v^<<^^^v>^>^vv>v^^<><v^v><<<v^v^<v>>><>v^><v>v>vv<><^<^v>^<^vv><^<^^>vvvv>v<<^v>v<v<>vv^>v>v^v^vv^^<^><v^<<^>^>><<^v<^v^^>^><<>^v><^>v>>><^vv^^v^^v<^>v<^<<^<^<><^><v<v>^<<^^^v<^v^vv><><^<>><>^<>v^v<v<^<<^^<<>>^><<vv^<><<<vv><v^<^v^^<><^><vv<>>v>>v\r\n>>v<<><v^>>>^<v<v<^v>^^>v^vv<^^v^<>^vv>vv><>vv>vv^^v<>^vv<v^<v^><>><v>^^v<<^<>v^^>v<>v<v>>v<^v^<>v><^<<<^<<<v>v<^><>vvv^>^^^<^v^^<v><<v<<>v>v^<><<^v<<<<>><>^v>^^v^^<<v^<><^v<^<<<><^>^^>>>^^><<<>^^v<v>>^^>>^<>v>vvvv><<v^>><^>^>^^<>>>>>^>v>^v^<>v<>>v>^v<^^<<vv<^>v<<v^<^>^<v^vvv^^<<vv><vv^><<^vvv<v>^v^vvv<<<^<>^<><<^<><<>^<^<>vv^><<v^<>>^^^><^^v^><v^>v<<>v>^v<^v^<^^<vv><>^vvv<v>>v<<^<<v<^^v>^>^v^v>><>>^>v^<<v<<>vv<v<<^^<<^>>>>>v<vvvv>^^<>vv>v^>^^vv<<v<^v^<>>><^^^v<^^^v^><v>v>>^vv<<>v>vvv>v<>>vv>^><v<>vvv<v<>^v<^^^^<<v>>v>><vvvv^>v<vv>>><<>v<>^>v<<^<><^><v>>^^^^<vv<>^^>><vv<>^^v<>^^>^v<v<<>>v^>v^<><vv>^<v^<^<v><<>><<v>vv<^^v<<<^^v<^v><<<<><^^vv>vv><>>vv^v<v<<><<v^>^v^^<v^^v<<<^v>v<^v><^^^v>v><>vv<>^>^^>^<^>vvv>v<>v^><>>>^^><>^<><^^v^^<<^v>v<><>^^<>>v^>^>>vvv<>^<^^^vv<^><^^v<<>>^<>^v>>^>^><>^<^^v>>v><^^><<^vv^v^^^v<<vvv^>vvvvv^v<<<vv^<<v^v^v^^<^vv^^v^>^<<^^v^^v>vvvv<^^<<<^^^<^v>>v>^^vvv^v<^^>v<><vvvvv^vv^>>>>v^><^vv^<vv>^vvv>><^<^<<^^<>><<><^^^<v^v>>vv><^><^<<v<^vv<><vv^>>v^><<v^v^v^^>>>>v<\r\n^>>v>v><vv<v^v^>>>>v>^v><<v<><>>>v><><>vv<^v^^>>v^v>>vvvv<<v<^^>^vv^^>vv^<><vv>v<>v>^v>>v<>^><v^^>^<^^^^v^^^><>^>^v<>>>vvv^^<<>vv^><v<^v^^><^v^^><^v^^<<^^^^v^^<<v<>><v^>>v>>>v<v>^v^v<vv^v<>><<v^^<^v<>>>v>>>^><^v<<>v><>>><v<<^^^>>>^<<v^vvv<>^v^vv<><v<>><vv>^v>>><><^^>^<>vv>><<><vvv>>><>><<v<^<v^>vv^<v^^^v^v^vv^<>v^v^v<>>>^>^>^vv>>^v<<><<>vv<>vvv>^<^>v>^vv>>^<v<<<^>v>v^^<v^>vvvv<^vv>^^<>^^>^^>v<^>^>>^^^><^vv>v>^>^^<>^v<v<^vv^>><<^<>vv^<v<^v>vvv^v<v>^vv^<vv^<<<v>^<<^v<v^^<^v<^>vv^^^>vv><><v^^^>^<<>^<^>>v><^>v>>^^<^<vv^>^<^<<>^v>><><<^<<>v<><^<v<<><^>>>vvvvv^v<>v><<v^><^^>>v^^vvvv^^v<<>^^<v^<<>>v><<^>v>v<<<>vv<>vv><<<v<<vv>v^^<<v><<v<><^>vv^>^^><<^^v><v<<<<><><^v>^>^^>^><^^>>^<^<>^^^<<<>><>>v^v>v>><vv>>><^><v^^^v<>^<>>>^v^<<<>^^<<^v^^^>^<<^>v><^>vv>^<>><<<>vvv<<v><v><vvvv><^^>vvv^^vv<>^<^^>vv>>^<<<<>v^v<^<^><v<^vvvv<<<>><><<>v^><>>v<v<vv>^<<vv<vv<^<<<^>>^v><<vv<<^v^>>^v<>v>>>v^v>>v><<^>><<<>>v>^>vv>^<^<>v<^^>><>>><<>^<vv>>v>v^<^vv^><^^v<vv^<<<^<^^><<v><vv^^>v<^^^>v<v<>>vvv>^vvv<>>>>><^vv<^\r\nvvvv<<vvv<^^<<^>v^<>><v<>>^>>v^vvv<<vv>^>><<>^<>><<^^vv^^v><v>>vv^>v<>><vvvvv>^><<^^^^<<>v><<<^v^v^<<^vv^^^^<<>v^>v<^>^><vv<>>^<>^<^<^vv^^^<^<><^><>v^<<<<v^<<vv<v^<v^><v><<<^^>v^<v<^v<>v>^<<>v>vv<^^v<<^v<<>^>^><^^<^^^<><><><v>>v<v<>v>v^v><<^v^v<^v>>><v>>^<><<<^v>>^v>>v>>^^<^>vv<^<<<>vvvv^<><<^^v<<<^v^^v^^<^><^>^><v^>v<v<vv^v<<>v^^<v>>vv^vv^^>^^>v^^<^>^^>^<v<>v<>v^>v<>^^v<>>^^^v>>><vv^v<>v<<><^v^><v<^^>v<<^<<vvv><<<<>vv<<^><^<<^^^^>>^>>v><^^v^^^>v^><v>vv^v>^>^>vv<>>vvv<v>>>v<^vv^^v^vv><v>v<<^v^>vv<v^>^v><>>^<^<^>^<>^vv<>^>v<<>^^^^^^<v^<^vv>><^>><<^<^^v<>><^<<^<v<v<v>vv^^>>>^><>>vv^vv^^>^vvv<>>><>vv^v^^vvv>v>><^>v^>^v^<<^^^^v^^^<><^^^^v>>>^<><vv<<v>v>>>^>^>>^^v<^^v^<^^^^><<^^><v>^>v>^^><v^^>^v<^>><><>^v>v>>^^<<>^^<^<<v>^>v>^v^<>^>^^><>v^><><>^>>>vv<^>v^>^^><v<<<>^<>>^><vvv<>vv^><<^<v<^v<^^<>><>^vv>><^<>>^^vvv<>>vv^^<<^><>^<>^^<v<<vv>>>>^^v^^v^<v>^><>^v<>v^v^<^<>>^>v^v><><<<^vvv><><>>^>vv^^^^^v<<vv^>^^>><^^><^v^vv^v<^<v<^v<>^>v>^>>^^>v>>v<^>v><>><v><>v<^^<><><<v>^>v<v><v^>v<^>v>v<<vvv><>v\r\n<v<><^v<^v>^<<>>^^^>^<>^^><^v><>><<^vv^><^>>^<><<<>><<>^v^^<<v><><><>>vvv^>v>^>v<<<^v>v<<>^^<>v>><>>v^>^v<^^>^^v>vv^vvv<v<<^>><<<>^<v^<>^<vv<<v^^^><^<v^v^^v^v<>v<^v<v<^<v<v^vv>^v^^<<<>^v<v^^^v<>><v<v<<v^v<<>><^^<v^v<vv<<>vv>>v<^>^vvvv^^^>^<<>vv><v>^<>>>^<^>^>^vv^><><^>^><^>^v><>v^<v^<>vvv^>^v^<v^>vv^>>v<<<><v^v<v>vv>v><<v^<^<^v>v^<<<v^v>^<<^>v>^<v<^>>>^vvv>vv^<v^^^^<v<v><<^<<v>^><><v<v<^<<>>^v^v><vv<<><v>v>vv<<^^>>^<^>^<<<v<><>v^><<vv><^^<>v<<<v><<v^><^<v^><^v^^^<vv<^>^>>vvv>v<^>>vv<><^^<>vvv>v>v>>>>^<^vv>^>>^^^^vv<v^^vvv>>>>^>^<<<>^<<v><^^^^<vv<v>v>^^>vv><v><v>^vv<^<>>>^v>^<^<^>^<><v^vv<vvvv^>v^vvv<^<<^vv^^>><<^^v<^^<v<^<^^><<<vv>>^<v>>v>v><<^<vv^v<<^<v^>v><>^>^v<>^<^^v<vv^>>vv><>v>>^>^v<v^<<^v<v^>v><<v<>^>^<<v><>><v>>^>^>>^>>v<v>^<^vv<^^><>^>>^vv^>v<<^>>><<>>vv^v><>vvv<>^v^^^><vv^v^v^<<<>^^^>>^<><^^>v<^>v>v^^>>>>><^^v<^>>><v>v^><<><<vv<<<vv><^v>^><^<<<^<><<<>>^v>^><v^<<^^<^^><<<>^>^^^>>^<^^<<^^<^>><<^^v<v<^vv>^<v^<v<vv^>>^^>v<^vv><^<^^^<^^><<v>v<v^v^v^v>v^><^<<>v<<^v<^v>^^^v<vvv<vv>v\r\nv^<^<vv>v<>v^<^>vvv>^^^v^^v<^<vv>><^>><><<<v^^>>^v>v<<>vv>vvvv^<v<^^><<^^^vv>>vv<^^^v>^^>^><>^^v<v<^v>v>vvv^v>>v>>><<v<^>^^>^^<<^<v<>><^<>^>v^>>^<><^<v^v<>>v^^<v<v<>^v^vv>v>^<v<^>><><<^<v^^^><vvvv>^>><v>^>>>^^vvv^^^^^v^^v>^>>>>>^><<^><>>^>v^<v<vv<>><^<><v>vv<<v^>vv^>>v<<<vvvvv>v^<>v><<^^v><<v^>^<^><v<vv^>v<><<^^^v<>^^><>v^<vv<<<>^^><v<vv<vv>^^<<^<^v^v>^^><v><v>>^v><><v^<^v<>>v^<>vv^vv>v>^^<^>^>>><<^><><v^^v>>v<^vv>v<<>vvv<>^>^>>><><<vvv^v^v^v><><>vvv<><<v>>v^^v>^<>^><^>>>v^>^<v^<vv<<<>>v>v<^v^vv^>v<v>^<<>^v><<v><^v>v>><>^>>>vv>>vvv>>^v>v^>><^<<<<^^<>>>^<<vvv>><<<^<^v^^<^^^v^v^><vvv>v<v^<><<<^<>^v<v<vvv<>>v<^^^v>><<>>^^>^<<>>^<><<^>><vvv<<>^^^><^><<<^<<v<<^<<vvvv>^v<>>^v<<v^v>>>^>>^>^^v^vv^<vv>v^>^><^<v^^^>>^vvv^<<v><v<<<vv^v^v><^>^>vv>v>><<^>^^vv>^>vv>v><>^<<vvv><^v^><<^>v>>v^v>><<vv>^v^v<v>^<vv<vv>^<<<^^v^>^><<>^vvvv^v^^<v^>v<^<><>vv<vv^v>v>^v>><vv<><v^<><^><>^<v>v<>>v>v^>^v^vv>v<>^^<vv>^^v^>vv><<^<^^<v<vvv^v^^<>><<>>>v<v>^><><v<>^v<><<^^vvv>^^<>><^v^>vvv^^<^>^>><<^>vvv<>v><^v><<v>v>>\r\n^^<<<>^<vvvvv><><^^^>v<^vv^^><^vv<<^^<v>^><><>^^^v>^v><vv^<>^^^>vvv^><><>^^>^<v>>><<<>v^>v>^v^^^v^v<>>>>><<v<>>>^>>><^v<v^<v^v^<v<<v<<^^vv>^v^<<v<^v^<>v<^<>^v^<v<<<vv<<v<<^>v^^>>v^<>v^^^v^^<<v^>^v><>>^^><<^>><v>^<^vv^^v^>><^^v<>^v^^<<^^><vv<v>v<v><>>v<<>vv<^^^v<>>^v^v><>^>><^^vv>^vvv^v<vv>v^<v<>>v^>^v<<>v^<>vv<v>v^^^v>^v^vv<v^<^^<^^><vv<^^>>>^^^vvvv<^<^<>^<^^v><>^^v><>^<v^>v<>v^v<>>v<><^^^^>^<<>><^^>vv^<<^><v>v><>>^<<^<<^>vv><v^<<<>^<^<vvv<>^<<v^v^>v<>v<v<v>^><^>vvv>><v^<v>^v>v<^<>v^vv>vv<v<^<^<<<><>vv>v^>>>vvv<v><vv<v><^<vv><^^^<vv<><v<><v^><vv<v<>>><v^<<v>v^v^^v^^^>>^><^v^^><v^v^^><><vv<^>>v^^<v<^>^^<vv>v>><<v^>>v><<<>vv<<>>><><v^vvv<<^>^vv>^^<><v>^>v^^>v^^v<v^>vv^<<^vv<>v<v<>^<><v>v<v>><v^<v<<<vv^<^>>v>v<^^>^v>^vv<>v<v<>>><<v^<<>^^<^^>>v>>>v<<<>>>v>>>^>>v<v^>v>^v^^<v>><v>v<<<^>v^>^>v<v><>>>^>>^vvv>v^vv><^^v^^^^<>^>^>>^>vv<<^^>^<v<^>v>>^<>v>^v^^^v<v><v<^><<v><<^<<^^>vv>^<>^^^<v^<v<><^<<>^^><>>><<>vvvv>v<v>vvv>^<><<<<v>v^<><^v^<^<vv>>>v>^<^<^v<^<^>v<^<v>><<vv>^^^^^v^>^^><>><><>v><<^v<\r\n>>><v><>^^<^vv><<v^vvv<^v>v^<>^v^^v<<>v^v><<<^^<<^<^>><<v<>v>^><>^^v^^v^>v>v^^^><^v>>v>vv<>>^^<<^><^^v><vv>^vv>v^>v^v<v>><^vv<^>><<v<<^<>vvv>><<v><>>^<^>^^^^v><><><><v<<>^>vv^v>vv<v^<<>v<>^<^>^<<^^vv^<v<v<<>^vv><^^<^v>^vv^v><^^><<vv<v^^>vv><<^<<>v^>><v>v>^vvv><^>><>^>>vv^v>vv^<>v<<v<>^<<<><<v<><v^<^v<<vvv^^>><><^vvvv<>><><v^v<><>>>><<>v<vv^>vv>>^^>^<^>^<^><v<>vv<^v>v^<^v>>v<v>^^>>>vv><v>v^<vv<><vv>^>><>>v>vv<<<^<<<>v^<>><v<^>vv>^<<>^>^^>>^v>^><>^<^^v<vv<>v<<^^<><^<<<v>^^^^^v>^^^vv^v<<v^^^>>><<<>>v>>v<v>v>>v^<^^vv^<v^<v^<^vv<^>^>>vv^<<vv>v<><^v^>^><>><v^<<^<<<<>^>>^vv^<<><v<^><^>^>v>><v<v>^v^<vv>><vv><>v<v><^^v<^><<^^><^^><v><<>>v>><v>>><<^v^^^vv>^>v><^<^>>><<<>v<^>v<^<<<>^^><^v^<v^vv^>^>^^<^>><<v>vv>^>><v^<^vv^vvv^><v>vv>^<^vv<v>>v<^>>>>^^^>vv^^<^vv<vvv>v^>^>>^><>><v<>v<>vv>^^v><v><v^<v^<<><^<^v<<>><<<^vv><^^vv^>v<^v>^<><^>^><><^<>vv>vv^<vv<^>^<vv><>>>v<<v^v>v^^>vvv>vvvv^<<>>v><<>v><><v><<>^^<<>><>^^>v<<v<<>>><^^vv<vv^<vvv>v<^<<v<<^^^^v^vvvv>v^<<^vvv^vv^v^>>>v^v^v<v><<vv><^<vv^vvv^v^^v\r\nv>v^<v>>^v^vv^<<v^<^vv^vvv<>>^<><v^<<^<v^<<v<^><>^<<^><>^^^^><>>vv<^v><^^<^<v<>vv>v^^>vv^>^<v>>^>>v>^<<^^<<^>>^>v>v<<>^^^v^^>^^>vvv^><<v<v<<^>><>^><>>^^<>>><^^>><^<vv>>v<v^<<v<^<>^<^>>v^^<v^>^<v^vvv>>><^>^<^^v<<^^v><v<>vv>^^^>><>v<<>v><>^>vv>>>v<^><>v>v>^v<^><^><v<vv><v^<^>v>^^vv^^>>v<><>>>>^v^^v>^><>vv<<vv^vvvv><vv>^><v<v><v^>^^v>>v^<<^^v^>^^v^v>><<>v<^^^^><<<>^<>>v>vv^^^>^v^v^>><>>v>^v>vvv>^>v<<^^>v><<^^>v>^<^<vv>v>^><><><>>><vv<<<<>^>><v><v^v>>>v^^^^<v<^><v<^^><v>v>vvv^^<vv<v^v^v>v^><v^^^v>v>><<v^>>^v<^^<<<v>><<v^^v^<^<>^>v<><>vvv>v<>>vv<^vv>vv^^<v>>>^<^<<>vv<<vv<><>^^v<v>vv<v<>v^<<v><v^><<>^^^<^^<<<<v^^<vv>>v^vvvvv<vvv><>v^<v<vv<<^>><^^v^<<<v^>v>vv>>v<v>^>^^v^v^v^vv^<>^>>vv>^>v^^^>v^<<><vv^><<^vvv>^v><v<<^>>>^v^>>>vv^<v^>vv><^^>v>>^>><v><^<>^><^>><^v<^<<<^^>v>v>v<v^v<^v>v<^^<<v<v<^^<<v>^v>vv>>>vv^<>^vv^<<^v^>^>><<v^^<><<^<vv<^<v><v^>><><><v>>v>>>v>^^^^<v>vv><v<vv^>vv^>><v<vv<^vv^>v<^><v>>>>v<^<^^^vv>v>>>^^^<>><v^>v^v^<<>><>>^^>>^<<><^>^<^^<><<^v>^>>^>>^>>^^^>><<v<v<>v^<>>vv>vv^<>^^\r\n^<><vvv><<<><>^vv<^^>^>^^^^<v^^>^<>>v^<>v^<<^<v>>>^><^><>^^<^<^^v^>v<^v<><<vv^>^<^><<^<<><<>v^v^v><>>>v><>^><>><v<<v<v<v^^<<v>vv>v^v<>>^<><v^><^<<<<^^>^^<<^v>><^v^^^<v^^><>^^>^>^^><<^>^^^^<v<<^^><^<^v^>^v^v<>^>v^vv>^v<^<><><<<<v<v^^>>>^^^v<^v^<>v^^>^<v>v<^^^v>vv^<>vv<v^>v<^<<^>^<>^^<>^^<><v>v<>vv^>vv<<<>><v><<<v^><<^>>>v>v>vv^^>>v^><v<v^vvv>><vv>>><<^><vvvvv<<>>vv^><>^<<^^<v>^v<<^v^^<<v<v<>^<^<v<^<>v<v<<>>^^>><v>v^vv^<^>>>^>v><>v<^>>v<^>v>v>>^^<^>^v^>v^v<v^v<>>vv^^<^vv<><<v<^<^^>^><<>^<v^><^v^<>>^>v>^^>>>^^><>v<vv^v>vv<v><^vv^v>v^<<><<<v^<^<>><^^<^>^^><<<^v<^^vv^^>>v<v>^v>^^<<<v>^^<v<^^v><^>><^^<<^v^^>^^^^<^v>v<><<<vv>v<v<vv>^^^<<^><v^v^^<^^<<>v>v>v<>v^v>vv^<>v^<>v>v>^<><v^>><vv<>vv><vv^><<^^<^^vvv>>>><vv<>>^>v<v<<<>>^v<>^>>><vv^<^vv<v^^v>>v^v<^>v>v^<v<^v^>>>><<^<v^^<^v<>><<>^<^<<^>><>>>^v<<>v^^>v^<vvv^v<<v<v^v>^vv^>^<v><^^v><<<^^vv<v><vv><^>>^><<^vv<>>vv^<>v^><>vv>^<vv<v^<^><>>v>^><<>v<^^<^v>v>>^^^<^>><>^<^v^><<^v<^^v^v<v>v>>>v><^>v<v<v^<^^vvv^^v>><vvv<^v><>v^^<<<>^^>>^v^^<^v>>>^vvv^>\r\n<<^>^<<>^^><>^v>vv<>vv<v>^v^^>>^v^>v^<<^v^>v^<v>>vv>v^<^<><^^v<>>>^<<<^<v>v^vvv<<v><v<<><<^>vv^v><>>>v><^v>v<vv<><<>^^^v<^v>>v^v>^^vvv>>vv><^v>^<^v<>>><^<<><v>v^><<<v^<>v>^v>^<<^vv<v^<^<v^^<>^<^v<^^^^^vv^v<>>v^v<><v^^v<v<^<<><^vv><^^^v<v^<<><>>>^<<^>^v^<>v^vv^^<<v<>><v><<<><v<<^><<^>><^><^v<v<vv<^<>vv^^>>v>v>^<>>v^>v<>v<<<>vv<<>>>v><><><<v<>>^^^<>^<^^vv><v^^^>v<>^^^v>v^^<^v^v<v><^>^v^^>>^^v>>^>>v^vv^v^<v<v^v^<>><>^>>v^<^^><<<^<<<vv><v><>>^>>^v>^v>v>^<>>>^v<<^^<^v^^><^>>v^><<v<>>>v<<<><vv<^^>>>v><<^^^<^><>v>^<^>>v<^^<^<<v<>>v><>vv^v><<<vv^>^><^>>vvv^>>>vv>v<>^<>v>^>>v<>>v<<<<v>v<v^v<>v^>>^>><^^^vv<v^>^>^>><<><v^^<><<vv^<<^^vv^v^v>^<><<<><<^><<>><v^^<v>><^<<><v^<><v>vvv^v><<v^v>^^v><^>^>vv<>^<v><v<>^><><^>>^>>v^<>v^^^<>^>^><<^^<>>v<<^^>v^><<v>^<<^><<^>^<<<><<>>^<<<><>^><<<^>v<^<<^vvv^>vv^v><v><v>vv^>^>vv<vvvv^v<>v><<vv<<<>^vv^^v<^>v>^^>v^^^^^v<<<<^^^>^^^>^^<^>^^<<^<<<><v>^>v><<v^v^<<vv>v<<^vv^v^<>^<^^><<^<>>^^vvv^<<^<<><<v<vv^<^v<><>v^>>>v^v<>>^>>>vvv<v><<v^v^v>^v><^v>^v^><<^^v>v>^<<^>^v\r\n";
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

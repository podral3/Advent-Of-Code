using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day9
{
    public class Day9_2
    {
        public long Run() //not very proud of this 
        {
            long sum = 0;
            string input = "2333133121414131402";
            input = File.ReadAllText("C:\\Users\\Podral3\\source\\repos\\Advent of Code 2024\\Day9\\input.txt");

            int[] result = input.Select(c => (int)char.GetNumericValue(c)).ToArray(); //last item is a file
            Console.WriteLine(IsAFile(result.Length - 1));
            int left = 0; int right = result.Length-1;
            List<Disk> list = new List<Disk>();
            while (left < result.Length)
            {
                if (IsAFile(left) && result[left] != -1) //if free space
                {
                    Disk disk = new (0, left / 2, result[left]);
                    list.Add(disk);
                    left++;
                }
                else
                {
                    Disk disk = new (result[left]);
                    list.Add(disk);
                    left++;
                }
            }
            for(int i = result.Length - 1; i >= 0; i-=2)
            {
                Disk fileToBeMoved = list[i];
                if (fileToBeMoved.FileIdAndSpaceTakenList.Count > 1) break;
                for(int j = 0; j < i; j++)
                {
                    Disk disk = list[j];
                    if(disk.FreeSpace >= fileToBeMoved.FileIdAndSpaceTakenList[0].Item2)
                    {
                        disk.AddFile(fileToBeMoved.FileIdAndSpaceTakenList[0].Item1, fileToBeMoved.FileIdAndSpaceTakenList[0].Item2);
                        list[i].FreeSpace += fileToBeMoved.FileIdAndSpaceTakenList[0].Item2;
                        list[i].FileIdAndSpaceTakenList.Clear();
                        break;
                    }
                }
            }
            List<string> nums = new List<string>();
            foreach (Disk disk in list)
            {
                foreach (var file in disk.FileIdAndSpaceTakenList)
                {
                    for (int i = 0; i < file.Item2; i++)
                        nums.Add(file.Item1.ToString());
                }
                for (int i = 0; i < disk.FreeSpace; i++)
                    nums.Add(".");
            }
            foreach (var c in nums)
            {
                Console.Write(c);
            }
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] != ".")
                {
                    int num = int.Parse(nums[i]);
                    sum += num * i;
                } 
            }
            return sum;

        }

        public bool IsAFile(int idx) => idx % 2 == 0;
    }

    public class Disk
    {
        public int FreeSpace;
        public List<(int, int)> FileIdAndSpaceTakenList = new List<(int, int)> ();
        public Disk(int freeSpace)
        {
            FreeSpace = freeSpace;
        }
        public Disk(int freespace, int fileID, int spaceToOccupy)
        {
            FreeSpace = 0; //this does not secure passing file lager than disk space, hopefully i wont need that
            FileIdAndSpaceTakenList.Add((fileID, spaceToOccupy));
        }

        public bool AddFile(int fileId, int spaceToOccupy)
        {
            if(spaceToOccupy <= FreeSpace)
            {
                FileIdAndSpaceTakenList.Add((fileId, spaceToOccupy));
                FreeSpace -= spaceToOccupy;
                return true;
            }
            return false;
        }
    }
}

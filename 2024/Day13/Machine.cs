using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Guard.Day13
{
    public class Machine
    {
        public Instuction Instruction1;
        public Instuction Instruction2;
        public (int, int) Prize;
        public Machine(string[] instructions)
        {
            ParseInstuctions(instructions);
        }
        public int Solve()
        {
            //Cramer
            //X = A button presses  Y = B button presses
            int a1 = Instruction1.X_Addition; int b1 = Instruction2.X_Addition;
            int a2 = Instruction1.Y_Addition; int b2 = Instruction2.Y_Addition;
            int c1 = Prize.Item1; int c2 = Prize.Item2;

            int D = (a1 * b2) - (a2 * b1); //Determinant of coeff matrix
            if (D == 0) return 0;

            //Coefficients of each variable
            int DX = (c1 * b2) - (c2 * b1); //replace col 1 with c
            int DY = (a1 * c2) - (a2 * c1); //replace col 2 with c
            double zero = 0;
            //Number of button presses
            double A_Button_Count = (double)DX / D;
            double B_Button_Count = (double)DY / D;
            if((A_Button_Count % 1 != zero || B_Button_Count % 1 != zero)) //if button presses are not whole numbers this means there is no solution
                return 0;

            if (A_Button_Count > 100 || B_Button_Count > 100 || A_Button_Count < 0 || B_Button_Count < 0) 
                return 0;
            return (int)(3 * A_Button_Count + B_Button_Count);

        }

        private void ParseInstuctions(string[] instructions)
        {
            Instruction1 = new (instructions[0],3);
            Instruction2 = new (instructions[1],1);
            var splitByTwoDots = instructions[2].Split(':');
            var splitByColon = splitByTwoDots[1].Split(',');
            Prize.Item1 = int.Parse(splitByColon[0].Substring(splitByColon[0].IndexOf('=') + 1));
            Prize.Item2 = int.Parse(splitByColon[1].Substring(splitByColon[1].IndexOf('=') + 1));
        }
    }

    public class Instuction
    {
        public int Cost;
        public int X_Addition;
        public int Y_Addition;
        public Instuction(string line, int cost)
        {
            Cost = cost;
            var splitByTwoDots = line.Split(':');
            var splitByColon = splitByTwoDots[1].Split(',');
            X_Addition = int.Parse(splitByColon[0].Substring(splitByColon[0].IndexOf('+')+1));
            Y_Addition = int.Parse(splitByColon[1].Substring(splitByColon[1].IndexOf('+') + 1));
        }
    }
}

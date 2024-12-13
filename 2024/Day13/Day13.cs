using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guard.Day13
{
    public class Day13
    {
        public long Run()
        {
            string filePath = "D:\\MP\\Guard\\Guard\\Day13\\input.txt"; 
            List<Machine> machines = new List<Machine>();
            int sum = 0;
            long sum2 = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                string[] arr = new string[3]; int i = 0;
                while ((line = reader.ReadLine()) != null && line != "!")
                {
                    if(line == "")
                    {
                        var m = new Machine(arr);
                        //Console.WriteLine(m.Solve());
                        sum += m.Solve();
                        sum2 += m.Solve2();
                        arr = new string[3]; i = 0;
                        continue;
                    }
                    arr[i] = line;
                    i++;
                }
            }
            return sum2;
        }
    }
    
}

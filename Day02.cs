using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day02 
    {
        public void SolveA() 
        {
            int count2 = 0;
            int count3 = 0;

            foreach (string line in File.ReadLines("02_input.txt"))
            {
                IDictionary<char, int> dict = new Dictionary<char, int>();
                foreach (char c in line.ToCharArray())
                {
                    if (dict.ContainsKey(c)) 
                      dict[c] += 1;
                    else 
                      dict.Add(c, 1);
                }

                if (dict.Values.Any(v => v == 2))
                  count2++;

                if (dict.Values.Any(v => v == 3))
                  count3++;
            }

            Console.WriteLine("Day02 A: " + (count2 * count3));  //= 5952
        }

        public void SolveB()
        {
            var lines = File.ReadLines("02_input.txt")
                            .OrderBy(s => s)
                            .ToList();

            int maxIdx     = lines.Count() - 1;
            int maxCharIdx = lines[0].Length;

            for (int i=0; i<(maxIdx-1); i++)
            {
                char[] line1 = lines[i].ToCharArray();
                for (int j=i+1; j<maxIdx; j++) 
                {
                    char[] line2 = lines[j].ToCharArray();
                    int mismatches = 0;
                    for (int a=0; a<(maxCharIdx-1); a++)
                    {
                        if (line1[a] != line2[a])
                            mismatches++;
                        
                        if (mismatches > 1)
                            break;
                    }

                    if (mismatches == 1) {
                        Console.Write("Day02 B: ");
                        for (int c=0; c<maxCharIdx-1; c++) 
                        {
                            if (line1[c] == line2[c])
                              Console.Write(line1[c]);
                        }
                    }
                }
            }

            Console.Write("\n");
        }
    }
}
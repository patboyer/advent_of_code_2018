using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day16
    {
        private static Func<int[], int, int, int> addr = (int[] reg, int in1, int in2) => reg[in1] + reg[in2];
        private static Func<int[], int, int, int> addi = (int[] reg, int in1, int in2) => reg[in1] + in2;
        private static Func<int[], int, int, int> mulr = (int[] reg, int in1, int in2) => reg[in1] * reg[in2];
        private static Func<int[], int, int, int> muli = (int[] reg, int in1, int in2) => reg[in1] * in2;
        private static Func<int[], int, int, int> banr = (int[] reg, int in1, int in2) => reg[in1] & reg[in2];
        private static Func<int[], int, int, int> bani = (int[] reg, int in1, int in2) => reg[in1] & in2;
        private static Func<int[], int, int, int> borr = (int[] reg, int in1, int in2) => reg[in1] | reg[in2];
        private static Func<int[], int, int, int> bori = (int[] reg, int in1, int in2) => reg[in1] | in2;
        private static Func<int[], int, int, int> setr = (int[] reg, int in1, int in2) => reg[in1];
        private static Func<int[], int, int, int> seti = (int[] reg, int in1, int in2) => in1;
        private static Func<int[], int, int, int> gtir = (int[] reg, int in1, int in2) => (in1 > reg[in2]) ? 1 : 0;
        private static Func<int[], int, int, int> gtri = (int[] reg, int in1, int in2) => (reg[in1] > in2) ? 1 : 0;
        private static Func<int[], int, int, int> gtrr = (int[] reg, int in1, int in2) => (reg[in1] > reg[in2]) ? 1 : 0;
        private static Func<int[], int, int, int> eqir = (int[] reg, int in1, int in2) => (in1 == reg[in2]) ? 1 : 0;
        private static Func<int[], int, int, int> eqri = (int[] reg, int in1, int in2) => (reg[in1] == in2) ? 1 : 0;
        private static Func<int[], int, int, int> eqrr = (int[] reg, int in1, int in2) => (reg[in1] == reg[in2]) ? 1 : 0;

        private List<Func<int[], int, int, int>> instr = new List<Func<int[], int, int, int>>() {
            bani, addr, mulr, addi,   
            gtri, banr, borr, eqri,  
            seti, eqrr, bori, setr, 
            eqir, muli, gtrr, gtir
        };

        public int[] ExecInstr(int[] reg, int cmd, int in1, int in2, int output)
        {
            int[] result = { 0, 0, 0, 0 };
            Array.Copy(reg, 0, result, 0, 4);
            result[output] = instr[cmd](reg, in1, in2);
            return result;
        }

        public void SolveA()
        {
            int cmd    = 0;
            int input1 = 0;
            int input2 = 0;
            int output = 0;
            int result = 0;

            int[] before    = { 0, 0, 0, 0 };
            int[] after     = { 0, 0, 0, 0 };
            int[] cmdResult = { 0, 0, 0, 0 };

            foreach (string line in File.ReadLines("16A_input.txt"))
            {
                string[] delimiters = { " ", ",", "[", "]", ":" };
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0) 
                {
                    continue;
                }
                else if (parts[0] == "Before")
                {
                    before = parts.Skip(1).Take(4).Select(s => int.Parse(s)).ToArray();
                }
                else if (parts[0] == "After")
                {
                    after = parts.Skip(1).Take(4).Select(s => int.Parse(s)).ToArray(); 

                    int numPossible = Enumerable.Range(0, instr.Count())
                        .ToList()
                        .Where(c => {
                            cmdResult = ExecInstr(before, c, input1, input2, output);
                            return Enumerable.SequenceEqual(cmdResult, after);
                        })
                        .Count();

                    if (numPossible >= 3)
                        result++;
                }
                else
                {
                    cmd    = int.Parse(parts[0]);
                    input1 = int.Parse(parts[1]);
                    input2 = int.Parse(parts[2]);
                    output = int.Parse(parts[3]);
                }
            }

            Console.WriteLine("Day 16 A: " + result);  //= 596
        }

        public void SolveB()
        {
            int[] reg = { 0, 0, 0, 0 };

            foreach (string line in File.ReadLines("16B_input.txt"))
            {
                string[] delimiters = { " ", ",", "[", "]", ":" };
                int[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(s => int.Parse(s))
                                  .ToArray();

                reg = ExecInstr(reg, parts[0], parts[1], parts[2], parts[3]);
            }

            Console.WriteLine("Day 16 B: " + reg[0]);  //= 
        }
    }
}

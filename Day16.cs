using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day16
    {
        public class CPU
        {
            private int[] _registers = { 0, 0, 0, 0 };

            public void Set(int[] src)
            {
                Array.Copy(src, 0, _registers, 0, 4);
            }

            public int[] Get()
            {
                return _registers;
            }

            public void addr(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] + _registers[input2];
            }

            public void addi(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] + input2;
            }

            public void mulr(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] * _registers[input2];
            }

            public void muli(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] * input2;
            }

            public void banr(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] & _registers[input2];
            }

            public void bani(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] & input2;
            }

            public void borr(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] | _registers[input2];
            }

            public void bori(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1] | input2;
            }

            public void setr(int input1, int input2, int output)
            {
                _registers[output] = _registers[input1];
            }

            public void seti(int input1, int input2, int output)
            {
                _registers[output] = input1;
            }

            public void gtir(int input1, int input2, int output)
            {
                _registers[output] = (input1 > _registers[input2]) ? 1 : 0;
            }

            public void gtri(int input1, int input2, int output)
            {
                _registers[output] = (_registers[input1] > input2) ? 1 : 0;
            }

            public void gtrr(int input1, int input2, int output)
            {
                _registers[output] = (_registers[input1] > _registers[input2]) ? 1 : 0;
            }

            public void eqir(int input1, int input2, int output)
            {
                _registers[output] = (input1 == _registers[input2]) ? 1 : 0;
            }

            public void eqri(int input1, int input2, int output)
            {
                _registers[output] = (_registers[input1] == input2) ? 1 : 0;
            }

            public void eqrr(int input1, int input2, int output)
            {
                _registers[output] = (_registers[input1] == _registers[input2]) ? 1 : 0;
            }
        }

        public void SolveA()
        {
            int input1 = 0;
            int input2 = 0;
            int output = 0;
            int result = 0;
            int[] before = { 0, 0, 0, 0 };
            int[] after  = { 0, 0, 0, 0 };
            CPU cpu = new CPU();

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
                    int numPossible = 0;
                    after = parts.Skip(1).Take(4).Select(s => int.Parse(s)).ToArray(); 

                    cpu.Set(before); cpu.addr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.addi(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.mulr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.muli(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.banr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.bani(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.borr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.bori(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.setr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.seti(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.gtir(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.gtri(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.gtrr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.eqir(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.eqri(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;
                    cpu.Set(before); cpu.eqrr(input1, input2, output); 
                    if (Enumerable.SequenceEqual(cpu.Get(), after)) numPossible++;

                    if (numPossible >= 3)
                        result++;
                }
                else
                {
                    input1 = int.Parse(parts[1]);
                    input2 = int.Parse(parts[2]);
                    output = int.Parse(parts[3]);
                }
            }

            Console.WriteLine("Day 16 A: " + result);  //= 596
        }

        public void SolveB()
        {
            int result = 0;
            Console.WriteLine("Day 16 B: " + result);  //= 
        }
    }
}

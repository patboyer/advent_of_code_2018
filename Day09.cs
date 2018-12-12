using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day09
    {
        public int Solve(int numPlayers, int lastMarble)
        {
            List<int> marbles = new List<int>() { 0 };
            List<int> players = new List<int>();
            int currentMarble = 1;
            int marbleIdx     = 0;
            int length        = 1;

            for (int i=0; i<numPlayers; i++)
            {
                players.Add(0);
            }

            while (currentMarble <= lastMarble)
            {
                if (currentMarble % 23 == 0)
                {
                    marbleIdx = (marbleIdx >= 7)
                        ? marbleIdx - 7
                        : marbleIdx + length - 7;
                    
                    int playerIdx = currentMarble % numPlayers;
                    players[playerIdx] += marbles[marbleIdx] + currentMarble;
                    marbles.RemoveAt(marbleIdx);
                    length--;
                }
                else 
                {
                    marbleIdx += 2;

                    if (marbleIdx < length)
                        marbles.Insert(marbleIdx, currentMarble);
                    else if (marbleIdx == length)
                        marbles.Add(currentMarble);
                    else if (marbleIdx == (length+1))
                    {
                        marbleIdx = 1;
                        marbles.Insert(marbleIdx, currentMarble);
                    }

                    length++;
                }

                currentMarble++;
            }

            return players.Max();
        }

        public void SolveA()
        {
            int numPlayers = 416;
            int lastMarble = 71617;
            Console.WriteLine("Day 09 A: " + Solve(numPlayers, lastMarble));  //= 436720

        }

        public void SolveB()
        {
            int numPlayers    = 416;
            int lastMarble    = 71617 * 100;
            Console.WriteLine("Day 09 B: " + Solve(numPlayers, lastMarble));  //= 
        }
    }
}

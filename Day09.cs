using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day09
    {
        public int SolveByArray(int numPlayers, int lastMarble)
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
            int numPlayers = 9;//416;
            int lastMarble = 32; //71617;
            Console.WriteLine("Day 09 A: " + SolveByArray(numPlayers, lastMarble));  //= 436720
        }

        class Node
        {
            public Node Prev;
            public Node Next;
            public uint Data;

            public Node(uint p_data)
            {
                Data = p_data;
                Prev = null;
                Next = null;
            }
        }

        public class MarbleCircle 
        {
            private Node _current;
            private Node _first;
            private Node _last;

            public MarbleCircle()
            {
                _first = _last = _current = new Node(0);
            }

            public void AddAfter(uint p_data)
            {
                Node n = new Node(p_data);
                n.Next = _current.Next;
                n.Prev = _current;
                _current.Next = n;
                _current = n;

                if (_current.Next == null)
                {
                    _last = _current;
                }
                else
                {
                    _current.Next.Prev = n;
                }
            }

            public uint GetData()
            {
                return _current.Data;
            }

            public void MoveBackward(int distance)
            {
                for (int i=0; i<distance; i++)
                    _current = (_current == _first) ? _last : _current.Prev;
            }

            public void MoveForward(int distance)
            {
                for (int i=0; i<distance; i++)
                    _current = (_current == _last) ? _first : _current.Next;
            }

            public void Remove()
            {
                if (_current == _last)
                {
                    _last = _current.Prev;
                    _current = _first;
                }
                  
                else if (_current == _first)
                {
                    _current = _first = _current.Next;
                }
                else 
                {
                    _current.Next.Prev = _current.Prev;
                    _current.Prev.Next = _current.Next;
                    _current = _current.Next;
                }
            }
        }

        public uint SolveByDoubleLinkedList(int numPlayers, uint lastMarble)
        {
            MarbleCircle marbles = new MarbleCircle();
            uint currentMarble = 1;

            List<uint> players = new List<uint>();
            for (int i=0; i<numPlayers; i++)
            {
                players.Add(0);
            }

            while (currentMarble <= lastMarble)
            {
                if (currentMarble % 23 == 0)
                {
                    marbles.MoveBackward(7);                    
                    int playerIdx = (int)(currentMarble % numPlayers);
                    players[playerIdx] += marbles.GetData() + currentMarble;
                    marbles.Remove();
                }
                else
                {
                    marbles.MoveForward(1);
                    marbles.AddAfter(currentMarble);
                }

                currentMarble++;
            }         

            return players.Max();   
        }

        public void SolveB()
        {
            int numPlayers  = 416;
            uint lastMarble = 71617 * 100;
            Console.WriteLine("Day 09 B: " + SolveByDoubleLinkedList(numPlayers, lastMarble));  //= 3527845091
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018
{
    class Day04
    {
        private class Guard 
        {
            public int id;
            public IDictionary<int, int> sleep;
            public DateTime sleepStart;

            public Guard(int p_id) 
            {
                id    = p_id;
                sleep = new Dictionary<int, int>();
                sleep.Add(0, 0);
            }

            public void BeginSleep(DateTime dt) 
            {
                sleepStart = dt;
            }

            public void EndSleep(DateTime dt2)
            {
                int minStart = sleepStart.Minute;
                int minEnd   = dt2.Minute;

                for (int i=minStart; i<minEnd; i++)
                {
                    if (sleep.ContainsKey(i))
                        sleep[i] += 1;
                    else 
                        sleep.Add(i, 1);
                }
            }

            public int SleepDuration() 
            {
                return sleep.Values.ToList().Sum();
            }

            public int SleepiestMinute()
            {
                return sleep.OrderByDescending(d => d.Value)
                            .Select(d => d.Key)
                            .First();
            }

            public int SleepiestMinuteFrequency()
            {
                return sleep[this.SleepiestMinute()];
            }
        }

        private IDictionary<int, Guard> guards = new Dictionary<int, Guard>();

        public void SolveA()
        {
            int GuardID = 0;

            foreach (string line in File.ReadLines("04_input.txt").OrderBy(s => s))
            {
                string[] delimiters = { "[", "]", " ", "#" };
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                DateTime dt = DateTime.ParseExact((parts[0] + " " + parts[1]), "yyyy-MM-dd HH:mm", null);
                string cmd  = parts[2];

                if (cmd == "Guard")
                {
                  GuardID = int.Parse(parts[3]);

                  if (! guards.ContainsKey(GuardID))
                  {
                      guards.Add(GuardID, new Guard(GuardID));
                  }
                }
                else if (cmd == "wakes")
                {
                    guards[GuardID].EndSleep(dt);
                }
                else if (cmd == "falls")
                {
                    guards[GuardID].BeginSleep(dt);
                }
            }

            Guard guard = guards.Values
                                .ToList()
                                .OrderByDescending(g => g.SleepDuration())
                                .First(); 

            int minute = guard.SleepiestMinute();
            int result = guard.id * minute;
            Console.WriteLine("Day04 A: " + result);  //= 99911
        }

        public void SolveB()
        {
            int GuardID = 0;

            foreach (string line in File.ReadLines("04_input.txt").OrderBy(s => s))
            {
                string[] delimiters = { "[", "]", " ", "#" };
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                DateTime dt = DateTime.ParseExact((parts[0] + " " + parts[1]), "yyyy-MM-dd HH:mm", null);
                string cmd  = parts[2];

                if (cmd == "Guard")
                {
                  GuardID = int.Parse(parts[3]);

                  if (! guards.ContainsKey(GuardID))
                  {
                      guards.Add(GuardID, new Guard(GuardID));
                  }
                }
                else if (cmd == "wakes")
                {
                    guards[GuardID].EndSleep(dt);
                }
                else if (cmd == "falls")
                {
                    guards[GuardID].BeginSleep(dt);
                }
            }

            Guard guard = guards.Values
                                .ToList()
                                .OrderByDescending(g => g.SleepiestMinuteFrequency())
                                .First(); 

            int minute = guard.SleepiestMinute();
            int result = guard.id * minute;
            Console.WriteLine("Day04 B: " + result);  //= 65854
        }
    }
}

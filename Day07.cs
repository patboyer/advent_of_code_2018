using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day07
    {
        public class Node: IComparable<Node>
        {
            public char name;
            public List<Node> parents;
            public List<Node> children;

            public Node(char p_name)
            {
                name     = p_name;
                parents  = new List<Node>();
                children = new List<Node>();
            }

            public override string ToString()
            {
                return name.ToString();
            }

            public override bool Equals(object obj)
            {
                var other = obj as Node;
                return (name == other.name);
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public int CompareTo(Node other)
            {
                return name.CompareTo(other.name);
            }
        }

        class Worker
        {
            public bool IsAvailable;
            private Node _job;
            private int _duration;
            private int _time;

            public Worker()
            {
                IsAvailable = true;
            }

            public void AssignWork(Node p_job, int p_duration)
            {
                IsAvailable = false;
                _job        = p_job;
                _duration   = p_duration;
                _time       = 0;
            }

            public Node DoWork()
            {
                _time++;

                if (_time == _duration)
                {
                    IsAvailable = true;
                    return _job;
                }
                else
                { 
                    return null;
                }
            }
        }

        public List<Node> ParseFile(string filename)
        {
            IDictionary<char, Node> nodes = new Dictionary<char, Node>();

            foreach (string line in File.ReadLines(filename))
            {
                string[] parts = line.Split(" ");
                char parent  = parts[1][0];
                char child   = parts[7][0];

                if (! nodes.ContainsKey(parent))
                    nodes.Add(parent, new Node(parent));

                if (! nodes.ContainsKey(child))
                    nodes.Add(child, new Node(child));
                
                nodes[parent].children.Add(nodes[child]);
                nodes[child].parents.Add(nodes[parent]);
            }

            return nodes.Values.ToList();
        }

        public string foo(Node n)
        {
            return n.name + string.Join("", n.parents.OrderByDescending(node => node).Select(node => foo(node)));
        }

        public void SolveA()
        {
            List<Node> nodes = ParseFile("07_input.txt");
            Node start = nodes.Where(n => n.children.Count() == 0).First();
            Console.WriteLine(foo(start));
        }

        public void SolveB()
        {
            List<Node> nodes = ParseFile("07_input.txt");
            Project proj = new Project(nodes);
            proj.Execute(2, 1);
            Console.WriteLine("Day 07 B: " + proj.Duration);  //= 
        }
    }
}

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
            public bool ready;

            public Node(char p_name)
            {
                name     = p_name;
                parents  = new List<Node>();
                children = new List<Node>();
                ready    = false;
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

        public void SolveA()
        {
            List<Node> nodes = ParseFile("07_input.txt");
            List<Node> done  = new List<Node>();
            List<Node> todo  = nodes.Where(n => n.parents.Count() == 0).ToList();

            while (done.Count() < nodes.Count())
            {
                foreach (Node t in todo)
                {
                    t.ready = (t.parents.Where(p => !done.Contains(p)).Count() == 0);
                }

                Node job = todo.Where(n => n.ready).OrderBy(n => n).First();
                todo.Remove(job);
                done.Add(job);
                todo = todo.Union(job.children).ToList();
            }

            string result = string.Join("", done.Select(n => n.name));
            Console.WriteLine("Day 07 A: " + result);
        }

        public void SolveB()
        {
            List<Node> nodes = ParseFile("07_input.txt");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2018
{
    class Day08
    {
        public class Node
        {
            public List<Node> Children;
            public List<int>  Metadata;

            public Node() 
            {
                Children = new List<Node>();
                Metadata = new List<int>( 0 );
            }

            public int GetMetadata()
            {
                return Metadata.Sum() + Children.Select(n => n.GetMetadata()).Sum();
            }

            public int GetValue()
            {
                if (Children.Count>0)
                    return 0 + Metadata.Where(i => ( (i>0) && ((i-1)<Children.Count()) )) 
                                       .Select(i => Children[i-1].GetValue())
                                       .Sum();
                else 
                    return Metadata.Sum();
            }
        }

        public List<int> BuildTree(Node node, List<int> data)
        {
            int numChildren = data[0];
            data.RemoveAt(0);

            int numMetadata = data[0];
            data.RemoveAt(0);

            for (int c=0; c<numChildren; c++)
            {
                Node child = new Node();
                node.Children.Add(child);
                data = BuildTree(child, data);
            }

            for (int m=0; m<numMetadata; m++)
            {
                node.Metadata.Add(data[0]);
                data.RemoveAt(0);
            }

            return data;
        }

        public void SolveA()
        {
            List<int> data = File.ReadAllText("08_input.txt")
                                 .Split(" ")
                                 .Select(s => int.Parse(s))
                                 .ToList();

            Node root = new Node();
            data = BuildTree(root, data);
            Console.WriteLine("Day 08 A: " + root.GetMetadata());  //= 40746
        }

        public void SolveB()
        {
            List<int> data = File.ReadAllText("08_input.txt")
                                 .Split(" ")
                                 .Select(s => int.Parse(s))
                                 .ToList();

            Node root = new Node();
            data = BuildTree(root, data);
            Console.WriteLine("Day 08 B: " + root.GetValue());  //= 37453
        }
    }
}

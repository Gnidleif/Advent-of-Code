using System;
using System.Linq;
using System.Collections.Generic;

namespace C_
{
    public class Day01 : IDay
    {
        public List<int> Input { get; private set; }

        public Day01(string data)
        {
            if(data == null) 
            {
                throw new ArgumentNullException("data parameter can't be null");
            }

            if(data.Length == 0) 
            {
                throw new ArgumentException("data parameter can't be zero-length");
            }

            this.Input = new List<int>(data.Length);
            foreach(char c in data)
            {
                int temp;
                if(!int.TryParse(c.ToString(), out temp))
                {
                    throw new FormatException($"{c} is not a number");
                }
                this.Input.Add(temp);
            }
        }

        public int Part1() => (
            from i in Enumerable.Range(0, this.Input.Count) 
            where this.Input[i] == this.Input[(i + 1) % this.Input.Count] 
            select this.Input[i]
        ).Sum();

        public int Part2() => (
            from i in Enumerable.Range(0, this.Input.Count)
            where this.Input[i] == this.Input[(i + this.Input.Count / 2) % this.Input.Count]
            select this.Input[i]
        ).Sum();
    }
}
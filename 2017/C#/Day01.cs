using System;
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

        public int Part1()
        {
            int sum = 0;
            for (int i = 0; i < this.Input.Count; i++)
            {
                if (this.Input[i] == this.Input[(i + 1) % this.Input.Count])
                {
                    sum += this.Input[i];
                }
            }
            return sum;
        }

        public int Part2()
        {
            int sum = 0;
            for(int i = 0; i < this.Input.Count; i++)
            {
                if(this.Input[i] == this.Input[(i + this.Input.Count / 2) % this.Input.Count])
                {
                    sum += this.Input[i];
                }
            }
            return sum;
        }
    }
}
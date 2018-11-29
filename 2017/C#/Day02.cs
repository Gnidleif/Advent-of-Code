using System;
using System.Linq;
using System.Collections.Generic;

namespace C_
{
    public class Day02 : IDay
    {
        public List<List<int>> Input { get; private set; }
        
        public Day02(string data)
        {
            if(data == null) 
            {
                throw new ArgumentNullException("data parameter can't be null");
            }
            
            if(data.Length == 0) 
            {
                throw new ArgumentException("data parameter can't be zero-length");
            }

            string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            this.Input = new List<List<int>>(lines.Length);
            for(int i = 0; i < lines.Length; i++)
            {
                string[] split = lines[i].Split(null);
                this.Input.Add(new List<int>(split.Length));
                for(int j = 0; j < split.Length; j++)
                {
                    int temp;
                    if(!int.TryParse(split[j].ToString(), out temp))
                    {
                        throw new FormatException($"{split[j]} is not a number");
                    }
                    this.Input[i].Add(temp);
                }
            }
        }

        public int Part1()
        {
            int sum = 0;
            this.Input.ForEach(i => sum += (i.Max() - i.Min()));
            return sum;
        }

        public int Part2()
        {
            throw new System.NotImplementedException();
        }
    }
}
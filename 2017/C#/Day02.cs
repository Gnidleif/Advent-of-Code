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

        public int Part1() => this.Input.Sum(item => item.Max() - item.Min());

        public int Part2() => this.Input.Sum(item => this.FindDivision(item));

        private int FindDivision(List<int> item)
        {
            for(int i = 0; i < item.Count; i++)
            {
                for(int j = i + 1; j < item.Count; j++)
                {
                    if(item[i] % item[j] == 0)
                    {
                        return item[i] / item[j];
                    }
                    else if(item[j] % item[i] == 0)
                    {
                        return item[j] / item[i];
                    }
                }
            }
            return 0;
        }
    }
}
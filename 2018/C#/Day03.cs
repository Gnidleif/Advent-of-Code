using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class Day03 : IDay
    {
        private class Claim
        {
            public int Id { get; set; }
            public HashSet<Tuple<int, int>> Coords { get; set; }

            public Claim(int id, int left, int top, int width, int height)
            {
                this.Id = id;
                this.Coords = new HashSet<Tuple<int, int>>(width * height);
                for (int x = left; x < left + width; x++)
                {
                    for (int y = top; y < top + height; y++)
                    {
                        this.Coords.Add(new Tuple<int, int>(x, y));
                    }
                }
            }
        }

        private List<Claim> Claims { get; set; }

        public Day03(string input)
        {
            string[] lines = input.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None);
            this.Claims = new List<Claim>(lines.Length);
            foreach (string line in lines)
            {
                var nums = line.Split(
                    new[] { '#', '@', ',', ':', 'x', ' ' },
                    StringSplitOptions.RemoveEmptyEntries
                ).Select(int.Parse).ToList();

                this.Claims.Add(new Claim(
                    nums[0], // id
                    nums[1], // left
                    nums[2], // top
                    nums[3], // width
                    nums[4] // height
                ));
            }
        }

        public int Part1()
        {
            var points = new Dictionary<Tuple<int, int>, int>();
            this.Claims.ForEach(item =>
            {
                foreach (var coord in item.Coords)
                {
                    if (!points.ContainsKey(coord))
                    {
                        points.Add(coord, 0);
                    }
                    points[coord]++;
                }
            });
            int total = 0;
            foreach (KeyValuePair<Tuple<int, int>, int> pair in points)
            {
                if (pair.Value > 1)
                {
                    total++;
                }
            }
            return total;
        }

        public int Part2()
        {
            int id = 0;
            foreach (var item in this.Claims)
            {
                var doesOverlap = false;
                foreach (var other in this.Claims)
                {
                    if (item.Id == other.Id)
                    {
                        continue;
                    }
                    if (item.Coords.Overlaps(other.Coords))
                    {
                        doesOverlap = true;
                        break;
                    }
                }
                if (!doesOverlap)
                {
                    id = item.Id;
                    break;
                }
            }
            return id;
        }
    }
}
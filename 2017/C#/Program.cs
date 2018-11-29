using System;

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Day01("1122").Part1());
            Console.WriteLine(new Day01("1212").Part2());

            Console.WriteLine(new Day02(@"5 1 9 5
7 5 3
2 4 6 8").Part1());
        }
    }
}

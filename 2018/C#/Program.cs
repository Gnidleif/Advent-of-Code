using System;
using System.Diagnostics;
using System.IO;

namespace AoC
{
    class Program
    {
        static string ReadFile(string filename)
        {
            string text = string.Empty;
            try 
            {
                using(var sr = new StreamReader(filename))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine($"Error reading file:\n{e.ToString()}");
            }
            return text;
        }

        static void Main(string[] args)
        {
            var tmp = ReadFile("Day03.txt");
            var d = new Day03(tmp);
            Console.WriteLine(d.Part1());
            Console.WriteLine(d.Part2());
        }
    }
}

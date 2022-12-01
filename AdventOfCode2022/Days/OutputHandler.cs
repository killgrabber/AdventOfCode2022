using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public static class OutputHandler
    {
        public static void Run(IDays day, int part)
        {
            Stopwatch stopwatch = new Stopwatch();
            switch (part)
            {
                case 1:
                    Console.WriteLine("PartOne:");
                    Console.WriteLine("------------------------------------------------");
                    stopwatch.Start();
                    day.PartOne();
                    stopwatch.Stop();
                    Console.WriteLine($"Excecution Time: {stopwatch.Elapsed}");
                    break;
                case 2:
                    Console.WriteLine("PartTwo:");
                    Console.WriteLine("------------------------------------------------");
                    stopwatch.Start();
                    day.PartTwo();
                    stopwatch.Stop();
                    Console.WriteLine($"Excecution Time: {stopwatch.Elapsed}");
                    break;
                case 3:
                    Console.WriteLine("PartOne:");
                    Console.WriteLine("------------------------------------------------");
                    stopwatch.Start();
                    day.PartOne();
                    stopwatch.Stop();
                    Console.WriteLine($"Excecution Time: {stopwatch.Elapsed}");

                    Console.WriteLine("\nPartTwo:");
                    Console.WriteLine("------------------------------------------------");
                    stopwatch.Reset();
                    stopwatch.Start();
                    day.PartTwo();
                    stopwatch.Stop();
                    Console.WriteLine($"Excecution Time: {stopwatch.Elapsed}");
                    break;
                default:
                    break;
            }
        }
    }
}

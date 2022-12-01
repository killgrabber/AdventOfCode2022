using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2022.Days {
  public class Day01 : IDays {
    public void PartOne() {
      List<int> sum = new List<int>();
      int zwischenSumme = 0;
      int lastMax = 0;
      Console.WriteLine("This is C#");
      string[] lines = File.ReadAllLines(@"../../../Inputs/input01.txt");
      foreach (string line in lines) {
        if (line.Length == 0) {
          sum.Add(zwischenSumme);
          zwischenSumme = 0;
          zwischenSumme = 0;
        } else {
          zwischenSumme += int.Parse(line);
        }
      }
      sum.Sort();
      Console.WriteLine(sum.ElementAt(sum.Count - 1));
      Console.WriteLine(sum.ElementAt(sum.Count - 2));
      Console.WriteLine(sum.ElementAt(sum.Count - 3));
      int sumsum = sum.ElementAt(sum.Count - 1) + sum.ElementAt(sum.Count - 2) + sum.ElementAt(sum.Count - 3);
      Console.WriteLine(sumsum);
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input01.txt");
    }
  }
}

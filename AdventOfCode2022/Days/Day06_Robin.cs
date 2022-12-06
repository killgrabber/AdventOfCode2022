using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day06_Robin : IDays {
    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input06.txt");
      int i = 0;
      List<char> save = new List<char>();
      foreach (string line in lines) {
        foreach (char c in line) {
          if (save.Contains(c)) {
            int index = save.IndexOf(c);
            save.RemoveRange(0, index + 1);
          } else if (save.Count == 4) {
            break;
          }
          save.Add(c);
          i++;
        }
      }

      Console.WriteLine($"Index: {i}");
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input06.txt");
      int i = 0;
      List<char> save = new List<char>();
      foreach (string line in lines) {
        foreach (char c in line) {
          if (save.Contains(c)) {
            int index = save.IndexOf(c);
            save.RemoveRange(0, index + 1);
          } else if (save.Count == 14) {
            break;
          }
          save.Add(c);
          i++;
        }
      }

      Console.WriteLine($"Index: {i}");
    }
  }
}

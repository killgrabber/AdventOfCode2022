using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day03 : IDays {

    int getCharValue(char v) {
      int value = 0;
      if (Char.IsLower(v)) {
        value = (int)v - 96;
      } else {
        value = (int)v - 38;
      }
      //Console.WriteLine($"{v}={value}");
      return value;
    }

    char containsBoth(string a, string b) {
      char item = 'ä';
      foreach (char c in a) {
        foreach (char c2 in b) {
          if (c == c2) {
            item = c; break;
          }
        }
        if (item != 'ä') {
          break;
        }
      }
      return item;
    }

    char containsTripple(string a, string b, string c) {
      char item = 'ä';
      foreach (char c1 in a) {
        foreach (char c2 in b) {
          foreach (char c3 in c) {
            if (c1 == c2 && c2 == c3) {
              item = c1; break;
            }
          }
          if (item != 'ä') {
            break;
          }
        }
        if (item != 'ä') {
          break;
        }
      }
      return item;
    }


    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input03.txt");
      int sum = 0;
      foreach (string l in lines) {
        string firstCompartment = l.Substring(0, l.Length / 2);
        string secondCompartment = l.Substring(l.Length / 2);
        char item = containsBoth(firstCompartment, secondCompartment);
        sum += getCharValue(item);
      }
      Console.WriteLine($"Summe: {sum}");
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input03.txt");
      int index = 0;
      int sum = 0;
      while(index < lines.Length ) {
        char item = containsTripple(lines[index], lines[index + 1], lines[index + 2]);
        sum += getCharValue(item);
        index += 3;
      }
      Console.WriteLine($"Summe: {sum}");
    }
  }
}

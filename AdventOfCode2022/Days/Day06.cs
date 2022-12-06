using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day06 : IDays {

    public bool checkDouble(List<char> markerString) {
      return markerString.Count() != markerString.Distinct().Count();
    }

    public int getFirstDistinct(int lengthOfMessage, string input) {
      int index = 0;
      while (index < input.Length) {
        List<char> markerString = new List<char>(lengthOfMessage);
        for (int i = 0; i < lengthOfMessage; i++) {
          markerString.Add(input[index + i]);
        }
        if (!checkDouble(markerString)) {
          break;
        } else {
          markerString.Clear();
        }
        index++;
      }
      return index+lengthOfMessage;
    }

    public void PartOne() {
      string lines = File.ReadAllText(@"../../../Inputs/input06.txt");
      int result = getFirstDistinct(4, lines);
      Console.WriteLine(result);
    }

    public void PartTwo() {
      string lines = File.ReadAllText(@"../../../Inputs/input06.txt");
      int result = getFirstDistinct(14, lines);
      Console.WriteLine(result);
    }
  }
}

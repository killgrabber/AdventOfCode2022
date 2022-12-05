using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day05 : IDays {

    public class CrateStack {
      public int number;
      public List<char> crates = new List<char>();

      public CrateStack(int n) {
        number = n;
      }

      public string toString() {
        string ret = $"number: {number}";
        foreach (char c in crates) {
          ret += $"[{c}] ";
        }
        return ret;
      }
    }

    public class Ship {
      public List<CrateStack> allCrateStack = new List<CrateStack>();

      public Ship() {

      }

      public Ship(string[] input, int end) {
        string crateNumbers = input[end];
        int cratePos = 0;
        foreach (char c in crateNumbers) {
          if (c != ' ') {
            int stackNumber = c - '0';
            allCrateStack.Add(new CrateStack(stackNumber));
            int height = end - 1;
            while (height >= 0 && input[height][cratePos] != ' ') {
              Console.WriteLine($"trying to add: {input[height][cratePos]} at h={height},n={cratePos}");
              allCrateStack[stackNumber - 1].crates.Add(input[height][cratePos]);
              height--;
            }
          }
          cratePos++;
        }
      }

      public void move(string cmd) {
        string[] cmds = cmd.Split(" ");
        if (cmd == "" || cmds[0] != "move") {
          return;
        }
        int moveAmount = int.Parse(cmds[1]);
        int moveFrom = int.Parse(cmds[3]) - 1;
        int moveTo = int.Parse(cmds[5]) - 1;
        Console.WriteLine($"move {moveAmount} from {moveFrom} to {moveTo}");
        List<char> cratesToMove = new List<char>();
        for (int i = 0; i < moveAmount; i++) {
          int stackLength = allCrateStack[moveFrom].crates.Count;
          char lastElement = allCrateStack[moveFrom].crates.ElementAt(stackLength - 1);
          allCrateStack[moveFrom].crates.RemoveAt(stackLength - 1);
          cratesToMove.Add(lastElement);
        }
        allCrateStack[moveTo].crates.AddRange(cratesToMove);
      }

      public void move2(string cmd) {
        string[] cmds = cmd.Split(" ");
        if (cmd == "" || cmds[0] != "move") {
          return;
        }
        int moveAmount = int.Parse(cmds[1]);
        int moveFrom = int.Parse(cmds[3]) - 1;
        int moveTo = int.Parse(cmds[5]) - 1;
        Console.WriteLine($"move {moveAmount} from {moveFrom} to {moveTo}");
        List<char> cratesToMove = new List<char>();
        int stackLength = allCrateStack[moveFrom].crates.Count;
        int pos2Remove = stackLength - moveAmount;
        for (int i = 0; i < moveAmount; i++) {
          char elmnt = allCrateStack[moveFrom].crates.ElementAt(pos2Remove);
          allCrateStack[moveFrom].crates.RemoveAt(pos2Remove);
          cratesToMove.Add(elmnt);
        }
        allCrateStack[moveTo].crates.AddRange(cratesToMove);
      }

      public string toString() {
        string ret = "";
        foreach (CrateStack c in allCrateStack) {
          ret += c.toString() + "\n";
        }
        return ret;
      }
    }

    public void PartOne() {
      Ship ship = new Ship();
      string[] lines = File.ReadAllLines(@"../../../Inputs/input05.txt");
      int index = 0;
      bool initalized = false;
      foreach (string line in lines) {
        Console.WriteLine(line);
        if (!initalized && line[1] == '1') {
          initalized = true;
          Console.WriteLine(index);
          ship = new Ship(lines, index);
        }
        index++;
        Console.Write(ship.toString());
        ship.move(line);
      }

      string result = "";
      foreach (CrateStack cs in ship.allCrateStack) {
        result += $"{cs.crates.ElementAt(cs.crates.Count - 1)}";
      }
      Console.WriteLine(result);
    }

    public void PartTwo() {
      Ship ship = new Ship();
      string[] lines = File.ReadAllLines(@"../../../Inputs/input05.txt");
      int index = 0;
      bool initalized = false;
      foreach (string line in lines) {
        Console.WriteLine(line);
        if (!initalized && line[1] == '1') {
          initalized = true;
          Console.WriteLine(index);
          ship = new Ship(lines, index);
        }
        index++;
        Console.Write(ship.toString());
        ship.move2(line);
      }

      string result = "";
      foreach (CrateStack cs in ship.allCrateStack) {
        result += $"{cs.crates.ElementAt(cs.crates.Count - 1)}";
      }
      Console.WriteLine(result);
    }
  }
}

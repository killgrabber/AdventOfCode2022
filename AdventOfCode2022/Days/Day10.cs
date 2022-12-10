using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day10 : IDays {

    public class Computer {
      int cycle = 1;
      int register = 1;
      int nextSignalStrength = 20;

      public List<string> commands = new List<string>();
      public List<int> signalStrenghts = new List<int>();

      public char[,] display = new char[7, 40];

      bool isCalculating = false;

      public void drawPixel() {
        int row = (int)Math.Floor((double)cycle/40);
        int col = cycle % 40;
        Console.WriteLine($"drawing px:{'x'} at {row},{col}");
        bool isSprite = col == register || col == register - 1 || col == register + 1;
        if (isSprite) {
          display[row, col] = '#';
        } else {
          display[row, col] = '.';
        }
      }

      public void displayToConsole() {
        for(int i = 0; i < 6; i++) {
          string line = "";
          for(int ii = 0; ii < 40; ii++) {
            line += display[i, ii];
          }
          Console.WriteLine(line);
        }
      }

      public void printSignalStrength() {
        if (cycle == nextSignalStrength && cycle <= 220) {
          Console.WriteLine($"signalStrength at cycle {cycle}: {register * cycle}");
          signalStrenghts.Add(register * cycle);
          nextSignalStrength += 40;
        }
      }

      public void doAllCommands() {
        while(commands.Count > 0) {
          doCommand();
        }
      }

      public void doCommand() {
        drawPixel();
        cycle++;
        printSignalStrength();
        if (isCalculating) {
          isCalculating = false;
          return;
        }
        string command = commands[0];
        commands.RemoveAt(0);
        if (command == "noop") {
          return;
        }
        string[] s = command.Split(' ');
        if (s[0] == "addx") {
          isCalculating = true;
          int add = 0;
          //check if negativ
          if (s[1][0] == '-') {
            string[] ss = s[1].Split('-');
            add = int.Parse(ss[1]) * -1;
          } else {
            add = int.Parse(s[1]);
          }
          register += add;
          Console.WriteLine($"to add {add}");
            }
      }

      public override string ToString() {
        return $"c: {cycle} x: {register}";
      }
    }



    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input10.txt");
      Computer computer = new Computer();
      computer.display[0, 0] = '#';
      foreach (string line in lines) {
        computer.commands.Add(line);
      }
      computer.doAllCommands();
      Console.WriteLine($"sum: {computer.signalStrenghts.Sum()}");
      computer.displayToConsole();
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input10.txt");
    }
  }
}

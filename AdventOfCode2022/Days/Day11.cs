using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day11 : IDays {

    public static List<Monkey> monkeys = new List<Monkey>();

    public int throwTo(int m, int item) {
      monkeys[m].addItem(item);
      return 0;
    }

    public class Monkey {
      List<int> items = new();
      int modifier = 0;
      bool usesMultiply = false;
      int divisibleBy = 0;
      int targetMonkey1 = 0;
      int targetMonkey2 = 0;

      public long inspectCount = 0;

      public void addItem(int item) {
        items.Add(item);
      }

      public int getNewWorryLevel(int index) {
        inspectCount++;
        int newLevel = items[index];
        //Console.WriteLine($"old worry level is: {newLevel}");

        int mod = (modifier == -1) ? newLevel : modifier;
        if (usesMultiply) {
          newLevel = newLevel * mod;
        } else {
          newLevel = newLevel + mod;
        }
        return (int)Math.Floor((double) newLevel/3);
      }

      public int getTargetMonkey(int newWorryLevel) {
        if (newWorryLevel % divisibleBy == 0) {
          return targetMonkey1;
        }
        return targetMonkey2;
      }

      public void round(Func<int, int, int> method) {
        while (items.Count > 0) {
          int newWorryLevel = getNewWorryLevel(0);
          int targetM = getTargetMonkey(newWorryLevel);
          //int nextWorry = getNextWorryLevel(newWorryLevel, targetM);
          Console.WriteLine($"item with level: {newWorryLevel} thrown to Monkey {targetM}");
          items.RemoveAt(0);
          method(targetM, newWorryLevel);
        }
      }

      public int getNextWorryLevel(int oldWorryLevel, int target) {
        int newLevel = oldWorryLevel;
        //idea: set level so that the next test does not change
        int nextDivisble = monkeys[target].divisibleBy;
        while (newLevel > (nextDivisble * 2)) {
          if (newLevel - nextDivisble > 0) {
            newLevel = newLevel - nextDivisble;
          }
        }
        Console.WriteLine($"Target: {target} oldLevel: {oldWorryLevel}%{nextDivisble}=={oldWorryLevel % nextDivisble} newLevel: {newLevel}%{nextDivisble}=={newLevel % nextDivisble}");
        return newLevel;
      }

      public override string ToString() {
        string ret = $"[{String.Join(", ", items.ToArray())}]";
        ret += $" mod: {modifier}";
        ret += $" div: {divisibleBy}";
        ret += $" t1: {targetMonkey1}";
        ret += $" t2: {targetMonkey2}";
        ret += $" inspectC: {inspectCount}";
        return ret;
      }

      public Monkey(string[] input) {
        if (input.Length != 6) {
          throw new Exception();
        }
        for (int i = 1; i < input.Length; i++) {
          string[] splittet = input[i].Split(' ');
          //
          if (splittet[2] == "Starting") {
            for (int j = 4; j < splittet.Length; j++) {
              int item = int.Parse(splittet[j].Remove(2));
              items.Add(item);
            }
          } else if (splittet[2] == "Operation:") {
            usesMultiply = splittet[6] == "*";
            if (splittet[7] == "old") {
              modifier = -1;
            } else {
              modifier = int.Parse(splittet[7]);
            }
          } else if (splittet[2] == "Test:") {
            divisibleBy = int.Parse(splittet[5]);
            string[] nextLineSplit = input[i + 1].Split(' ');
            targetMonkey1 = int.Parse(nextLineSplit[9]);
            nextLineSplit = input[i + 2].Split(' ');
            targetMonkey2 = int.Parse(nextLineSplit[9]);
          }
        }
      }


    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input11.txt");
      for (int i = 0; i < lines.Length; i += 7) {
        string[] monkeyString = new string[6];
        for (int j = 0; j < monkeyString.Length; j++) {
          //Console.WriteLine(lines[i+j]);
          monkeyString[j] = lines[i + j];
        }
        Monkey m = new(monkeyString);
        monkeys.Add(m);
      }

      foreach (Monkey m in monkeys) {
        Console.WriteLine(m);
      }

      int amountRounds = 20;
      for (int i = 0; i < amountRounds; i++) {
        for (int j = 0; j < monkeys.Count; j++) {
          monkeys[j].round(throwTo);
        }
        for (int log = 0; log < monkeys.Count; log++) {
          if(i == 20 || i == 1000 || i == 2000 || true) {
            Console.WriteLine($"{monkeys[log]}");
          }
        }
        Console.WriteLine("");
      }

      foreach (Monkey m in monkeys) {
        Console.WriteLine(m);
      }
      List<Monkey> SortedList = monkeys.OrderBy(o => o.inspectCount).ToList();
      long max1 = SortedList[SortedList.Count - 1].inspectCount;
      long max2 = SortedList[SortedList.Count - 2].inspectCount;
      long result = max1 * max2;
      Console.WriteLine($"result: {result}");

    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input11.txt");
    }
  }
}

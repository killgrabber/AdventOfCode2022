using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day11 : IDays {

    public static List<Monkey> monkeys = new List<Monkey>();

    public int throwTo(int from, int to, long item) {
      //Console.WriteLine($"item with level: {item} thrown from {from} to {to}");
      monkeys[from].items.RemoveAt(0);
      monkeys[to].addItem(item);
      return 0;
    }

    public class Monkey {
      public List<long> items = new();
      public int modifier = 0;
      public bool usesMultiply = false;
      public int divisibleBy = 0;
      public int targetMonkey1 = 0;
      public int targetMonkey2 = 0;

      public long inspectCount = 0;

      public long number = 1;

      public void addItem(long item) {
        items.Add(item);
      }

      public long getNewWorryLevel(int index, int m) {
        inspectCount++;
        long newLevel = items[index];
        long oldLevel = newLevel;
        long mod = (modifier == -1) ? newLevel : modifier;
        if (usesMultiply) {
          newLevel = newLevel * mod;
        } else {
          newLevel = newLevel + mod;
        }
        newLevel = newLevel % number;
        //Console.WriteLine($"m: {m} worry level was: {oldLevel}, worry level is: {newLevel} targetM: {getTargetMonkey(newLevel)}");
        return newLevel;
      }

      public int getTargetMonkey(long newWorryLevel) {
        if (newWorryLevel % divisibleBy == 0) {
          return targetMonkey1;
        }
        return targetMonkey2;
      }

      public void round(Func<int, int, long, int> method, int monkey) {
        while (items.Count > 0) {
          long newWorryLevel = getNewWorryLevel(0, monkey);
          int targetM = getTargetMonkey(newWorryLevel);
          //int minimizedWorryLevel = minimizeOut(newWorryLevel, targetM);
          method(monkey, targetM, newWorryLevel);
        }
      }
      public int minimizeOut(int level, int targetMonkey) {
        int levelOfTarget = level;
        //Console.WriteLine($"old worry level is: {newLevel}");
        int modifi = monkeys[targetMonkey].modifier;
        int div = monkeys[targetMonkey].divisibleBy;
        int mod = (modifi == -1) ? levelOfTarget : modifi;

        if (monkeys[targetMonkey].usesMultiply) {
          levelOfTarget = levelOfTarget * mod;
        } else {
          levelOfTarget = levelOfTarget + mod;
        }

        int neededResult = levelOfTarget % div;
        int newResult = 0;
        if (monkeys[targetMonkey].usesMultiply) {
          newResult = neededResult / (mod % 1);
        } else {
          newResult = neededResult + div - mod;
        }
        //Console.WriteLine($"Monkey {targetMonkey} will calculate: {levelOfTarget}: isDivisble {neededResult}. Should send: {newResult}");
        return newResult;
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

      long numberPrime = 1;
      foreach (Monkey m in monkeys) {
        numberPrime = numberPrime * m.divisibleBy;
        Console.WriteLine(m);
      }

      foreach (Monkey m in monkeys) {
        m.number = numberPrime;
      }

      Console.WriteLine($"prime: {numberPrime}");

      int amountRounds = 10000;
      for (int i = 0; i < amountRounds; i++) {
        if (i == 20 || i == 1000 || i == 2000 || i == 3000 || i == 4000 || i == 6000 || i == 7000 || i == 8000 || i == 9000 || i == 10000) {
          for (int log = 0; log < monkeys.Count; log++) {
            Console.WriteLine($"{monkeys[log]}");
          }
          Console.WriteLine($"----------Finished Round {i}");
        }

        for (int j = 0; j < monkeys.Count; j++) {
          monkeys[j].round(throwTo, j);
        }
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day07 : IDays {

    public class AoCFile {
      public string name;
      public int size;

      public AoCFile(string input) {
        string[] strings = input.Split(" ");
        name = strings[1];
        size = int.Parse(strings[0]);
      }
    }

    public class AocFileSystem {
      public List<Directory> directories = new List<Directory>();
      int currentPos = 0;
      int nextFree = 0;

      public Directory currentDirectory = new Directory("/");

      public AocFileSystem(string[] input) {
        foreach (string line in input) {
          Console.WriteLine($"next command: {line}"); ;
          handleNewLine(line);
          //Console.Write(visualize());
        }
      }

      public void handleNewLine(string line) {
        string[] strings = line.Split(" ");
        if (strings[0] == "$") {
          handleCommand(line);
        } else if (strings[0] == "dir") {
          createDir(strings[1]);
        } else {
          //must be a file
          currentDirectory.createFile(line);

        }
      }

      public void log() {
        string consoleOut = $"ls: cur: {currentDirectory.name} subs: ";
        foreach (Directory curD in currentDirectory.subDirectories) {
          consoleOut += $"[{curD.name}],";
        }
        Console.WriteLine(consoleOut);
      }

      public void handleCommand(string command1) {
        string[] cmds = command1.Split(" ");
        if (cmds[1] == "ls") {
          log();
        } else if (cmds[1] == "cd" && cmds[2] == "..") {
          Console.WriteLine("going to parent");
          currentDirectory = currentDirectory.parent;
          log();
        } else if (cmds[1] == "cd") {
          Console.WriteLine("switching dir");
          string nextDirName = cmds[2];
          Console.WriteLine($"should switch to dir: {nextDirName}");
          if (nextDirName == "/") {
            Console.WriteLine($"start in correct dir: {nextDirName}");
          } else {
            bool found = false;
            foreach (Directory d in currentDirectory.subDirectories) {
              if (d.name == nextDirName) {
                found = true;
                currentDirectory = d; break;
              }
            }
            if (!found) {
              throw new Exception();
            }
          }
        }
        //Console.WriteLine($"currentPos: {currentPos}, dir on currPos '{directories[currentPos].name}'");
      }

      public string toString() {
        string ret = string.Empty;
        ret += $"{currentDirectory.toString()}\n";
        return ret;
      }

      public int countTry2() {
        int sum = 0;
        goToTop();
        sum = currentDirectory.getSubSums();

        Console.WriteLine(currentDirectory.name);



        return countPartOne();

      }

      public int countPartOne() {
        currentDirectory.getSubSums();
        return currentDirectory.countPartOne();
      }

      public int countPartTwo(int min) {
        goToTop();
        currentDirectory.getSubSums();
        return currentDirectory.countPartTwo(min);
      }

      public void initializeFileSize() {
        foreach (Directory d in directories) {
          d.totalFileSize = d.getSum();
        }
      }

      public void initalizeSubDirectoryFileSize() {
        for (int i = directories.Count - 1; i > 0; i--) {
          int motherIndex = directories[i].indexOfMother;
          directories[motherIndex].totalFileSize += directories[i].totalFileSize;
        }
      }

      public void createDir(string dir) {
        Directory newD = new Directory(dir);
        newD.parent = currentDirectory;
        currentDirectory.subDirectories.Add(newD);
        Console.WriteLine($"add dir: {dir} with mother: {currentDirectory.name}");

        //directories.Add(new Directory(dir, currentPos, directories.Count));
        //Console.WriteLine($"add dir: {dir} with mother: {directories[currentPos].name}");
      }
      public string visualize() {
        string ret = "";
        for (int i = 0; i < directories.Count; i++) {
          ret += "\n";
          ret += $"name: {directories[i].name} ";
          ret += $"motherDir: {directories[directories[i].indexOfMother].name} ";
          ret += $"filesize: {directories[i].totalFileSize}";
        }
        return ret;
      }

      public int count() {
        int sum = 0;
        foreach (Directory d in directories) {
          int dirSum = d.totalFileSize;
          sum += dirSum;
        }
        return sum;
      }

      public void goToTop() {
        while (currentDirectory.parent != null) {
          currentDirectory = currentDirectory.parent;
        }
      }
    }


    public class Directory {
      public string name;

      public Directory? parent = null;
      public List<AoCFile> files = new List<AoCFile>();
      public List<Directory> subDirectories = new List<Directory>();

      public int indexOfMother = 0;
      public int myPosition;

      public int totalFileSize = 0;
      public int subDirSums = 0;

      public Directory(string dirName, int mother, int pos) {
        name = dirName;
        indexOfMother = mother;
        myPosition = pos;
      }

      public Directory(string dirName) {
        name = dirName;
        indexOfMother = 0;
      }


      public string toString() {
        string ret = "";
        foreach (Directory d in subDirectories) {
          ret += d.toString();
        }
        ret += $"{files.Count}";
        return ret;
      }

      public void createFile(string file) {
        Console.WriteLine($"add file: {file}");
        files.Add(new AoCFile(file));
      }

      public int getSum() {
        int sum = 0;
        foreach (AoCFile file in files) {
          sum += file.size;
        }

        foreach (Directory d in subDirectories) {
          sum += d.getSum();
        }
        return sum;
      }

      public int getSubSums() {
        int sum = getSum();
        foreach (Directory d in subDirectories) {
          totalFileSize += sum + d.getSubSums();
        }
        return totalFileSize;
      }

      public int countPartOne() {
        int sum = getSum();
        //disable 'false' for part one
        if (sum > 100000 && false) {
          sum = 0;
        }
        Console.WriteLine($"{name}: #size {sum}");
        foreach (Directory d in subDirectories) {
          sum += d.countPartOne();
        }
        return sum;
      }

      public int countPartTwo(int min) {
        int sum = getSum();
        //disable 'false' for part one
        if (sum > min) {
          Console.WriteLine($"{name}: #size {sum}");
        }
        foreach (Directory d in subDirectories) {
          int subsum = d.countPartTwo(min);
          if(subsum > min && subsum < sum) {
            sum = subsum;
            Console.WriteLine($"new min: {sum}");
          }
        }
        return sum;
      }
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input07.txt");
      AocFileSystem aocFileSystem = new AocFileSystem(lines);
      aocFileSystem.goToTop();

      Console.WriteLine(aocFileSystem.currentDirectory.name);
      Console.WriteLine($"count: {aocFileSystem.countPartOne()}");
      Console.WriteLine($"filesize: {aocFileSystem.currentDirectory.totalFileSize}");
      Console.WriteLine($"size {aocFileSystem.currentDirectory.getSum()}");


      //aocFileSystem.directories.ForEach(x =>  Console.WriteLine($"{x.name}, {x.totalFileSize}"));
      //Console.WriteLine(aocFileSystem.directories.Where(x => x.totalFileSize <= 100000).Sum(x => x.totalFileSize));
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input07.txt");
      AocFileSystem aocFileSystem = new AocFileSystem(lines);
      int fileSystemsizeMax = 70000000;
      int fileSystemsizeUsed = 43313415;
      int placeThatNeedsToBeFreed = 30000000 - (fileSystemsizeMax - fileSystemsizeUsed);
      int used = aocFileSystem.countPartTwo(placeThatNeedsToBeFreed);
      Console.WriteLine(placeThatNeedsToBeFreed);
    }
  }
}

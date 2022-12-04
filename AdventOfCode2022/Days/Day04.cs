using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Days {
  public class Day04 : IDays {

    public class Assigment {
      public int start;
      public int end;

      public Assigment(int start, int end) {
        this.start = start;
        this.end = end;
      }

      public bool contains(Assigment other) {
        return this.start <= other.start && this.end >= other.end;
      }
      public string toString() {
        return $"{start}-{end}";
      }

      public List<int> getList() {
        List<int> list = new List<int>();
        int index = start;
        while(index <= end) {
          list.Add(index++);
        }
        return list;
      }
    }

    public class Assigments {
      Assigment elfOne;
      Assigment elfTwo;

      public Assigments(string inputLine) {
        string[] parts = inputLine.Split(',', '-');
        this.elfOne = new Assigment(int.Parse(parts[0]), int.Parse(parts[1]));
        this.elfTwo = new Assigment(int.Parse(parts[2]), int.Parse(parts[3]));
      }

      public bool containsEachOther() {
        return this.elfOne.contains(this.elfTwo) || this.elfTwo.contains(this.elfOne);
      }

      public bool overlaps() {
        foreach(int a in this.elfOne.getList()) {
          foreach(int b in this.elfTwo.getList()) {
            if (a == b) return true;
          }
        }
        return false;
      }

      public string toString() {
        return $"one: {elfOne.toString()}, two: {elfTwo.toString()}";
      }
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input04.txt");
      int sum = 0;
      foreach(string line in lines) {
        Assigments a = new Assigments(line);
        Console.WriteLine(a.toString());
        if(a.containsEachOther()) {
          sum++;
        }
      }
      Console.WriteLine(sum);
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input04.txt");
      int sum = 0;
      foreach (string line in lines) {
        Assigments a = new Assigments(line);
        Console.WriteLine(a.toString());
        if (a.overlaps()) {
          sum++;
        }
      }
      Console.WriteLine(sum);
    }
  }
}

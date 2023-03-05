using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2022.Days {
  public class Day13 : IDays {

    List<PairOfPackets> listOfPacketPairs = new();

    public class PairOfPackets {
      public Packet left;
      public Packet right;

      public PairOfPackets(string l, string r) {
        left = new Packet(l);
        right = new Packet(r);
      }

      public bool compare() {
        return left.compare(right);
      }

      public override string ToString() {
        return $"left: {left}\nright: {right}\n";
      }

    }

    public class Packet {
      public List<Packet> packets = new List<Packet>();
      public List<int> values = new();

      public bool isInt() {
        return packets.Count == 0;
      }

      public void addInt(int n) {
        values.Add(n);
      }
      public void addPackage() {
        packets.Add(new Packet());
      }

      public Packet() {

      }

      public void addOld(string line) {
        Console.WriteLine($"next command {line}");
        if (line.Length == 0 || line[0] == 'a') {
          return;
        }
        if (line[0] == '[') {
          string nextCommand = "";
          int index = 1;
          while (index < line.Length && line[index] != ']') {
            nextCommand += line[index++];
          }
          packets.Add(new Packet(nextCommand));
          line = line.Remove(0, index);
        } else if (line[0] == ']') {
          return;
        } else if (line[0] == ',') {
          line = line.Remove(0, 1);
        } else {
          values.Add(line[0] - '0');
          line = line.Remove(0, 1);
        }
        addOld(line);
      }

      public void add(string line) {
        Console.WriteLine($"next command {line}");
        int charIndex = 0;
        int packetIndex = -1;
        foreach (char c in line) {
          //Console.WriteLine($"char: {c} index: {packetIndex}");
          switch (c) {
            case '[':
              packetIndex++;
              if (packetIndex > 0) {
                //Console.WriteLine("added new packet");
                packets.Add(new Packet());
              }
              break;
            case ']':
              if (line.Length > charIndex+2 && line[charIndex+2] != '[') {
                packetIndex--;
              }
              break;
            case ',':
              //do nothing
              break;
            default:
              //Console.WriteLine($"added: {c} at: {packetIndex-1}");
              if(packetIndex == 0) {
                values.Add(c - '0');
              } else {
                packets[packetIndex-1].values.Add(c - '0');
              }
              break;
          }
          charIndex++;
        }
        Console.WriteLine($"packet: {this}");
      }

      public Packet(string line) {
        add(line);
        //Console.WriteLine($"added: {this}");
      }

      public override string ToString() {
        string ret = "[";
        foreach (Packet p in packets) {
          if (p.packets.Count != 0 || true) {
            ret += $"{p}";
          }
        }
        int count = 0;
        foreach (int n in values) {
          if (count++ != 0) {
            ret += ",";
          }
          ret += $"{n}";
        }
        return ret + "]";
      }

      bool checkInts(Packet other) {
        Console.WriteLine($"Comparing ints {this} with {other}");
        int valueIndex = 0;
        while (valueIndex < values.Count) {
          if (valueIndex < other.values.Count) {
            Console.WriteLine($"Comparing {values[valueIndex]} with {other.values[valueIndex]}");
            if (values[valueIndex] == other.values[valueIndex]) {
              // check next
              valueIndex++;
            } else {
              return values[valueIndex] < other.values[valueIndex];
            }
          } else {
            Console.WriteLine("Right has run out of items");
            return false;
          }
        }
        return false;
      }

      public bool compare(Packet other) {
        Console.WriteLine($"Comparing {this} with {other}");
        if (isInt() && other.isInt()) {
          return checkInts(other);
        }

        if (isInt() && !other.isInt()) {
          Console.WriteLine("right is list");
          foreach (Packet otherP in other.packets) {
            compare(otherP);
          }
        }

        if (!isInt() && other.isInt()) {
          Console.WriteLine("Left is list");
          foreach (Packet p in packets) {
            p.compare(other);
          }
        }

        if (!isInt() && !other.isInt()) {
          Console.WriteLine("Both are lists");
          while (packets.Count > 0 && other.packets.Count > 0) {
            Packet nextLeft = packets[packets.Count - 1];
            Packet nextRight = other.packets[other.packets.Count - 1];
            packets.RemoveAt(packets.Count - 1);
            other.packets.RemoveAt(other.packets.Count - 1);
            nextLeft.compare(nextRight);
          }
        }
        return false;
      }
    }


    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input13.txt");
      for (int i = 0; i < lines.Length - 1; i += 3) {
        listOfPacketPairs.Add(new PairOfPackets(lines[i], lines[i + 1]));
      }
      int sum = 0;
      foreach (PairOfPackets pp in listOfPacketPairs) {
        Console.WriteLine(pp);
        if (pp.compare()) {
          sum++;
        }
      }
      Console.WriteLine($"sum: {sum}");
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input13.txt");
    }
  }
}

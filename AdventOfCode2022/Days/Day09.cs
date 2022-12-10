using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day09 : IDays {

    public class Position {
      public bool isTail = false;
      public bool visited = false;
      public char state = '.';
      public int x;
      public int y;

      public int ropeCount = 0;

      public Position(char s, int x, int y, bool isTail = false) {
        state = s;
        this.x = x;
        this.y = y;
        this.isTail = isTail;
      }

      public override string ToString() {
        string ch = state.ToString();
        if (visited) {
          ch = "#";
        }

        if (ropeCount != 0) {
          ch = $"{ropeCount}";
        }
        return ch;
      }
    }

    public class Rope {
      public List<List<Position>> positions = new List<List<Position>>();
      public List<Position> tailPoses = new List<Position>();

      static int capacity = 30;
      static int startX = capacity / 2;
      static int startY = capacity / 2;
      Position currentHeadPose = new Position('H', startX, startY, true);
      Position currentTailPose = new Position('T', startX, startY);


      public Rope() {
        Console.WriteLine("init rope");
        for (int i = 0; i < capacity; i++) {
          List<Position> list = new List<Position>();
          for (int ii = 0; ii < capacity; ii++) {
            Position blank = new Position('.', i, ii);
            list.Add(blank);
          }
          positions.Add(list);
        }
        add(ref currentHeadPose);
        currentHeadPose.visited = true;
        currentHeadPose.ropeCount++;
        Console.WriteLine("init rope done");
        tailPoses.Add(currentHeadPose);
      }

      public void add(ref Position position) {
        position.visited = positions[position.x][position.y].visited;
        positions[position.x][position.y] = position;
        return;

        if (position.x >= positions.Count) {
          List<Position> newRow = new List<Position>();
          newRow.Add(position);
          positions.Add(newRow);
        } else {
          positions[position.x].Add(position);
        }
      }

      public void move(string inputLine) {
        Console.WriteLine($"-- {inputLine} -- ");
        string[] strings = inputLine.Split(" ");
        string command = strings[0];
        int moveAmount = int.Parse(strings[1]);

        int newX = 0;

        int newY = 0;

        if (command == "U") {
          //Console.WriteLine("Move up");
          newX++;
        } else if (command == "D") {
          newX--;
          //Console.WriteLine("Move down");
        } else if (command == "R") {
          newY++;
          //Console.WriteLine("Move right");
        } else if (command == "L") {
          newY--;
          //Console.WriteLine("Move left");
        } else {
          throw new Exception("Unsupported move");
        }
        //part one
        for (int i = 0; i < moveAmount; i++) {
          moveHeadBy(newX, newY);
        }
      }

      public void moveHeadBy(int x, int y) {
        int nextHeadx = x + currentHeadPose.x;
        int nextHeady = y + currentHeadPose.y;

        Console.WriteLine($"move head to {nextHeadx},{nextHeady}");

        currentHeadPose.state = '.';
        currentHeadPose = new Position('H', nextHeadx, nextHeady);
        add(ref currentHeadPose);
        moveTail(x, y);
      }

      public void moveTail(int x, int y) {
        int xHead = x + currentHeadPose.x;
        int yHead = y + currentHeadPose.y;

        int xTail = currentTailPose.x;
        int yTail = currentTailPose.y;

        double[] d = getVecHeadTail();
        double diff = getTailHeadDiff();
        Console.WriteLine($"head: {xHead},{yHead}, tail: {xTail},{yTail} vec: {d[0]},{d[1]} diff {diff}");


        if (diff == 2) {
          Console.WriteLine($"Moving tail by {x},{y}");
          currentTailPose.state = '.';
          currentTailPose = positions[currentHeadPose.x - x][currentHeadPose.y - y];
          currentTailPose.state = 'T';
          tailPoses.Add(currentTailPose);
          //currentTailPose.visited = true;
          for (int i = 0; i < tailPoses.Count; i++) {
            Position p = tailPoses[i];
            p.ropeCount++;
          }
        } else if (diff > 2) {
          //move diag
          int moveX = 1;
          int moveY = 1;
          if (d[0] < 0 && d[1] < 0) {
            //down left
            moveX = -1;
            moveY = -1;
          } else if (d[0] < 0 && d[1] > 0) {
            //down right
            moveX = -1;
            moveY = 1;
          } else if (d[0] > 0 && d[1] < 0) {
            // top left
            moveX = 1;
            moveY = -1;
          } else if (d[0] > 0 && d[1] > 0) {
            //top right
          }
          Console.WriteLine($"Moving tail diag by {moveX},{moveY}");
          int lastTailX = currentTailPose.x;
          int lastTailY = currentTailPose.y;
          Console.WriteLine($"last tail pos {lastTailX},{lastTailY}");
          currentTailPose = positions[lastTailX + moveX][lastTailY + moveY];
          currentTailPose.state = 'T';
          tailPoses.Add(currentTailPose);
          for (int i = 0; i < tailPoses.Count; i++) {
            Position p = tailPoses[i];
            p.ropeCount++;
          }

        }
        logPositions();
      }

      public double getTailHeadDiff() {
        double[] d = getVecHeadTail();
        d[0] = Math.Abs(d[0]);
        d[1] = Math.Abs(d[1]);
        return Math.Sqrt((d[0] * d[0]) + (d[1] * d[1]));
      }

      public double[] getVecHeadTail() {
        double[] ret = new double[2];
        ret[0] = currentHeadPose.x - currentTailPose.x;
        ret[1] = currentHeadPose.y - currentTailPose.y;
        return ret;
      }

      public int count() {
        int sum = 0;
        for (int i = positions.Count - 1; i >= 0; i--) {
          for (int ii = 0; ii < positions[i].Count; ii++) {
            if (positions[i][ii].visited) {
              sum++;
            }
          }
        }
        return sum;
      }


      public void logPositions() {
        string ret = "\n";
        for (int i = positions.Count - 1; i >= 0; i--) {
          for (int ii = 0; ii < positions[i].Count; ii++) {
            ret += positions[i][ii];
          }
          ret += "\n";
        }
        Console.WriteLine(ret);
      }
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input09.txt");
      Rope rope = new Rope();
      foreach (string line in lines) {
        Console.WriteLine(line);
        rope.move(line);
      }

      Console.WriteLine($"Count: {rope.count()}");
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input09.txt");
    }
  }
}

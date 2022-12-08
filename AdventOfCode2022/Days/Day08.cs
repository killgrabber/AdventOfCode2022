using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day08 : IDays {

    List<int[]> trees = new List<int[]>();

    List<int[]> treeViewDist = new List<int[]>();



    void setTrees(string[] input) {
      foreach (string line in input) {
        int index = 0;
        int[] currentLine = new int[line.Length];
        foreach (char c in line) {
          currentLine[index++] = c - '0';
        }
        trees.Add(currentLine);
      }
    }

    void initalizeViewDistances() {
      for(int i = 0; i < trees.Count; i++) {
        int[] currentLine = new int[trees[i].Length];
        for (int ii = 0; ii < trees[i].Length; ii++) {
          currentLine[ii] = 1;
        }
        Console.WriteLine($"Adding line to view");
        treeViewDist.Add(currentLine);
      }
    }

    void logTrees() {
      string ret = "";
      foreach (int[] treeLine in trees) {
        foreach (int t in treeLine) {
          ret += t;
        }
        ret += "\n";
      }
      Console.WriteLine(ret);
    }

    void logViewDistance() {
      string ret = "";
      foreach (int[] treeLine in treeViewDist) {
        foreach (int t in treeLine) {
          ret += $"{t} ";
        }
        ret += "\n";
      }
      Console.WriteLine(ret);
    }

    int countVisibleTrees() {
      //first all outside trees
      int visibleTrees = (trees.Count - 1 + trees[0].Length - 1) * 2;
      //Console.WriteLine($"Visible outside trees: {visibleTrees}");
      //then the other
      for (int row = 1; row < trees.Count - 1; row++) {
        for (int col = 1; col < trees[row].Length - 1; col++) {
          int treeHeight = trees[row][col];
          if (isVisible(treeHeight, row, col)) {
            visibleTrees++;
          }
        }
      }
      return visibleTrees;
    }

    int countViews() {
      for (int i = 0; i < trees.Count; i++) {
        for (int ii = 0; ii < trees[i].Length; ii++) {
          int treeHeight = trees[i][ii];
          countDistances(treeHeight, i, ii);
        }
      }

      int max = 0;
      for (int i = 0; i < treeViewDist.Count; i++) {
        for (int ii = 0; ii < treeViewDist[i].Length; ii++) {
          max = Math.Max(max, treeViewDist[i][ii]);
        }
      }
      return max;
    }


    bool isVisible(int treeHeight, int row, int col) {
      bool isVisible = checkVisibleRight(treeHeight, row, col);
      isVisible = isVisible || checkVisibleLeft(treeHeight, row, col);
      isVisible = isVisible || checkVisibleUp(treeHeight, row, col);
      isVisible = isVisible || checkVisibleDown(treeHeight, row, col);

      return isVisible;
    }

    void countDistances(int treeHeight, int row, int col) {
      checkVisibleRight(treeHeight, row, col);
      checkVisibleLeft(treeHeight, row, col);
      checkVisibleUp(treeHeight, row, col);
      checkVisibleDown(treeHeight, row, col);
    }



    bool checkVisibleUp(int treeHeight, int row, int col) {
      //check up
      bool visible = true;
      int viewDistance = 0;
      for (int i = row - 1; i >= 0; i--) {
        viewDistance++;
        if (trees[i][col] >= treeHeight) {
          visible = false; break;
        }
        Console.WriteLine($"Checking {treeHeight} with {trees[i][col]} viewDistance: {viewDistance}");
      }
      //Console.WriteLine($"--Checked to up--");
      setViewDistance(viewDistance, row, col);
      return visible;
    }
    bool checkVisibleDown(int treeHeight, int row, int col) {
      //check up
      bool visible = true;
      int viewDistance = 0;
      for (int i = row + 1; i < trees.Count; i++) {
        viewDistance++;
        if (trees[i][col] >= treeHeight) {
          visible = false; break;
        }
        Console.WriteLine($"Checking {treeHeight} with {trees[i][col]} viewDistance: {viewDistance}");
      }
      //Console.WriteLine($"--Checked to down--");
      setViewDistance(viewDistance, row, col);
      return visible;
    }

    bool checkVisibleRight(int treeHeight, int row, int col) {
      int[] line = trees[row];
      //check right
      bool visible = true;
      int viewDistance = 0;
      for (int i = col + 1; i < line.Length; i++) {
        viewDistance++;
        if (line[i] >= treeHeight) {
          visible = false; break;
        }
        Console.WriteLine($"Checking {treeHeight} with {trees[i][col]} viewDistance: {viewDistance}");
      }
      //Console.WriteLine($"--Checked to right--");
      setViewDistance(viewDistance, row, col);
      return visible;
    }

    bool checkVisibleLeft(int treeHeight, int row, int col) {
      int[] line = trees[row];
      int viewDistance = 0;
      //check left
      bool visible = true;
      for (int i = col - 1; i >= 0; i--) {
        viewDistance++;
        if (line[i] >= treeHeight) {
          visible = false; break;
        }
        Console.WriteLine($"Checking {treeHeight} with {trees[i][col]} viewDistance: {viewDistance}");
      }
      //Console.WriteLine($"--Checked to left--");
      setViewDistance(viewDistance, row, col);
      return visible;
    }

    public void setViewDistance(int viewDistance, int x, int y) {
      Console.WriteLine($"was: {treeViewDist[x][y]} adding {viewDistance} at {x},{y}");
      treeViewDist[x][y] = treeViewDist[x][y] * viewDistance;
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input08.txt");
      setTrees(lines);
      initalizeViewDistances();
      logTrees();
      Console.WriteLine($"Visible Treeeees: {countVisibleTrees()}");
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input08.txt");
      trees.Clear();
      treeViewDist.Clear();
      setTrees(lines);
      initalizeViewDistances();
      int part2 = countViews();
      logViewDistance();
      Console.WriteLine($"Visible Treeeees: {part2}");
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days {
  public class Day02 : IDays {

    enum RIS { ROCK, PAPER, SCISSORS };
    enum TOWIN { WIN, LOSE, DRAW };

    RIS getRIS(char input) {
      if (input == 'A' || input == 'X') { return RIS.ROCK; }
      if (input == 'B' || input == 'Y') { return RIS.PAPER; }
      if (input == 'C' || input == 'Z') { return RIS.SCISSORS; }
      throw new Exception();
    }

    TOWIN getToWin(char input) {
      if (input == 'X') { return TOWIN.LOSE; }
      if (input == 'Y') { return TOWIN.DRAW; }
      if (input == 'Z') { return TOWIN.WIN; }
      throw new Exception();
    }

    RIS getCorrectCounterPick(RIS other, TOWIN result) {
      RIS counterPick = RIS.ROCK;
      switch (result) {
        case TOWIN.DRAW:
          return other;
        case TOWIN.WIN:
          counterPick = (RIS)(((int)other + 1) % 3); break;
        case TOWIN.LOSE:
          counterPick = (RIS)(((int)other + 2) % 3); break;
      }
      return (RIS)counterPick;
    }

    bool isDraw(RIS your, RIS other) {
      return your == other;
    }

    int getScore(RIS your, RIS other) {
      int score = 0;
      if (isDraw(your, other)) {
        score = 3;
      } else {
        bool won = false;
        switch (your) {
          case RIS.ROCK:
            won = other == RIS.SCISSORS; break;
          case RIS.PAPER:
            won = other == RIS.ROCK; break;
          case RIS.SCISSORS:
            won = other == RIS.PAPER; break;
        }
        score = won ? 6 : 0;
      }
      //Console.WriteLine($" other: {other}, your: {your} score: {score + (int)your} ");
      return score + (int)your + 1;
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input02.txt");
      int score = 0;
      foreach (string line in lines) {
        char your = line[2];
        char other = line[0];
        score += getScore(getRIS(your), getRIS(other));
      }
      Console.WriteLine(score);
    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input02.txt");
      int score = 0;
      foreach (string line in lines) {
        char your = line[2];
        char other = line[0];
        RIS yourPick = getCorrectCounterPick(getRIS(other), getToWin(your));
        score += getScore(yourPick, getRIS(other));
        Console.WriteLine($"Yourpick: {yourPick},other: {getRIS(other)} score: {score}");
      }
      Console.WriteLine(score);
    }
  }
}

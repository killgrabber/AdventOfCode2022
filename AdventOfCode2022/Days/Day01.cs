using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2022.Days {
  public class Day01 : IDays {

    public class GPSData {
      public string time = "";
      public double altiude;
      public double speed;
      public bool isLifting = false;

      public GPSData(string input) {
        string[] splittedInput = input.Split(',');
        time = splittedInput[1];
        string[] altiudeSplit = splittedInput[5].Split('.');
        altiude = int.Parse(altiudeSplit[0]);
        altiude += int.Parse(altiudeSplit[1])/1000;
        altiudeSplit = splittedInput[7].Split('.');
        speed = double.Parse(altiudeSplit[0]);
        speed += double.Parse(altiudeSplit[1]) / 1000;
      }

      public bool lifting(GPSData next, GPSData nextnext) {
        isLifting = altiude <= next.altiude && next.altiude <= nextnext.altiude;
        return isLifting;
      }

      public override string ToString() {
        return $"{time}: {altiude}m {speed}ms";
      }
    }

    List<GPSData> gpsDataList = new();

    public double getAverageDrivingSpeed() {
      setLifting();
      double sum = 0;
      int count = 0;
      foreach(GPSData gpsData in gpsDataList) {
        if(!gpsData.isLifting) {
          if(gpsData.speed > 0) {
            sum += gpsData.speed;
            count++;
          }
        }
      }
      return sum / count;
    }

    public void setLifting() {
      for(int i = 0; i < gpsDataList.Count-2; i++) {
        gpsDataList[i].lifting(gpsDataList[i + 1], gpsDataList[i + 2]);
      }
    }

    public void PartOne() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/20230106-102051.txt");
      int c = 0;
      foreach (string line in lines) {
        if(c++ == 0) {
          continue;
        }
        gpsDataList.Add(new GPSData(line));
      }

      double averageSpeed = getAverageDrivingSpeed();
      Console.WriteLine($"Average speed: {averageSpeed*3.6}");

    }

    public void PartTwo() {
      string[] lines = File.ReadAllLines(@"../../../Inputs/input01.txt");
    }
  }
}

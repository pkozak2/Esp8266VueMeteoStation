using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Helpers
{
    public static class AirQualityHelper
    {
        public static string[] PollutionLevels = { "Very low", "Low", "Medium", "High", "Very high"};

        public static int[] PM10Thresholds1h = { 0, 25, 50, 90, 180 };
        public static int[] PM10Thresholds24h = { 0, 15, 30, 50, 100 };

        
        public static int PM10Threshold1y = 40;
        public static int PM10DailyThreshold1y = 50;
        public static int PM10DaysAboveThreshold = 31;

        public static int[] PM25Thresholds1h = { 0, 15, 30, 55, 110 };
        public static int[] PM25Thresholds24h = { 0, 10, 20, 30, 60 };

        public static int PM25DailyThreshold1y = 30;

        public static int PM10Limit1h = 50;
        public static int PM10Limit24h = 50;

        public static int PM25Limit1h = 25;
        public static int PM25Limit24h = 25;

        public static AirQualityIndex FindLevel(int[] thresholds, double? value)
        {
            if(value == null)
            {
                return new AirQualityIndex();
            }
            for(int i = 0;i <thresholds.Length; i++)
            {
                if(thresholds[i] > value)
                {
                    return new AirQualityIndex
                    {
                        Index = i - 1,
                        Name = PollutionLevels[i - 1]
                    };
                }
            }
            return new AirQualityIndex() { Index = thresholds.Length - 1, Name = PollutionLevels[thresholds.Length - 1] };
        } 
    }

    public class AirQualityIndex
    {
        public string Name { get; set; }
        public int Index { get; set; }
    }
}

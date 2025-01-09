using System.Collections.Generic;
using System.IO;
using Objects;

namespace DataAccessAndPreparation
{
    public static class ReadFromCSV
    {
        private const string FilePath = "Assets/SensorET/sensor_et.csv";

        /// <summary>
        /// Reads sensor data stored in a csv file
        /// </summary>
        /// <returns>List of sensor data</returns>
        public static List<sensor_et> GetSensorDataFromCSV()
        {
            var str = File.ReadAllText(FilePath);
            if (str == "") return new ();
            var noNewLine = str.Split('\n');
            var data = new List<sensor_et>();
            for (var i = 0; i<noNewLine.Length-1;i++)
            {
                var line = noNewLine[i];
                var result = line.Split(',');
                var dataPoint = new sensor_et();
                dataPoint.Id = int.Parse(result[0]);
                dataPoint.Id_activity = int.Parse(result[1]);
                dataPoint.Id_session = int.Parse(result[2]);
                dataPoint.PosX = ParseDotStringToFloat(result[3]);
                dataPoint.PosY = ParseDotStringToFloat(result[4]); 
                dataPoint.PupilDiaX = ParseDotStringToFloat(result[5]);
                dataPoint.PupilDiaY = ParseDotStringToFloat(result[6]);
                dataPoint.HeadX = ParseDotStringToFloat(result[7]);
                dataPoint.HeadY = ParseDotStringToFloat(result[8]);
                dataPoint.HeadZ = ParseDotStringToFloat(result[9]);
                dataPoint.Validity = result[10];
                dataPoint.Timestamp = long.Parse(result[11]);
                data.Add(dataPoint);
            }
            return data;
        }

        /// <summary>
        /// Changes dot to comma in string and parses to float
        /// </summary>
        /// <param name="str">String to parse</param>
        /// <returns>string parsed to float</returns>
        private static float ParseDotStringToFloat(string str)
        {
            var splitString = str.Split('.');
            if (splitString.Length == 1)
            {
                return float.Parse(str);
            }

            var newString = splitString[0] + "," + splitString[1];
            return float.Parse(newString);
        }
    }
}
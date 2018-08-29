// Garry Cummins
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MathGame.Conditions
{
    class Score
    {
        public static int HighScore, Mode, Speed;

        public static void SaveSett(string key, string contents)
        {
            ApplicationData.Current.LocalSettings.Values[key] = contents;
        }//Save Settings

        public static string LoadSett(string key)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return settings.Values.ContainsKey(key) ? settings.Values[key] as string : String.Empty;
        }// Load Settings


    }
}

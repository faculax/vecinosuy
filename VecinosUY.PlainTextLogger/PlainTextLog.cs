using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VecinosUY.Logger;

namespace VecinosUY.PlainTextLogger
{
    public class PlainTextLog : ILogger
    {
        private static string ACTION = "accion: ";
        private static string DATE = " fecha: ";
        private static string USER = " usuario: ";
        public ICollection<string> getAllLogs(DateTime start, DateTime end)
        {
            List<string> ret;
            var logFile = File.ReadAllLines(@"C:\Logs\Log.txt");
            List<string> LogList = new List<string>(logFile);
            ret = new List<string>();
            foreach (string s in LogList)
            {
                int indexStart = s.IndexOf(DATE) + DATE.Length;
                int indexEnd = s.IndexOf("/20") + 5;
                string date = s.Substring(indexStart, indexEnd - indexStart);
                string pattern = "dd/MM/yyyy";
                DateTime parsedDate;
                DateTime.TryParseExact(date, pattern, null, DateTimeStyles.None, out parsedDate);
                int indexHourStart = s.IndexOf("&");
                string stringHour = s.Substring(indexHourStart + 1, 2);
                string stringMinutes = s.Substring(indexHourStart + 3,2);
                TimeSpan ts = new TimeSpan(Convert.ToInt16(stringHour), Convert.ToInt16(stringMinutes), 0);
                parsedDate = parsedDate.Date + ts;

                if (parsedDate > start && parsedDate < end)
                {
                    ret.Add(s);
                }
            }
            if (ret.Count == 0) {
                ret.Add("NO HAY LOGS PARA EL PERIODO SELECCIONADO");
            }
            return ret;

        }

        public void logg(string action, DateTime date, string user)
        {
            using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(@"C:\Logs\Log.txt", true))
            {
                string day = date.Day.ToString();
                if (day.Length == 1) {
                    day = "0" + day;
                }
                string month = date.Month.ToString();
                if (month.Length == 1) {
                    month = "0" + month;
                }
                int hour = DateTime.Now.Hour;
                int minutes = DateTime.Now.Minute;
                file.WriteLine(ACTION + action + DATE + day+"/"+month+ "/" + date.Year.ToString()+ "&"+hour+minutes  + USER + user);
            }
        }
    }
}

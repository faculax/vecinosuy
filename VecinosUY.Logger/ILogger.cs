using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Logger
{
    public interface ILogger
    {
        void logg(string action, DateTime date, string user);
        ICollection<string> getAllLogs(DateTime start, DateTime end);
    }
}

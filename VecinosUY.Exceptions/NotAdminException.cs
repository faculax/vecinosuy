using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Exceptions
{
    public class NotAdminException : Exception
    {
        public string Mymessage { get; set; }
        public NotAdminException(String messageParm) : base()
        {
            Mymessage = messageParm;
        }


    }
}

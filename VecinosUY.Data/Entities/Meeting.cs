using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public Boolean Deleted { get; set; }

        public Meeting()
        {

        }
    }
}

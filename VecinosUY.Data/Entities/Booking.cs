using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Booking
    {
        [Key]
        public string BookingId { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Service { get; set; }

        [Required]
        public DateTime BookedFrom { get; set; }
        [Required]
        public DateTime BookedTo { get; set; }

        public Booking()
        {

        }
    }
}

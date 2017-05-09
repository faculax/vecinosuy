using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Service
    {
        [Key]
        public string ServiceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Building Building { get; set; }

        public Service()
        {

        }
    }
}

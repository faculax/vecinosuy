using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Property 
    {
        [Key]
        public int PropertyId { get; set; }
        [Required]
        public string PropertyKey { get; set; }        
        public string Value { get; set; }

        public Property()
        {

        }
    }
}

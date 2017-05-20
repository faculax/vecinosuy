using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string YesNoQuestion { get; set; }
        [Required]
        public Boolean Deleted { get; set; }
        [Required]
        public int Yes { get; set; }
        [Required]
        public int No { get; set; }

        public Vote()
        {

        }
    }
}

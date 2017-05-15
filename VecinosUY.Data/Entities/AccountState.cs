using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class AccountState
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        public int Month { get; set; }
        [Key, Column(Order = 2)]
        public int Year { get; set; }
        [Required]
        public int Ammount { get; set; }
        [Required]
        public Boolean Deleted { get; set; }
        //  public virtual ICollection<Payment> Payments { get; set; }


        public AccountState()
        {

        }
    }
}

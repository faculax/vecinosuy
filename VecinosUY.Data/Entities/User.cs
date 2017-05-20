using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class User 
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Boolean Admin { get; set; }
        public Boolean Deleted { get; set; }
        public string Token { get; set; }
        //  public virtual ICollection<Payment> Payments { get; set; }
        [Required]
        public string Phone { get; set; }

        public User()
        {

        }
    }
}

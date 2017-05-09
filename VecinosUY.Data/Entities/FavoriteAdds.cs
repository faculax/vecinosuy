using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class FavoriteAdds
    {
        [Key]
        public string FavoriteAddsId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        
        //replace "string" for Add Entity once its pulled from the repository
        public List<string> Adds { get; set; }

        public FavoriteAdds()
        {

        }
    }
}

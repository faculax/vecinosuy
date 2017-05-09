using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class Announcement
    {

        [Key]
        public int AnnouncementId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public Boolean Deleted { get; set; }

        public string Image { get; set; }



        public Announcement()
        {

        }
    }
}

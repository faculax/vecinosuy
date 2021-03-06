﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecinosUY.Data.Entities
{
    public class UserDTO 
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public Boolean Admin { get; set; }
        public Boolean Deleted { get; set; }
        public string Phone { get; set; }

        public string Token { get; set; }

        public UserDTO() { }
    }
}

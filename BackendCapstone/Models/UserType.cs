﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCapstone.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class FlowerModel
    {
        public int FlowerID { get; set; }
        public string FlowerName { get; set; }

        [NotMapped]
        public bool FlowerChecked { get; set; }
    }
}

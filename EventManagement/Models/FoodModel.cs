using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    [NotMapped]
    public class FoodModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }

        [NotMapped]
        public bool FoodChecked { get; set; }
    }
}

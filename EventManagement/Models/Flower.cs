using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class Flower
    {
        [Key]
        public int FlowerID { get; set; }

        [Required(ErrorMessage = "Required Flower Name")]
        public string FlowerName { get; set; }

        public string FlowerFilename { get; set; }
        public string FlowerFilePath { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createdate { get; set; }

        [Required(ErrorMessage = "Required Flower Cost")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        public int? FlowerCost { get; set; }
    }
    [NotMapped]
    public class FlowerViewModel
    {
        public int FlowerID { get; set; }

        public string FlowerName { get; set; }
        public string Createdate { get; set; }
        public int? FlowerCost { get; set; }
    }
}

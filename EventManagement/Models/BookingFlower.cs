using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class BookingFlower
    {
        [Key]
        public int BookingFlowerID { get; set; }
        public int? FlowerID { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? BookingID { get; set; }

        [NotMapped]
        public List<FlowerModel> FlowerList { get; set; }
    }
}

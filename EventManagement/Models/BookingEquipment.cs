using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class BookingEquipment
    {
        [Key]
        public int BookingEquipmentID { get; set; }

        public int? EquipmentID { get; set; }

        public int? Createdby { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int BookingID { get; set; }

        [NotMapped]
        public List<EquipmentModel> EquipmentList { get; set; }
    }
}

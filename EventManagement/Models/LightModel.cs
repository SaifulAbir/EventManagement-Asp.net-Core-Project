using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class LightModel
    {
        public int LightID { get; set; }
        public string LightName { get; set; }

        [NotMapped]
        public bool LightChecked { get; set; }
    }
}

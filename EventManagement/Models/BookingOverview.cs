using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    [NotMapped]
    public class BookingOverview
    {
        public int? TotalRequest { get; set; }
        public int? TotalUpcoming { get; set; }
        public int? TotalCancel { get; set; }
        public string RecentBookingDate { get; set; }
    }

    [NotMapped]
    public class AdminOverview
    {
        public int? TotalUser { get; set; }
        public int? TotalUpcoming { get; set; }
        public int? TotalFeedback { get; set; }
        public string RecentBookingDate { get; set; }
    }
}

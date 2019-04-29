using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface ITotalbilling
    {
        BillingModel GetBillingDetailsbyBookingNo(string BookingNo);
        CompleteBookingModel ShowCompleteBookingDetailsbyBookingNo(string BookingNo);
        BookingOverview GetOverview(int Createdby);
        AdminOverview GetAdminOverview();
    }
}

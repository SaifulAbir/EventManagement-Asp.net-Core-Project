using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface IBookFood
    {
        IEnumerable<Food> FoodList(Food Food);
        int BookFood(BookingFood Food);
    }
}

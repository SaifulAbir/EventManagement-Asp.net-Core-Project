﻿using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface ICity
    {
        List<City> ListofCity(int? ID);
    }
}

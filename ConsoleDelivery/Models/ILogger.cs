﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public interface ILogger
    {
        DateTime DateTimeOfLog { get; set; }
    }
}

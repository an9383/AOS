﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

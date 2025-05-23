﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Orders
{
   public class DeliveryMethodResponse
    {
        public int Id { get; set; }
        public string ShortName { get; set; } 
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
       
    }
}

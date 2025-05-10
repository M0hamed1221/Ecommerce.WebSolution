﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
    public class BasketItemDto
    {
        public string Id { get; set; }

        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}

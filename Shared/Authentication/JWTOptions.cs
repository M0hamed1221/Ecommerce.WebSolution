﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
   public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public string Key { get; set; }

        public int durationInDays { get; set; }

    }
}

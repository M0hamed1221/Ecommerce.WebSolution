﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string DisplayNAme { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

    }
}

﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
   public class ApplicationUser:IdentityUser
    {
        public string DisplyName { get; set; }

       
        //city , street,country
        public Address Address { get; set; }

    }
    public class Address
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

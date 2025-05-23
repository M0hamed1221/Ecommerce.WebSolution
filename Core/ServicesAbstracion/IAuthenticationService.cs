﻿using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
   public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest  registerRequest);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDto> GetUserAddressAsync(string email);

        Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto);
        Task<UserResponse> GetUserByEmailASync(string email);

    }
}

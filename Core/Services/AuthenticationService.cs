using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServicesAbstracion;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            //step 01 => Find User Usong Email
            var user = await _userManager.FindByEmailAsync(loginRequest.Email)??throw new UserNotFoundException(loginRequest.Email);
            //step 02 => Check Password For This Email
           var isvaildPass=    await _userManager.CheckPasswordAsync(user,loginRequest.Password) ;
            if(isvaildPass)
            {
                return new UserResponse()
                { DisplayName=user.DisplyName,
                  Email=user.Email,
                  //step 03 => Generate Token
                  Token= "JWTToken"

                };

            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                DisplyName = registerRequest.DisplayNAme,
                PhoneNumber = registerRequest.PhoneNumber
            };
            var CreateUser = await _userManager.CreateAsync(user, registerRequest.Password);
            if (CreateUser.Succeeded) return new UserResponse()
            {
                DisplayName = user.DisplyName,
                Email = user.Email,
                Token = "JWTToken"//generate Token

            };
            else
            {
                var error = CreateUser.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(error);
            }
        }
    }
}

using AutoMapper;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstracion;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IOptions<JWTOptions> _jwtoptions , IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);
            return user != null ;
            
        }

        public async Task<AddressDto> GetUserAddressAsync(string email)
        {
            // i want to map from addressdto to application user address
            var user =  _userManager.Users.Include(u=>u.Address).FirstOrDefault(x => x.Email == email)
            ??throw new UserNotFoundException(email);
            if (user.Address is not null)
            
                return _mapper.Map<AddressDto>(user.Address);
            
          
               throw new AddressNotFoundException(user.UserName);
            

        }

        public  async   Task<UserResponse> GetUserByEmailASync(string email)
        {
            var user =  _userManager.Users.Include(u => u.Address).FirstOrDefault(x => x.Email == email)
            ?? throw new UserNotFoundException(email);

            return new  UserResponse()
                {
                     DisplayName = user.DisplyName,
                     Email = user.Email,
                     Token =  GenerateToken(user).Result
            }
            ;

        }

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
                  Token= await GenerateToken(user)

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
                Token = await GenerateToken(user)//generate Token

            };
            else
            {
                var error = CreateUser.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(error);
            }
        }

        public async Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = _userManager.Users.Include(u => u.Address).FirstOrDefault(x => x.Email == email)
            ?? throw new UserNotFoundException(email);

            if (user.Address is not null)
            {//update
                user.Address.City = addressDto.City;
                user.Address.Street = addressDto.Street;
                user.Address.Country = addressDto.Country;
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
             
            }
            else // create
            {
                user.Address=_mapper.Map<Address>(addressDto);


            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }

        private  async Task<string> GenerateToken(ApplicationUser user)
        {
            var jwtOptions = _jwtoptions.Value;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                 new Claim(ClaimTypes.Name,user.UserName!),
               
            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string secritkey = jwtOptions.Key;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secritkey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.durationInDays),
                signingCredentials: credentials
                );
            var Tokenhandler = new JwtSecurityTokenHandler();
            return Tokenhandler.WriteToken(token);
           
           
        }
    }
}

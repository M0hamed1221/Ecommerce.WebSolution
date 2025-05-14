using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthentecationController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        {
            return Ok(await _serviceManager.AuthenticationService.LoginAsync(request));
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
        {
            return Ok(await _serviceManager.AuthenticationService.RegisterAsync(request));
        }

    }
}

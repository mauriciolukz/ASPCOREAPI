using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPCOREAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASPCOREAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }

        [HttpGet("GetUserToken")]
        public string GetUserToken()
        {

            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //IList<Claim> claim = identity.Claims.ToList();
            //return claim;
            return "";
        }
    }
}

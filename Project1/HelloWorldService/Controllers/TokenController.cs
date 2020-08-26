﻿using Microsoft.AspNetCore.Mvc;

namespace HelloWorldService.Controllers
{
    public class TokenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // This should require SSL
        [HttpPost]
        public IActionResult Post([FromBody] TokenRequest tokenRequest)
        {
            var token = TokenHelper.GetToken(tokenRequest.UserName, tokenRequest.Password);

            return Ok(new Models.Token { TokenString = token });
        }

        // This should require SSL
        [HttpGet] // URL Binding
        [Route("{userName}/{password}")]
        public dynamic Get(string userName, string password)
        {
            var token = TokenHelper.GetToken(userName, password);
            return new { Token = token };
        }
    }
}
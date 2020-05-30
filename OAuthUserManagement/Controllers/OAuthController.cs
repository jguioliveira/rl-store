using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Repositories;
using UserManagement.OAuth.Configuration;
using UserManagement.OAuth.ViewModel;

namespace UserManagement.OAuth.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly Settings _appSettings;

        public OAuthController(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _appSettings = configuration.GetSection("Settings").Get<Settings>();
        }

        public IActionResult Authenticate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(
            [FromQuery]string client_id,
            [FromQuery]string scope,
            [FromQuery]string response_type,
            [FromQuery]string redirect_uri,
            [FromQuery]string state,
            UserSignIn userSignIn)
        {
            var query = new QueryBuilder();
            query.Add("state", state);

            if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(client_id))
            {
                query.Add("error", "invalid_request");
                return Redirect($"{redirect_uri}?{query}");
            }            

            if (!_appSettings.OAuthSettings.Clients.Contains(client_id))
            {
                query.Add("error", "unauthorized_client");
                return Redirect($"{redirect_uri}?{query}");
            }

            if (response_type != "code")
            {
                query.Add("error", "unsupported_response_type");
                return Redirect($"{redirect_uri}?{query}");
            }

            var user = await _userRepository.GetAsync(userSignIn.Email);

            if ((user is null) || !user.CheckPassword(userSignIn.Password))
            {
                query.Add("error", "access_denied");
            }

            var expiration = DateTime.Now.AddMinutes(5);
            string code = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Email}]:[{expiration.Ticks}"));
            query.Add("code", code);

            return Redirect($"{redirect_uri}{query}");
        }

        [HttpPost]
        public async Task<IActionResult> Token(
            string grant_type,
            string code,
            string redirect_uri,
            string client_id)
        {

            var byteCode = Convert.FromBase64String(code);
            var key = Encoding.UTF8.GetString(byteCode);
            var email = key.Split("]:[")[0];

            var user = await _userRepository.GetAsync(email);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Groups.Aggregate((a, x) => a + ", " + x))
            };

            var secret = Encoding.UTF8.GetBytes(_appSettings.OAuthSettings.Secret);

            JwtSecurityToken token = new JwtSecurityToken(
                _appSettings.OAuthSettings.Issuer, 
                _appSettings.OAuthSettings.Audience,
                claims, 
                DateTime.Now, 
                DateTime.Now.AddMinutes(15),
                new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return Ok(new { access_token = handler.WriteToken(token)});
        }
    }
}

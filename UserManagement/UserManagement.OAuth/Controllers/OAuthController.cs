using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.OAuth.Configuration;
using UserManagement.OAuth.ViewModel;

namespace UserManagement.OAuth.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly Settings _appSettings;

        public OAuthController(
            IUserRepository userRepository,
            IUserTokenRepository userTokenRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
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

            UserToken userToken = new UserToken(user.Email, redirect_uri, DateTime.Now.AddMinutes(5).Ticks);
            await _userTokenRepository.CreateAsync(userToken);

            query.Add("code", userToken.AuthorizationCode);

            return Redirect($"{redirect_uri}{query}");
        }

        [HttpPost]
        public async Task<IActionResult> PortalAuthenticate([FromBody]UserSignIn userSignIn)
        {
            var user = await _userRepository.GetAsync(userSignIn.Email);

            if ((user is null) || !user.CheckPassword(userSignIn.Password))
            {
                return Unauthorized(new { error = "Invalid username or password." });
            }

            UserToken userToken = new UserToken(user.Email, "frontend", DateTime.Now.AddMinutes(5).Ticks);
            await _userTokenRepository.CreateAsync(userToken);

            dynamic token = await TokenGenerate(userToken);

            return Ok(
                new 
                {
                    token.access_token,
                    token.token_type,
                    user = new { email = user.Email, firstName = user.FirstName, lastName = user.LastName, groups = user.Groups } 
                });
        }

        [HttpPost]
        public async Task<IActionResult> Token(
            string grant_type,
            string code,
            string redirect_uri,
            string client_id)
        {

            UserToken userToken = await _userTokenRepository.GetAsync(code);

            if(userToken is null || userToken.IsCanceled || redirect_uri != userToken.RedirectUri )
            {
                return BadRequest(new { error = "invalid_request" });
            }
            else if (!string.IsNullOrEmpty(userToken.Token) || new DateTime(userToken.AuthorizationCodeExpiration) <= DateTime.Now)
            {
                userToken.Cancel();
                await _userTokenRepository.UpdateAsync(userToken.Id, userToken);
                return BadRequest(new { error = "invalid_request" });
            }
            else
            {                
                var objectToken = await TokenGenerate(userToken);
                return Ok(objectToken);
            }            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Validate()
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var email = HttpContext.User.Identity.Name;

            if(!string.IsNullOrEmpty(email) && HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetAsync(email);

                return Ok(
                     new
                     {
                         access_token,
                         token_type = "bearer",
                         user = new { email = user.Email, firstName = user.FirstName, lastName = user.LastName, groups = user.Groups }
                     });
            }

            return BadRequest("invalid token.");
        }

        private async Task<object> TokenGenerate(UserToken userToken)
        {
            var user = await _userRepository.GetAsync(userToken.Email);
            List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Groups.Aggregate((a, x) => a + ", " + x))
                };

            var secret = Encoding.UTF8.GetBytes(_appSettings.OAuthSettings.Secret);
            var expirationToken = DateTime.Now.AddDays(1);

            JwtSecurityToken token = new JwtSecurityToken(
                _appSettings.OAuthSettings.Issuer,
                _appSettings.OAuthSettings.Audience,
                claims,
                DateTime.Now,
                expirationToken,
                new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var access_token = handler.WriteToken(token);
            userToken.SetToken(access_token, expirationToken.Ticks);
            await _userTokenRepository.UpdateAsync(userToken.Id, userToken);

            return new
            {
                access_token,
                token_type = "bearer",
                expires_in = (int)(expirationToken - DateTime.Now).TotalSeconds,
                refresh_token = ""
            };
        }
    }
}

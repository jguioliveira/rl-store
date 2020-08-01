using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InventoryManagement.Api.Requirements
{
    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        private readonly string _uriTokenValidation;

        public JwtRequirementHandler(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
            _uriTokenValidation = configuration.GetValue<string>("UriTokenValidation");
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirement requirement)
        {
            if(_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var authHeaderArr = authHeader.FirstOrDefault().Split(' ');

                if(authHeaderArr.Length < 2 || !"Bearer".Equals(authHeaderArr.First(), System.StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(authHeaderArr.Last()))
                    context.Fail();

                var accessToken = authHeaderArr.Last();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await _httpClient.GetAsync(_uriTokenValidation);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    context.Succeed(requirement);
                else
                    context.Fail();
            }
            else
                context.Fail();
        }
    }
}

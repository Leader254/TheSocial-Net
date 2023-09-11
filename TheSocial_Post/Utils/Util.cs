using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace TheSocial_Post.Utils
{
    public class Util : DelegatingHandler
    {

        private readonly IHttpContextAccessor _accessor;

        public Util(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

// Import the necessary namespace for authentication.
using Microsoft.AspNetCore.Authentication;
// Import the necessary namespace for HTTP headers.
using System.Net.Http.Headers;

namespace TheSocial_Post.Utils
{
    public class Util : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public Util(IHttpContextAccessor accessor)
        {
            // Constructor takes an IHttpContextAccessor as a parameter and assigns it to the _accessor field.
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Inside the SendAsync method, we retrieve an access token from the current HttpContext.
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            // Set the "Authorization" header of the outgoing HTTP request with the bearer token.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Call the base.SendAsync method to continue processing the HTTP request.
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

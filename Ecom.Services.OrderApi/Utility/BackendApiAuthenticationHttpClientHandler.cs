﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Ecom.Services.OrderApi.Utility
{
    public class BackendApiAuthenticationHttpClientHandler :DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

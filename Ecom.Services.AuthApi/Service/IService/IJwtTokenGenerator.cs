﻿using Ecom.Services.AuthApi.Models;

namespace Ecom.Services.AuthApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}

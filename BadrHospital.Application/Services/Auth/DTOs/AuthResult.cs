using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.DTOs
{
    public class AuthResult
    {
        public AuthResult(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}

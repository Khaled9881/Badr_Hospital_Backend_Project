using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.DTOs
{
    public class LoginResultDTO
    {
        public bool isSignedInSuccessfully { get; set; }
        public string? FailureMessage { get; set; }
        public string? token { get; set; }
        public string? RefreshToken { get; set; }

    }
}

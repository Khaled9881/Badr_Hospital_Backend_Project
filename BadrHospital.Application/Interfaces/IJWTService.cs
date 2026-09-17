using BadrHospital.Application.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Interfaces
{
    public interface IJWTService
    {
        public string CreateToken(TokenRequest tokenRequest);
    }
}

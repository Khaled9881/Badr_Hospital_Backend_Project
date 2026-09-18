using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Common
{
    public class TokenRequest
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}

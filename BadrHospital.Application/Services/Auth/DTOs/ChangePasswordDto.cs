using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.DTOs
{
    public record ChangePasswordDto(string CurrentPassword, string NewPassword);
}

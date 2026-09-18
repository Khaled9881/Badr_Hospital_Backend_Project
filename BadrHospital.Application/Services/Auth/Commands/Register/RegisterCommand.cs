using BadrHospital.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Services.Auth.Commands.Register
{
    public record RegisterCommand(string Email, string Password, string userName, string FirstName, string LastName, DateTime DateOfBirth, Gender Gender, string PhoneNumber, string Address) : IRequest<string>;
}

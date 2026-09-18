using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using BadrHospital.Application.Services.Patient.Commands.CreatePatient;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace BadrHospital.Application.Services.Auth.Commands.Register
{
    public class RegisterCommandHandler(IMediator mediator) : IRequestHandler<RegisterCommand, string>
    {
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreatePatientCommand(
            request.Email, request.Password, request.userName, request.FirstName, request.LastName, request.DateOfBirth, request.Gender, request.PhoneNumber, request.Address
            ), cancellationToken);
        }
    }
}

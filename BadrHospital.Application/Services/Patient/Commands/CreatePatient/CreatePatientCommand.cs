using BadrHospital.Domain.Enums;
using MediatR;


namespace BadrHospital.Application.Services.Patient.Commands.CreatePatient
{
    public record CreatePatientCommand(string Email, string Password, string userName, string FirstName, string LastName, DateTime DateOfBirth, Gender Gender, string PhoneNumber, string Address) : IRequest<string>;
}

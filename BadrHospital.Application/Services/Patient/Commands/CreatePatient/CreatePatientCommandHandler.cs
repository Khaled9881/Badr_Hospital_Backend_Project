using BadrHospital.Application.Common.Exceptions;
using BadrHospital.Application.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BadrHospital.Application.Services.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandHandler(IIdentityService identityService, IJWTService jWTService, ITransactionManager transactionManager, IApplicationDbContext dbContext) : IRequestHandler<CreatePatientCommand, string>
    {
        public async Task<string> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {

            if (await identityService.FindByEmailAsync(request.Email))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Email", "Email is Already Existed") }
                );
            if (await identityService.FindByNameAsync(request.userName))
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("UserName", "User name is already taken") });


            var token = await transactionManager.ExecuteTransactionAsync<string>(async () =>
            {
                var (Result, UserId) = await identityService.CreateUserAsync(request.Email, request.Password, request.userName, request.PhoneNumber);

                if (!Result.Succeeded)
                    ThrowValidationException(Result);

                var AddtoRoleResult = await identityService.AddtoRoleAsync(UserId.ToString(), "Patient");
                if (!AddtoRoleResult.Succeeded)
                    ThrowValidationException(AddtoRoleResult);



                var patient = new HospitalManagementSystem.Domain.Patients.Patient()
                {
                    //Id = Guid.NewGuid(),
                    ApplicationUserId = UserId,
                    MedicalRecordNumber = $"MRN-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}",
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address
                };

                var UniqueConstraintDbConflicts = dbContext.Patients.Where(p => p.PhoneNumber == request.PhoneNumber).FirstOrDefault();
                if (UniqueConstraintDbConflicts != null)
                {
                    var failures = new List<ValidationFailure>();
                    if (UniqueConstraintDbConflicts.PhoneNumber == request.PhoneNumber)
                        failures.Add(new ValidationFailure("PhoneNumber", "Phone number is already registered."));
                    // etc.
                    if (failures.Count > 0) throw new ValidationException(failures);
                }

                await dbContext.Patients.AddAsync(patient, cancellationToken);
                await dbContext.SaveChangesAsync(true, cancellationToken);

                return jWTService.CreateToken(new Common.TokenRequest()
                {
                    UserId = UserId.ToString(),
                    Email = request.Email,
                    Roles = new List<string>() { "Patient" }
                });
            }, cancellationToken);

            return token;
        }

        // Helper Function
        [DoesNotReturn]
        private void ThrowValidationException(IdentityResult result)
        {
            var errors = string.Join(" ", result.Errors.Select(e => e.Description));
            throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Errors", errors) });
        }

    }


}

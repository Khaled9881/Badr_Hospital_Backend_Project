using HospitalManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using BadrHospital.Application.Interfaces;

namespace BadrHospital.Infrastructure.Services
{
    public class TransactionManager(ApplicationDbContext dbContext) : ITransactionManager
    {
        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            await this.ExecuteTransactionAsync<bool>(async () =>
            {
                await action();
                return true;
            }, cancellationToken);
        }

        public async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async (ct) =>
            {
                await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var result = await action();
                    await transaction.CommitAsync(ct);
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync(ct);
                    throw;
                }

            }, cancellationToken);

        }
    }
}

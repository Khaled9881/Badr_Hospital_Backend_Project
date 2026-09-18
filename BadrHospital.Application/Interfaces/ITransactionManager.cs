using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Interfaces
{
    public interface ITransactionManager
    {
        public Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken);

        public Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken);
    }
}

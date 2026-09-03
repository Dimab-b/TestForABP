using System;
using System.Collections.Generic;
using System.Text;

namespace TestForABP.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

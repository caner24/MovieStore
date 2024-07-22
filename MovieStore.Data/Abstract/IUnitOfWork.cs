using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        ICastDal CastDal { get; }
        ICustomerDal CustomerDal { get; }
        IDirectorDal DirectorDal { get; }
        IKindDal KindDal { get; }
        IMovieDal MovieDal { get; }
    }
}

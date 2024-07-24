using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieStoreContext _context;
        private IDbContextTransaction? _transaction;
        private readonly ICastDal _castDal;
        private readonly ICustomerDal _customerDal;
        private readonly IDirectorDal _directorDal;
        private readonly IKindDal _kindDal;
        private readonly IMovieDal _movieDal;

        public UnitOfWork(MovieStoreContext context, ICastDal castDal, ICustomerDal customerDal, IDirectorDal directorDal, IKindDal kindDal, IMovieDal movieDal)
        {
            _context = context;
            _castDal = castDal;
            _customerDal = customerDal;
            _directorDal = directorDal;
            _kindDal = kindDal;
            _movieDal = movieDal;
        }

        public ICastDal CastDal => _castDal;
        public ICustomerDal CustomerDal => _customerDal;
        public IDirectorDal DirectorDal => _directorDal;
        public IKindDal KindDal => _kindDal;
        public IMovieDal MovieDal => _movieDal;
        public MovieStoreContext Context => _context;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _transaction?.Dispose();
        }
    }
}

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
        private readonly Lazy<ICastDal> _castDal;
        private readonly Lazy<ICustomerDal> _customerDal;
        private readonly Lazy<IDirectorDal> _directorDal;
        private readonly Lazy<IKindDal> _kindDal;
        private readonly Lazy<IMovieDal> _movieDal;

        public UnitOfWork(MovieStoreContext context, Lazy<ICastDal> castDal, Lazy<ICustomerDal> customerDal, Lazy<IDirectorDal> directorDal, Lazy<IKindDal> kindDal, Lazy<IMovieDal> movieDal)
        {
            _context = context;
            _castDal = castDal;
            _customerDal = customerDal;
            _directorDal = directorDal;
            _kindDal = kindDal;
            _movieDal = movieDal;
        }

        public ICastDal CastDal => _castDal.Value;
        public ICustomerDal CustomerDal => _customerDal.Value;
        public IDirectorDal DirectorDal => _directorDal.Value;
        public IKindDal KindDal => _kindDal.Value;
        public IMovieDal MovieDal => _movieDal.Value;

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

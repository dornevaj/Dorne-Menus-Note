using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Evernote.DAL.Generic_Repository;
using Evernote.Entities.DataBaseContext;

namespace Evernote.DAL.Unit_Of_Works
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _dbContext;
        public UnitOfWork()
        {
            _dbContext = new DataBaseContext();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new EfGenericRepository<T>(_dbContext);
        }
        //public IGenericRepository<T> GenericRepository
        //{
        //    get { return _modelRepository ?? (_modelRepository = new GenericRepository<Model>(_context)); }
        //}

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch
            {
                //log error
                throw;
               
            }
        }

        public Task<int> SaveASync()
        {
            try
            {
                return _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

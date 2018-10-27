using Evernote.DAL.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.DAL.Unit_Of_Works
{
   public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        //IGenericRepository<T> GenericRepository { get; } 

        int SaveChanges();
        Task<int> SaveASync();
    }
}

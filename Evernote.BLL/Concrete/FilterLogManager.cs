using Evernote.BLL.Abstract;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.Models;

namespace Evernote.BLL.Concrete
{
    public class FilterLogManager : IFilterLogService
    {
        public void Add(FilterLog filterLog)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<FilterLog>().Add(filterLog);
                uow.SaveChanges();
            }
        }
    }
}

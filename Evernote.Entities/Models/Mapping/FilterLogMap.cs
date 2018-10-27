using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.Entities.Models.Mapping
{
  public  class FilterLogMap : EntityTypeConfiguration<FilterLog>
    {
        public FilterLogMap()
        {
            this.HasRequired<User>(u => u.User)
                .WithMany(f => f.FilterLogs)
                .HasForeignKey<long>(o => o.UserId);
        }
    }
}

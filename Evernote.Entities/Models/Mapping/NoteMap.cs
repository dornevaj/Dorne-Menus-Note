using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.Entities.Models.Mapping
{
    public class NoteMap : EntityTypeConfiguration<Note>
    {
       public NoteMap()
        {
            //one to many relationship between User(one) and Notes(many)
            this.HasRequired<User>(u => u.User)
                .WithMany(n => n.Notes)
                .HasForeignKey<long>(f => f.UserId)
                 .WillCascadeOnDelete(false);

            //one to many relationship between Category(one) and Notes(many)
            this.HasRequired<Category>(u => u.Category)
               .WithMany(n => n.Notes)
               .HasForeignKey<long>(f => f.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}

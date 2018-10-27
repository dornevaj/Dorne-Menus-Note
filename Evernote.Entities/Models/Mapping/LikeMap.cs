
using System.Data.Entity.ModelConfiguration;

namespace Evernote.Entities.Models.Mapping
{
    public class LikeMap : EntityTypeConfiguration<Like>
    {
        
        public LikeMap()
        {
            //one to many relationship between User(one) and Like(many)
            this.HasRequired<User>(u => u.User)
                .WithMany(l => l.Likes)
                .HasForeignKey<long>(f => f.UserId)
                 .WillCascadeOnDelete(false);

            //one to many relationship between Note(one) and Like(many)
            this.HasRequired<Note>(u => u.Note)
                .WithMany(l => l.Likes)
                .HasForeignKey<long>(f => f.NoteId)
                 .WillCascadeOnDelete(false);
        }
    }
}


using System.Data.Entity.ModelConfiguration;

namespace Evernote.Entities.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            //one to many relationship between User(one) and Comment(many)
            this.HasRequired<User>(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey<long>(f => f.UserId)
                .WillCascadeOnDelete(false);

            //one to many relationship between Note(one) and Comment(many)
            this.HasRequired<Note>(n => n.Note)
                .WithMany(c => c.Comments)
                .HasForeignKey<long>(f => f.NoteId)
                 .WillCascadeOnDelete(false);
            
        }
    }
}

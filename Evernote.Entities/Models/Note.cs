using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evernote.Entities.Models
{
    public class Note : BaseEntity
    {
        public Note()
        {
            this.Comments = new List<Comment>();
            this.Likes = new List<Like>();
        }

        [Key]
        public long NoteId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public Nullable<bool> IsDraft { get; set; }

        public long UserId { get; set; }
        public long CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}

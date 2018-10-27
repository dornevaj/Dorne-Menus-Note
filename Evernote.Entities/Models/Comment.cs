using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evernote.Entities.Models
{
    public class Comment : BaseEntity
    {
        [Key]
        public long CommentId { get; set; }
        public string Text { get; set; }

        public long UserId { get; set; }
        public long NoteId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("NoteId")]
        public virtual Note Note { get; set; }

    }
}

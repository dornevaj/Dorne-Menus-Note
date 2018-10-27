using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evernote.Entities.Models
{
  public  class Like : BaseEntity
    {
        [Key]
        public long LikeId { get; set; }
        public long UserId { get; set; }
        public long NoteId { get; set; }

        [ForeignKey("NoteId")]
        public virtual Note Note { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}

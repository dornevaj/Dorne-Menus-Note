using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evernote.Entities.Models
{
   public class Category : BaseEntity
    {
        public Category()
        {
            this.Notes = new List<Note>();
        }

        [Key]
        public long CategoryId { get; set; }
        public String Title { get; set; }
        public String Description  { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}

using System;

namespace Evernote.Entities.Models
{
   public class BaseEntity
    {
        public Nullable<long> CreateUser { get; set; }
        public Nullable<long> UpdateUser { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}

using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evernote.Entities.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Notes = new List<Note>();
            this.Likes = new List<Like>();
            this.FilterLogs = new List<FilterLog>();
        }

        [Key]
        public long UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NickName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public Guid UserGuid { get; set; }

        public UserRoles UserRole { get; set; }
        public enum UserRoles
        {
            SuperAdmin = 1,
            Admin = 2,
            Normal = 3,
        }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<FilterLog> FilterLogs { get; set; }
    }
}

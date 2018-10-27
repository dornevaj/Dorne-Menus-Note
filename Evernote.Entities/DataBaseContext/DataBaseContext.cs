
using Evernote.Entities.Models;
using Evernote.Entities.Models.Mapping;
using System;
using System.Data.Entity;

namespace Evernote.Entities.DataBaseContext
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=DataBaseContext")
        {
            //Database.SetInitializer<DataBaseContext>(new CreateDatabaseIfNotExists<DataBaseContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Evernote.Entities.Migrations.Configuration>("DataBaseContext"));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FilterLog> FilterLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new NoteMap());
            modelBuilder.Configurations.Add(new FilterLogMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {

            foreach (var BaseEntries in ChangeTracker.Entries<BaseEntity>())
            {
                if (BaseEntries.State == EntityState.Added)
                {
                    BaseEntries.Entity.CreatedDate = DateTime.Now;
                }

                //update date
                if (BaseEntries.State == EntityState.Modified)
                {
                    BaseEntries.Entity.ModifiedDate = DateTime.Now;
                }

                //if deleted, save deleting date -> if you want
                //if this record deleted, gona add another database 
                //if (BaseEntries.State == EntityState.Deleted)
                //{

                //}
            }

            return base.SaveChanges();
        }
    }
}

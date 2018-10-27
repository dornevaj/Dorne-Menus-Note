namespace Evernote.Entities.Migrations
{
    using Evernote.Entities.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Evernote.Entities.DataBaseContext.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Evernote.Entities.DataBaseContext.DataBaseContext context)
        {
            if (!context.Users.Any())
            {
                User admin = new User();

                admin.UserId = 1;
                admin.IsActive = true;
                admin.Name = "Admin";
                admin.NickName = "Admin";
                admin.SurName = "Admin";
                admin.UserRole = User.UserRoles.Admin;
                admin.Password = Helper.MD5.Calculate("123");
                admin.EmailAddress = "Admin@Admin.com";

                context.Users.Add(admin);

                for (int i = 2; i < 11; i++)
                {
                    User user = new User();
                    user.UserId = i;
                    user.IsActive = true;
                    user.Name = FakeData.NameData.GetMaleFirstName();
                    user.NickName = FakeData.NameData.GetFirstName();
                    user.SurName = FakeData.NameData.GetSurname();
                    user.UserRole = User.UserRoles.Normal;
                    user.Password = Helper.MD5.Calculate("123");
                    user.EmailAddress = user.Name + "" + user.SurName + "@mail.com";

                    context.Users.Add(user);
                }
            }

            if (!context.Categories.Any())
            {
                for (int i = 1; i < 11; i++)
                {
                    Category category = new Category();
                    category.CategoryId = i;
                    category.Description = FakeData.TextData.GetSentences(i);
                    category.Title = "Category-" + i;

                    context.Categories.Add(category);
                }                
            }

            if (!context.Notes.Any())
            {
                for (int i = 1; i < 11; i++)
                {
                    Note note = new Note();
                    note.NoteId = i;
                    note.CategoryId = i;
                    note.UserId = i;
                    note.IsDraft = false;
                    note.Title = FakeData.TextData.GetSentence();
                    note.Text = FakeData.TextData.GetSentences(i);

                    context.Notes.Add(note);
                }
            }

            if (!context.Comments.Any())
            {
                for (int i = 1; i < 11; i++)
                {
                    Comment comment = new Comment();
                    comment.CommentId = i;
                    comment.NoteId = i;
                    comment.UserId = i;
                    comment.Text = FakeData.TextData.GetSentence();

                    context.Comments.Add(comment);
                }
            }

            if (!context.Likes.Any())
            {
                for (int i = 1; i < 11; i++)
                {
                    Like like = new Like();
                    like.LikeId = i;
                    like.NoteId = i;
                    like.UserId = i;

                    context.Likes.Add(like);
                }
            }

            base.Seed(context);
        }
    }
}

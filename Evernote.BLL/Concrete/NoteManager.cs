using Evernote.BLL.Abstract;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Concrete
{
    public class NoteManager : INoteService
    {
        public void AddNote(Note note)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Note>().Add(note);
                uow.SaveChanges();
            }
        }

        public void DeleteNote(Note note)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Note>().Delete(note);
                uow.SaveChanges();
            }
        }

        public void DeleteNoteById(long id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Note>().DeleteById(id);
                uow.SaveChanges();
            }
        }

        public IEnumerable<Note> GetAllNotes(System.Linq.Expressions.Expression<Func<Note, bool>> predicate = null, Func<IQueryable<Note>, IOrderedQueryable<Note>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            List<Note> notes;
            using (var uow = new UnitOfWork())
            {
                notes = new List<Note>();
                notes.AddRange(uow.GenericRepository<Note>().GetAll(predicate:predicate,orderBy:orderBy,includeProperties:includeProperties,skip:skip,take:take));
            }

            return notes;
        }

        public Note GetNote(Expression<Func<Note, bool>> predicate)
        {
            Note note;
            using (var uow = new UnitOfWork())
            {
                note = new Note();
                note = uow.GenericRepository<Note>().Get(predicate:predicate);
            }
            return note;
        }

        public Note GetNoteById(long id)
        {
            Note note;
            using (var uow = new UnitOfWork())
            {
                note = new Note();
                note = uow.GenericRepository<Note>().GetById(id);
            }
            return note;
        }

        public Note GetNoteByIdInclude(long id, string includeProperties = "")
        {
            Note note;
            using (var uow = new UnitOfWork())
            {
                note = new Note();
                note = uow.GenericRepository<Note>().GetByIdInclude(x=>x.NoteId==id, includeProperties:includeProperties);
            }
            return note;
        }

        public void UpdateNote(Note note)
        {
            using (var uow = new UnitOfWork())
            {
                uow.GenericRepository<Note>().Update(note);
                uow.SaveChanges();
            }
        }
    }
}

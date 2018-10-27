using Evernote.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Evernote.BLL.Abstract
{
   public interface INoteService
    {
        IEnumerable<Note> GetAllNotes(Expression<Func<Note, bool>> predicate = null, Func<IQueryable<Note>, IOrderedQueryable<Note>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        Note GetNote(Expression<Func<Note, bool>> predicate);
        Note GetNoteById(long id);
        Note GetNoteByIdInclude(long id, string includeProperties = "");
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNoteById(long id);
        void DeleteNote(Note note);
    }
}

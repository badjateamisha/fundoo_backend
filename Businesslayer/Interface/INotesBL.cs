using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNotes(notesModel note, long UserID);

        public IEnumerable<NotesEntity> ReadNotes(long userId);

        public NotesEntity UpdateNotes(notesModel note, long NoteID);

    }
}

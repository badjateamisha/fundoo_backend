using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(notesModel note, long UserID);

        public IEnumerable<NotesEntity> ReadNotes(long userId);

        public NotesEntity UpdateNotes(notesModel note, long NoteID);

        public bool DeleteNotes(long userId, long NoteID);

        public bool Archive(long userId, long NoteID);

        public bool Pin(long userId, long NoteID);

        public bool Trash(long userId, long NoteID);

        public NotesEntity Color(long NoteID, string color);

        public string Image(IFormFile image, long NoteID, long userID);
    }
}

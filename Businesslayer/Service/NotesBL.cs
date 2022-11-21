
using Businesslayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Service
{
    public class NotesBL : INotesBL
    {
        INotesRL noteRL;

        public NotesBL(INotesRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public NotesEntity AddNotes(notesModel note, long UserID)
        {
            try
            {
                return noteRL.AddNotes(note,UserID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                return noteRL.ReadNotes(userId);
            }
            catch(Exception)
            {
                return null;
            }
        }


    }
}

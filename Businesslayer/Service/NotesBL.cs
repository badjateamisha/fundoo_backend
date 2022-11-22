
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

        public NotesEntity UpdateNotes(notesModel note, long NoteID)
        {
            try
            {
                return noteRL.UpdateNotes(note,NoteID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteNotes(long userId, long NoteID)
        {
            try
            {
                return noteRL.DeleteNotes(userId,NoteID);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Archive(long userId, long NoteID)
        {
            try
            {
                return noteRL.Archive(userId, NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Pin(long userId, long NoteID)
        {
            try
            {
                return noteRL.Pin(userId, NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Trash(long userId, long NoteID)
        {
            try
            {
                return noteRL.Trash(userId, NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity Color(long NoteID, string color)
        {
            try
            {
                return noteRL.Color(NoteID,color);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

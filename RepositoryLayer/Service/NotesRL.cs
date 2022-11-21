using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        
        FundooContext fundooContext;

        private readonly IConfiguration config;

        public NotesRL(FundooContext fundooContext, IConfiguration config)

        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        public NotesEntity AddNotes(notesModel note, long UserID)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = note.Title;
                notesEntity.Description = note.Description;
                notesEntity.Remainder = note.Remainder;
                notesEntity.Color = note.Color;
                notesEntity.Image = note.Image;
                notesEntity.Archive = note.Archive;
                notesEntity.Trash = note.Trash;
                notesEntity.Pin = note.Pin;
                notesEntity.Editedat = note.Editedat;
                notesEntity.Createdat = note.Createdat;
                notesEntity.UserId = UserID;

                fundooContext.NotesTable.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return notesEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

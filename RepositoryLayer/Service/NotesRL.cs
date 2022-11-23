using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                var result = this.fundooContext.NotesTable.Where(e => e.UserId == userId);
                return result;
            }

            catch (Exception)
            {

                throw;
            }

        }

        public NotesEntity UpdateNotes(notesModel note, long NoteID)
        {
            try
            {
                NotesEntity notesEntity = fundooContext.NotesTable.Where(X => X.NoteID == NoteID).FirstOrDefault();
                if (notesEntity != null)
                {
                    notesEntity.Title = note.Title;
                    notesEntity.Description = note.Description;
                    notesEntity.Remainder = note.Remainder;
                    notesEntity.Color = note.Color;
                    notesEntity.Image = note.Image;
                    notesEntity.Archive = note.Archive;
                    notesEntity.Trash = note.Trash;
                    notesEntity.Pin = note.Pin;
                    notesEntity.Editedat = note.Editedat;

                    fundooContext.NotesTable.Update(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotes(long userId,long NoteID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault(); ;

                if (result != null)
                {
                    fundooContext.Remove(result);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Archive(long userId, long NoteID)
        {
            try
            {
                var output = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault(); 

                if (output.Archive == true)
                {
                    output.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    output.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault(); ;

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault(); ;

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.First(e => e.NoteID == NoteID);
                if (result != null)
                {
                    result.Color = color;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string Image(IFormFile image, long NoteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userID && x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                       this.config["CloudinarySettings:CloudName"],
                       this.config["CloudinarySettings:ApiKey"],
                        this.config["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imgPath = uploadResult.Url.ToString();
                    result.Image = imgPath;
                    fundooContext.SaveChanges();
                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

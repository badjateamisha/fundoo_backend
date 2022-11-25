using CommonLayer.Model;
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
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;

        
        public CollabRL(FundooContext fundooContext)

        {
            this.fundooContext = fundooContext;
        }

        public CollaboratorEntity AddCollaborator(collaboratorModel  collab,long UserID, long NoteId)
        {
            try
            {
                CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                collaboratorEntity.Email = collab.Email;
                collaboratorEntity.UserId = UserID;
                collaboratorEntity.NoteID = NoteId;

                fundooContext.CollaboratorTable.Add(collaboratorEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collaboratorEntity;
                }
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CollaboratorEntity> ReadCollaborator(long UserID)
        {
            try
            {
                var result = this.fundooContext.CollaboratorTable.Where(e => e.UserId == UserID);
                if (result != null)
                {
                    return result;
                }
                else
                    return null;
            }

            catch (Exception)
            {

                throw;
            }

        }

        public bool DeleteCollaborator(long userId, long NoteID, long CollaboratorId)
        {
            try
            {
                var result = fundooContext.CollaboratorTable.Where(x => x.UserId == userId && x.NoteID == NoteID && x.CollaboratorID == CollaboratorId).FirstOrDefault(); ;

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

    }
}

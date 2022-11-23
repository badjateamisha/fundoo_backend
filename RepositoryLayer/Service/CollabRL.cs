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

        private readonly IConfiguration config;

        public CollabRL(FundooContext fundooContext, IConfiguration config)

        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        public CollaboratorEntity AddCollaborator(collaboratorModel collab, long UserID, long NoteId)
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

        public IEnumerable<CollaboratorEntity> ReadCollaborator(long UserID,long NoteID)
        {
            try
            {
                var result = this.fundooContext.CollaboratorTable.Where(e => e.UserId == UserID);
                return result;
            }

            catch (Exception)
            {

                throw;
            }

        }

    }
}

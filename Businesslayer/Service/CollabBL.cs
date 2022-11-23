using Businesslayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Service
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRL;

        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollaboratorEntity AddCollaborator(collaboratorModel collab, long UserID, long NoteId)
        {
            try
            {
                return collabRL.AddCollaborator(collab, UserID,NoteId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<CollaboratorEntity> ReadCollaborator(long UserID, long NoteID)
        {
            try
            {
                return collabRL.ReadCollaborator(UserID,NoteID);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

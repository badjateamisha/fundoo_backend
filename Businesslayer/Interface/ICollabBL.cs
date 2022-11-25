using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interface
{
    public interface ICollabBL
    {
        public CollaboratorEntity AddCollaborator(collaboratorModel collab, long UserID, long NoteId);

        public IEnumerable<CollaboratorEntity> ReadCollaborator(long UserID);

        public bool DeleteCollaborator(long userId, long NoteID, long CollaboratorId);
    }
}

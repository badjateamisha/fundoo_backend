using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollaboratorEntity AddCollaborator(collaboratorModel collab, long UserID, long NoteId);
    }
}

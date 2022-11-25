using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public bool AddLabel(string name, long NoteId, long UserID);

        public IEnumerable<LabelEntity> ReadLabel(long UserID);
    }
}

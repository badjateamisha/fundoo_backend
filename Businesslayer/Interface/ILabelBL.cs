using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interface
{
    public interface ILabelBL
    {
        public bool AddLabel(string name, long NoteId, long UserID);

        public IEnumerable<LabelEntity> ReadLabel(long UserID);

        public LabelEntity UpdateLabel(string name, long NoteID);

        public bool DeleteLabel(long userId, long LabelId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Interface
{
    public interface ILabelBL
    {
        public bool AddLabel(string name, long NoteId, long UserID);
    }
}

using Businesslayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public bool AddLabel(string name, long NoteId, long UserID)
        {
            try
            {
                return labelRL.AddLabel(name, NoteId,UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> ReadLabel( long UserID)
        {
            try
            {
                return labelRL.ReadLabel( UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity UpdateLabel(string name, long NoteID)
        {
            try
            {
                return labelRL.UpdateLabel(name,NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

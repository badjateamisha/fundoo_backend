using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)

        {
            this.fundooContext = fundooContext;
        }

        public bool AddLabel(string name, long NoteId, long UserID)
        {
            try
            {
                                
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.Name = name;
                    labelEntity.NoteID = NoteId;
                    labelEntity.UserId = UserID;
                    fundooContext.LabelTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return true;
                
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LabelEntity> ReadLabel(long UserID)
        {
            try
            {
                var result = this.fundooContext.LabelTable.Where(e => e.UserId == UserID);
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
    }
}

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
    }
}

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

        public LabelEntity UpdateLabel(string name, long NoteID)
        {
            try
            {
                LabelEntity labelEntity = fundooContext.LabelTable.Where(X => X.NoteID == NoteID).FirstOrDefault();
                if (labelEntity != null)
                {
                    labelEntity.Name = name;
                    fundooContext.LabelTable.Update(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long userId, long LabelId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.UserId == userId && x.LabelID == LabelId).FirstOrDefault(); ;

                if (result != null)
                {
                    fundooContext.Remove(result);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

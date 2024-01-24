using Microsoft.Extensions.Configuration;
using Model_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class Label_RL : ILabel_RL
    {
        private readonly FundooDB_Context fundoodbContext;
        private readonly IConfiguration configuration;

        public Label_RL(FundooDB_Context fundoodbContext, IConfiguration configuration)
        {
            this.fundoodbContext = fundoodbContext;
            this.configuration = configuration;
        }

        public LabelEntity AddLabel(Label_ML label_ML, long noteId, long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.LabelName = label_ML.LabelName;
                UserEntity user = fundoodbContext.Users.FirstOrDefault(x => x.UserId == userId);
                NoteEntity note = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                labelEntity.UsersTable = user;
                labelEntity.Notes = note;
                fundoodbContext.Labels.Add(labelEntity);
                int result = fundoodbContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<LabelEntity> GetAllLabels(long labelId, long UserId, long noteId)
        {
            try
            {
                List<LabelEntity> result = fundoodbContext.Labels.Where(x => x.UserId == UserId && x.NoteId == noteId && x.LabelId == labelId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateLabel(long labelId, long noteId, long userId, Label_ML label_ML)
        {
            try
            {
                var result = fundoodbContext.Labels.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId);
                if (result != null)
                {
                    result.LabelName = label_ML.LabelName;
                    fundoodbContext.SaveChanges();
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
        public bool DeleteLabel(long labelId, long noteId, long userId)
        {
            try
            {
                var result = fundoodbContext.Labels.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId);
                if (result != null)
                {
                    fundoodbContext.Labels.Remove(result);
                    fundoodbContext.SaveChanges();
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

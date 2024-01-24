using Business_Layer.Interfaces;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class Label_BL : ILabel_BL
    {
        private readonly ILabel_RL ilabel_RL;

        public Label_BL(ILabel_RL ilabel_RL)
        {
            this.ilabel_RL = ilabel_RL;
        }

        public LabelEntity AddLabel(Label_ML label_ML, long noteId, long userId)
        {
            try
            {
                return this.ilabel_RL.AddLabel(label_ML, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelEntity> GetAllLabels(long labelId, long UserId, long noteId)
        {
            try
            {
                return this.ilabel_RL.GetAllLabels(labelId, UserId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateLabel(long labelId, long noteId, long userId, Label_ML label_ML)
        {
            try
            {
                return this.ilabel_RL.UpdateLabel(noteId, userId, labelId, label_ML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long labelId, long noteId, long userId)
        {
            try
            {
                return this.ilabel_RL.DeleteLabel(labelId, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


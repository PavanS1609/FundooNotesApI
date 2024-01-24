using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface ILabel_BL
    {
        public LabelEntity AddLabel(Label_ML label_ML, long noteId, long userId);
        public List<LabelEntity> GetAllLabels(long labelId, long UserId, long noteId);
        public bool UpdateLabel(long labelId, long noteId, long userId, Label_ML label_ML);
        public bool DeleteLabel(long labelId, long noteId, long userId);
    }
}

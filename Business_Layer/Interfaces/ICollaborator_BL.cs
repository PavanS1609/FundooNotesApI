using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface ICollaborator_BL
    {
        public CollaboratorEntity AddCollab(Collaborator_ML collaborator_ML, long noteId, long userId);
      
        public bool DeleteCollab(long collabId, long noteId, long userId);
    }
}

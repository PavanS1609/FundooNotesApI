using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface ICollaborator_RL
    {
        public CollaboratorEntity AddCollab(Collaborator_ML collaborator_ML, long noteId, long userId);
        public bool DeleteCollab(long collabId, long noteId, long userId);
    }
}

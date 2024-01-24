using Business_Layer.Interfaces;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class Collaborator_BL : ICollaborator_BL
    {
        private readonly ICollaborator_RL icollaborator_RL;
        public Collaborator_BL(ICollaborator_RL icollaborator_RL)
        {
            this.icollaborator_RL = icollaborator_RL;
        }

        public CollaboratorEntity AddCollab(Collaborator_ML collaborator_ML, long noteId, long userId)
        {
            try
            {
                return icollaborator_RL.AddCollab(collaborator_ML, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCollab(long collabId, long noteId, long userId)
        {
            try
            {
                return icollaborator_RL.DeleteCollab(collabId, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

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
    public class Collaborator_RL : ICollaborator_RL
    {
        private readonly FundooDB_Context fundoodbContext;
        private readonly IConfiguration configuration;
        public Collaborator_RL(FundooDB_Context fundoodbContext, IConfiguration configuration)
        {
            this.fundoodbContext = fundoodbContext;
            this.configuration = configuration;
        }

        public CollaboratorEntity AddCollab(Collaborator_ML collaborator_ML, long noteId, long userId)
        {
            try
            {
                CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                collaboratorEntity.CollaboratorEmail = collaborator_ML.CollaboratorEmail;
                UserEntity user = fundoodbContext.Users.FirstOrDefault(x => x.UserId == userId);
                NoteEntity note = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                collaboratorEntity.Users = user;
                collaboratorEntity.Notes = note;
                fundoodbContext.Collaborators.Add(collaboratorEntity);
                int result = fundoodbContext.SaveChanges();
                if (result > 0)
                {
                    return collaboratorEntity;
                }
                else
                {
                    return null;
                }
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
                var result = fundoodbContext.Collaborators.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.CollaboratorId == collabId);
                if (result != null)
                {
                    fundoodbContext.Collaborators.Remove(result);
                    fundoodbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

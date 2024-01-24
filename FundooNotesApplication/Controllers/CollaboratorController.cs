using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Business_Layer.Interfaces;
using Model_Layer.Models;
using Repository_Layer.Entity;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaborator_BL collaborator_BL;
        public CollaboratorController(ICollaborator_BL collaborator_BL)
        {
            this.collaborator_BL = collaborator_BL;
        }
        [Authorize]
        [HttpPost("Add-collab")]
        public IActionResult AddCollab(Collaborator_ML collaborator_ML, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = collaborator_BL.AddCollab(collaborator_ML, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<CollaboratorEntity> { Status = true, Message = "added collab successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add collab" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
       
        [Authorize]
        [HttpDelete("delete-collab")]
        public IActionResult deletecollab(long collabId, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = collaborator_BL.DeleteCollab(collabId, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<bool> { Status = true, Message = "deleted collab successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to delete collab" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}



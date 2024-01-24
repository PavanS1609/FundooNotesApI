using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using Business_Layer.Interfaces;
using Model_Layer.Models;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabel_BL ilabel_BL;
        public LabelController(ILabel_BL ilabel_BL)
        {
            this.ilabel_BL = ilabel_BL;
        }
        [Authorize]
        [HttpPost("Add-Label")]
        public IActionResult AddLabel(Label_ML label_ML, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = ilabel_BL.AddLabel(label_ML, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<LabelEntity> { Status = true, Message = "added label successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add label" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Get-All-Labels")]
        public IActionResult GetAllLabels(long labelId, long UserId, long noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<LabelEntity> result = ilabel_BL.GetAllLabels(labelId, userId, noteId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<List<LabelEntity>> { Status = true, Message = "retrieved all labels", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get labels" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("Update-label")]
        public IActionResult updateLabel(long labelId, long noteId, long userId, Label_ML label_ML)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = ilabel_BL.UpdateLabel(labelId, noteId, userId, label_ML);
                if (result != null)
                {
                    return this.Ok(new Response_ML<bool> { Status = true, Message = "updated label successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update Note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("delete-label")]
        public IActionResult deleteLabel(long labelId, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = ilabel_BL.DeleteLabel(labelId, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<bool> { Status = true, Message = "deleted note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to delete Note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }
    }
}

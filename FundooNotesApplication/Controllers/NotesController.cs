using Business_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository_Layer.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Model_Layer.Models;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotes_BL inotes_BL;
       private readonly IDistributedCache distributedCache;

        public NotesController(INotes_BL inotes_BL, IDistributedCache distributedCache)
        {
            this.inotes_BL = inotes_BL;
            this.distributedCache = distributedCache;

        }
        [Authorize]
        [HttpPost("Add-Note")]
        public IActionResult AddNote(Notes_ML notes_ML)
        {
            try
            {
                //HttpContext.Session.GetInt32("UserID");
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotes_BL.AddNote(notes_ML,userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "added note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add Note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Get-All-Notes")]
        public IActionResult GetAllNotes(long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<NoteEntity> result = inotes_BL.GetAllNotes((int)userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<List<NoteEntity>> { Status = true, Message = "retrieved all notes",Data =result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get Notes" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("Update-note")]
        public IActionResult updateNotes(long noteId, Notes_ML notes_ML)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                NoteEntity result = inotes_BL.UpdateNote(noteId,userId, notes_ML);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity  > { Status = true, Message = "updated note successfully", Data=result });
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
        [HttpDelete("delete-note")]
        public IActionResult deleteNote(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = inotes_BL.DeleteNote(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "deleted note successfully" });
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
        [Authorize]
        [HttpPut("Is-pin-or-not")]
        public IActionResult IsPinOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = inotes_BL.IsPin(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "pinned" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not pinned" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Is-Archieve-or-not")]
        public IActionResult IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = inotes_BL.IsArchieve(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "archieved" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not archieve" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Is-trash-or-not")]
        public IActionResult IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = inotes_BL.IsTrash(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "is in trash" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not in trash" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("color")]
        public IActionResult color(long noteId, string color)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotes_BL.color(noteId, color);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "updated color successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update color" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Reminder")]
        public IActionResult Reminder(long noteId, DateTime remind)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotes_BL.Remainder(noteId, remind);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "updated reminder successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update reminder" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotes_BL.UploadImage(noteId, userId, img);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "updated image successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update image" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Fetch-note")]
        public IActionResult GetNoteById(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                NoteEntity result = inotes_BL.GetNoteById(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "retrieved all notes", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get Notes" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("Get-all-notes-using-reddis")]
        public async Task<IActionResult> GetAllNotesUsingRedis(int userId)
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedisNoteList = await distributedCache.GetAsync(CacheKey);
                if (RedisNoteList != null)
                {
                    var SerializedNoteList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerializedNoteList);

                }
                else
                {
                    NoteList = (List<NoteEntity>)inotes_BL.GetAllNotes(userId);
                    var SeralizedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SeralizedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(CacheKey, redisNoteList, options);

                }

                return Ok(NoteList);

            }
            catch (Exception ex)
            {
                return BadRequest(new Response_ML<NoteEntity> { Status = false, Message = ex.Message });
            }

        }

        [HttpGet("Notes_By_Date")]
        public IActionResult SearchNotesByDate(DateTime CreatedAt)
        {
            try
            {
                var result = inotes_BL.SearchNotesByDate(CreatedAt);
                if (result != null)
                {

                    return this.Ok(new Response_ML<NoteEntity> { Status = true, Message = "retrieved the note", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "No Such Note Exist" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Notes_SameTitle")]
        public IActionResult SearchNoteByTitle(string title)
        {
            try
            {
                List<NoteEntity> result = inotes_BL.SearchNoteByTitle(title);
                if (result != null)
                {

                    return this.Ok(new Response_ML<List<NoteEntity>> { Status = true, Message = "retrieved the Title" ,Data =result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "No Such Title Exist" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}

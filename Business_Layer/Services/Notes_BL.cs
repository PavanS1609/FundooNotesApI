using Business_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class Notes_BL : INotes_BL
    {
        private readonly INotes_RL inotes_RL;

        public Notes_BL(INotes_RL inotes_RL )
        {
            this.inotes_RL = inotes_RL;
        }

        public NoteEntity AddNote(Notes_ML notes_ML, int userId)
        {
            try
            {
                return this.inotes_RL.AddNote(notes_ML, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNotes(int UserId)
        {
            try
            {
                return inotes_RL.GetAllNotes(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNote(long noteId, long userId, Notes_ML notes_ML)
        {
            try
            {
                return inotes_RL.UpdateNote(noteId, userId, notes_ML);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return inotes_RL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsPin(long noteId, long userId)
        {
            try
            {
                return inotes_RL.IsPin(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsArchieve(long noteId, long userId)
        {
            try
            {
                return inotes_RL.IsArchieve(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsTrash(long noteId, long userId)
        {
            try
            {
                return inotes_RL.IsTrash(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity color(long noteId, string color)
        {
            try
            {
                return inotes_RL.color(noteId, color);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity Remainder(long noteId, DateTime remind)
        {
            try
            {
                return inotes_RL.Remainder(noteId, remind);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                return inotes_RL.UploadImage(noteId, userId, img);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity GetNoteById(long noteId, long userId)
        {
            try
            {
                return inotes_RL.GetNoteById(noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity SearchNotesByDate(DateTime CreatedAt)
        {
            try
            {
                return inotes_RL.SearchNotesByDate(CreatedAt);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<NoteEntity> SearchNoteByTitle(string title)
        {
            try
            {
                return inotes_RL.SearchNoteByTitle(title);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}


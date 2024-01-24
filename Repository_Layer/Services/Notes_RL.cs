using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Repository_Layer.Context;
using System.Linq;
using Model_Layer.Models;
using Microsoft.VisualBasic;

namespace Repository_Layer.Services
{
    public class Notes_RL : INotes_RL
    {
        private readonly FundooDB_Context fundoodbContext;
        private readonly IConfiguration configuration;
        public Notes_RL(FundooDB_Context fundoodbContext, IConfiguration configuration)
        {
            this.fundoodbContext = fundoodbContext;
            this.configuration = configuration;
        }

        public NoteEntity AddNote(Notes_ML notes_ML,int UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = notes_ML.Title;
                noteEntity.Note = notes_ML.Note;
                noteEntity.Remainder = notes_ML.Remainder;
                noteEntity.color = notes_ML.color;
                noteEntity.Image = notes_ML.Image;
                noteEntity.IsArchive = notes_ML.IsArchive;
                noteEntity.IsPin = notes_ML.IsPin;
                noteEntity.Istrash = notes_ML.Istrash;
                noteEntity.CreatedAt = notes_ML.CreatedAt;
                noteEntity.UpdatedAt = notes_ML.UpdatedAt;
                noteEntity.UserId= UserId;
                fundoodbContext.Notes.Add(noteEntity);
                int result = fundoodbContext.SaveChanges();
                if (result > 0)
                {

                    return noteEntity;
                }
                else
                {
                    return null;
                }
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
                List<NoteEntity> result = fundoodbContext.Notes.ToList();
                return result;
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
                NoteEntity result = fundoodbContext.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (result != null)
                {
                    result.Title = notes_ML.Title;
                    result.Note = notes_ML.Note;
                    result.Remainder = notes_ML.Remainder;
                    result.color = notes_ML.color;
                    result.Image = notes_ML.Image;
                    result.IsArchive = notes_ML.IsArchive;
                    result.IsPin = notes_ML.IsPin;
                    result.Istrash = notes_ML.Istrash;
                    fundoodbContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
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
                var result = fundoodbContext.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if (result.Istrash == true)
                {
                    fundoodbContext.Notes.Remove(result);
                    fundoodbContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Istrash = true;
                    this.fundoodbContext.SaveChanges();
                    return true;
                }
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
                NoteEntity result = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.IsPin == true)
                    {
                        result.IsPin = false;
                        this.fundoodbContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.IsPin = true;
                        this.fundoodbContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
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
                NoteEntity result = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.IsArchive == true)
                    {
                        result.IsArchive = false;
                        this.fundoodbContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.IsArchive = true;
                        this.fundoodbContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
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
                NoteEntity result = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.Istrash == true)
                    {
                        result.Istrash = false;
                        this.fundoodbContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.Istrash = true;
                        this.fundoodbContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
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
                NoteEntity note = this.fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (note.color != null || note.color==null)
                {
                    note.color = color;
                    this.fundoodbContext.SaveChanges();
                    return note;
                }
                return null;
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
                NoteEntity note = this.fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (note.Remainder != null)
                {
                    note.Remainder = remind;
                    this.fundoodbContext.SaveChanges();
                    return note;
                }
                return null;
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
                var result = this.fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    Account account = new Account(
                        this.configuration["CloudinarySettings:Cloud_Name"],
                        this.configuration["CloudinarySettings:Api_key"],
                        this.configuration["CloudinarySettings:Api_secret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadReult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadReult.Url.ToString();
                    result.Image = imagePath;
                    fundoodbContext.SaveChanges();
                    return "Image uploaded succesfully";
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

        public NoteEntity GetNoteById(long noteId, long userId)
        {
            try
            {
                NoteEntity noteEntity = fundoodbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                return noteEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

         //        search notes for a particular date.
         //if it exist, print information of notes, else throw error, no such note exist

        public NoteEntity SearchNotesByDate(DateTime CreatedAt)
        {
            try
            {
                var result = fundoodbContext.Notes.FirstOrDefault(x => x.CreatedAt == CreatedAt);
                if(result != null)
                {
                    return result;
                }
                return null;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       // write another method for searching note by title of note
       // and show information of the notes
       public List<NoteEntity> SearchNoteByTitle(string title)
        {
            try
            {
               List<NoteEntity> noteEntity = fundoodbContext.Notes.Where(p=> p.Title == title).ToList(); 
                if(noteEntity != null)
                {
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

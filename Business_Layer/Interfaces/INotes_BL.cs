using Microsoft.AspNetCore.Http;
using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface INotes_BL
    {
        public NoteEntity AddNote(Notes_ML notes_ML, int userId);
        public List<NoteEntity> GetAllNotes(int UserId);
        public NoteEntity UpdateNote(long noteId, long userId, Notes_ML notes_ML);
        public bool DeleteNote(long noteId, long userId);
        public bool IsPin(long noteId, long userId);
        public bool IsArchieve(long noteId, long userId);
        public bool IsTrash(long noteId, long userId);
        public NoteEntity color(long noteId, string color);

        public NoteEntity Remainder(long noteId, DateTime remind);
        public string UploadImage(long noteId, long userId, IFormFile img);
        public NoteEntity GetNoteById(long noteId, long userId);

        public NoteEntity SearchNotesByDate(DateTime CreatedAt);

        public List<NoteEntity> SearchNoteByTitle(string title);
    }
}

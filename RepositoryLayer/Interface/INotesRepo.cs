using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRepo
    {
        public NotesEntity CreateNotes(NotesRegModel model, long UserId);
        public List<NotesEntity> GetAllNotes(long userId);
        public List<NotesEntity> GetNotesByID(int NotesID,long userId);
        public string UpDateNotes(long NotesID, string takeaNote,long userId);
        public long DeleteNotes(long NotesID,long userId);
    }
}

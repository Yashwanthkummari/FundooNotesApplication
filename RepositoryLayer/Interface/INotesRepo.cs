using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INotesRepo
    {
        public NotesEntity CreateNotes(NotesRegModel model, long UserId);
        public List<NotesEntity> GetAllNotes(long userId);
        public List<NotesEntity> GetNotesByID(int NotesID,long userId);
        public string UpDateNotes(long NotesID, string takeaNote,long userId);
        public long DeleteNotes(long NotesID,long userId);
        public string UpdateColour(long NotesID, long UserId, string colour);
        public Task<Tuple<int, string>> UploadImage(long NotesId, long UserId, IFormFile imageFile);
        public bool ArchiveNotes(long NotesID, long UserId);
        public bool PinNotes(long NotesID, long UserId);
        public bool TrashNotes(long NotesID, long UserId);
    }
}

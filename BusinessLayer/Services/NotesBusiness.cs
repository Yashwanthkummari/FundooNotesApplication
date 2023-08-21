using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private readonly INotesRepo _repo;

        public NotesBusiness(INotesRepo notesRepo)
        {
            _repo = notesRepo;
        }

        public NotesEntity CreateNotes(NotesRegModel model, long UserId)
        {
            try
            {
                return _repo.CreateNotes(model, UserId);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<NotesEntity> GetAllNotes(long userId)
        {
            try
            {
                return _repo.GetAllNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotesEntity> GetNotesByID(int NotesID, long userId)
        {
            try
            {
                return _repo.GetNotesByID(NotesID, userId);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpDateNotes(long NotesID, string takeaNote, long userId)
        {
            try
            {
                return _repo.UpDateNotes(NotesID, takeaNote, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public long DeleteNotes(long NotesID, long userId)
        {
            try
            {
                return _repo.DeleteNotes(NotesID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tuple<int, string>> UploadImage(long NotesId, long UserId, IFormFile imageFile)
        {
            try
            {
                return await _repo.UploadImage(NotesId, UserId, imageFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdateColour(long NotesID, long UserId, string colour)
        {
            try
            {
                return _repo.UpdateColour(NotesID, UserId, colour);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool ArchiveNotes(long NotesID, long UserId)
        {
            try
            {
                return _repo.ArchiveNotes(NotesID, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinNotes(long NotesID, long UserId)
        {
            try
            {
                return _repo.PinNotes(NotesID, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool TrashNotes(long NotesID, long UserId)
        {
            try
            {
                return _repo.TrashNotes(NotesID, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

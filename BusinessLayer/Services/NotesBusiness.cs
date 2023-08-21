using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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

        public List<NotesEntity> GetNotesByID(int NotesID,long userId)
        {
            try
            {
                return _repo.GetNotesByID(NotesID,userId);

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
                return _repo.UpDateNotes(NotesID, takeaNote,userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public long DeleteNotes(long NotesID,long userId)
        {
            try
            {
                return _repo.DeleteNotes(NotesID,userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

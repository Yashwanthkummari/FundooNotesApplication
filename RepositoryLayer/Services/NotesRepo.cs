using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext _fundooContext;
        private readonly IConfiguration _configuration;
        public NotesRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this._fundooContext = fundooContext;
            this._configuration = configuration;
        }
        public NotesEntity CreateNotes(NotesRegModel model, long UserId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = model.Title;
                notesEntity.TakeNote = model.TakeaNote;
                notesEntity.UserId = UserId;

                _fundooContext.Notes.Add(notesEntity);

                _fundooContext.SaveChanges();

                if (notesEntity != null)
                {
                    return notesEntity;
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
        public List<NotesEntity> GetAllNotes(long userId)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(n => n.UserId == userId);

                if (result != null)
                {
                    List<NotesEntity> notesEntities = new List<NotesEntity>();
                    notesEntities = _fundooContext.Notes.Where(n => n.UserId == userId).ToList();
                    return notesEntities;
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
        public List<NotesEntity> GetNotesByID(int NotesID,long userId)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(n => n.NoteId == NotesID && n.UserId==userId);
                if (result != null)
                {
                    List<NotesEntity> notesList = new List<NotesEntity>();
                    notesList = _fundooContext.Notes.Where(n => n.NoteId == NotesID).ToList();
                    return notesList;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpDateNotes(long NotesID, string takeaNote,long userId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(n => n.NoteId == NotesID && n.UserId == userId );
            if (result != null)
            {
                result.TakeNote = result.TakeNote + "" + takeaNote;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();
                return result.TakeNote;

            }
            else
            {
                return null;
            }

        }
        public long DeleteNotes(long NotesID,long userId)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(n => n.NoteId == NotesID && n.UserId == userId);
                if (result != null)
                {
                    _fundooContext.Notes.Remove(result);
                    _fundooContext.SaveChanges();
                    return result.NoteId;

                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdateColour(long NotesID, long UserId, string colour)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NotesID && x.UserId == UserId);

            if (result != null)
            {
                result.Colour = colour;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return result.Colour;
            }
            else
            {
                return null;
            }
        }
        public bool ArchiveNotes(long NotesID, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NotesID && x.UserId == UserId);

            if (result != null)
            {
                result.IsArchive = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return true;
            }

            else
            {
                return false;
            }
        }
        public bool PinNotes(long NotesID, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NotesID && x.UserId == UserId);

            if (result != null)
            {
                result.IsPin = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return true;
            }


            else
            {
                return false;
            }
        }
        public bool TrashNotes(long NotesID, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NotesID && x.UserId == UserId);

            if (result != null)
            {
                result.IsTrash = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }
        }

    }
}

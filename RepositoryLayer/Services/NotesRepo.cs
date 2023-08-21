using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext _fundooContext;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly FileService _fileService;

        public NotesRepo(FundooContext fundooContext, IConfiguration configuration, Cloudinary cloudinary, FileService fileService)
        {
            this._fundooContext = fundooContext;
            this._configuration = configuration;
            this._cloudinary = cloudinary;
            this._fileService = fileService;
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
        public async Task<Tuple<int, string>> UploadImage(long NotesId, long UserId, IFormFile imageFile)
        {
            try
            {
                var note = _fundooContext.Notes.FirstOrDefault(data => data.NoteId == NotesId && data.UserId == UserId);
                if (note != null)
                {
                    var fileServiceresult = await _fileService.SaveImage(imageFile);
                    if (fileServiceresult == null)
                    {
                        return new Tuple<int, string>(0, fileServiceresult.Item2);
                    }

                    var uploading = new ImageUploadParams
                    {
                        File = new CloudinaryDotNet.FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    };
                    ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploading);

                    string ImageUrl = uploadResult.SecureUrl.AbsoluteUri;

                    note.Image = ImageUrl;
                    _fundooContext.Notes.Update(note);
                    _fundooContext.SaveChanges();
                    return new Tuple<int, string>(1, "product added with image sucessfully");
                }
                return null;

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "An error occured :" + ex.Message);
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

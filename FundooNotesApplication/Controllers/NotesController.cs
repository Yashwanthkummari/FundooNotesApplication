using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using CloudinaryDotNet;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    { 
        private readonly INotesBusiness _notesBusiness;
        private readonly Cloudinary _cloudinary;
       
        public NotesController(INotesBusiness notesBusiness,Cloudinary cloudinary)
        {
            this._notesBusiness = notesBusiness;
            this._cloudinary = cloudinary;
        }
        [Authorize]
        [HttpPost]
        [Route("Note")]
        public IActionResult CreateNotes(NotesRegModel model)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.CreateNotes(model, userId);

            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "Notes Added  sucessfully", Data = result });

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "User Registration Unsucessfull", Data = result });

            }
        }
        [Authorize]
        [HttpGet]
        [Route("AllNotes")]

        public IActionResult GetAllNotes()
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.GetAllNotes(userId);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = " All Notes retrived Sucessfully", Result = result });
            }
            else
            {
                return this.NotFound(new { Success = false });

            }
        }

        [Authorize]
        [HttpGet]
        [Route("NotedByID")]

        public IActionResult GetAllNotesByID(int NotesID)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.GetNotesByID(NotesID,userId);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = " All Notes retrived Sucessfully", Result = result });
            }
            else
            {
                return this.NotFound();
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("UpdateNotes")]
        public IActionResult UpdateNotes(long NotesID, string takeaNote)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.UpDateNotes(NotesID, takeaNote, userId);
            if (result != null)
            {
                return this.Ok(new { sucess = true, Message = "Notes Updated sucessfully", Result = result });
            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Notes Updated UNsucessful", Result = result });
            }

        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteNotes")]

        public IActionResult DeleteNotes(long NotesID)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.DeleteNotes(NotesID, userId);
            if (result != 0)
            {
                return this.Ok(new { sucess = true, Message = "Notes Deleted  sucessfully", Result = result });

            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Notes not Deleted ", Result = result });

            }
        }
        [Authorize]
        [HttpPatch]
        [Route("AddImage")]

        public async Task<IActionResult> UploadImage(long Noteid, IFormFile image)
        {
            var userclaim = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            int userId = int.Parse(userclaim);
            var result = await _notesBusiness.UploadImage(Noteid, userId, image);
            if (result.Item1 == 1)
            {
                return this.Ok(new { sucess = true, message = "Image uploaded Sucessfully", data = result });
            }
            else
            {
                return this.Ok(new { sucess = false, message = "Image uploaded UnSucessfull", data = result });

            }

        }
        [Authorize]
        [HttpPatch]
        [Route("UpdateColour")]

        public IActionResult UpdateColour(long NotesID, string colour)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.UpdateColour(NotesID, userId, colour);
            if (result != null)
            {
                return this.Ok(new { sucess = true, Message = "Colour Updated Sucessfully", Result = result });

            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Colour not updated ", Result = result });

            }
        }

        [Authorize]
        [HttpPatch]
        [Route("Archeive Notes")]

        public IActionResult ArcheiveNotes(long NotesID)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.ArchiveNotes(NotesID, userId);
            if (result == true)
            {
                return this.Ok(new { sucess = true, Message = "Notes Archieved Sucessfully", Result = result });

            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Notes not found ", Result = result });

            }
        }

        [Authorize]
        [HttpPatch]
        [Route("PinNotes")]

        public IActionResult PinNotes(long NotesID)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.PinNotes(NotesID, userId);
            if (result == true)
            {
                return this.Ok(new { sucess = true, Message = "Notes pined Sucessfully", Result = result });

            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Notes not found ", Result = result });

            }
        }
        [Authorize]
        [HttpPatch]
        [Route("TrashNotes")]

        public IActionResult TrashNotes(long NotesID)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _notesBusiness.TrashNotes(NotesID, userId);
            if (result ==true )
            {
                return this.Ok(new { sucess = true, Message = "Notes went to Trash Sucessfully", Result = result });

            }
            else
            {
                return this.NotFound(new { sucess = false, Message = "Notes not found ", Result = result });

            }
        }
      
    }
}


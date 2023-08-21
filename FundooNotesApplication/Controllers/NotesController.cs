using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    { 
        private readonly INotesBusiness _notesBusiness;
        public NotesController(INotesBusiness notesBusiness)
        {
            this._notesBusiness = notesBusiness;
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

    }
}

using Businesslayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        
            INotesBL notesBL;

            public NotesController(INotesBL iNotesBL)
            {
                this.notesBL = iNotesBL;
            }

            [Authorize]
            [HttpPost("Add")]
            public IActionResult AddNotes(notesModel note)
            {
                try
                {
                    long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                    var result = notesBL.AddNotes(note, UserID);
                    if (result != null)
                    {
                        return this.Ok(new
                        {
                            success = true,
                            message = "Notes Added Successfully",
                            data = result
                        });

                    }
                    else
                    {
                        return this.BadRequest(new
                        {
                            success = false,
                            message = "Failed to add notes",

                        });
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }

            }

        [Authorize]
        [HttpPost("Read")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.ReadNotes(userId);
                if (result != null)
                {
                    return Ok(new 
                    {
                        success = true, 
                        message = "Notes Received: ", 
                        data = result 
                    });
                }
                return BadRequest(new 
                {
                    success = false, 
                    message = "Error in receiving notes!" 
                });
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost("Update")]
        public IActionResult UpdateNotes(notesModel note, long NoteID)
        {
            try
            {

                var result = notesBL.UpdateNotes(note, NoteID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Notes Updated Successfully",
                        data = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Notes Update Unsuccessfull",

                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [Authorize]
        [HttpPost("Delete")]
        public IActionResult DeleteNotes(long NoteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.DeleteNotes(userId,NoteID);
                if (result != false)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Notes Deleted Successfully",

                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to delete Note.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Archive(NoteID, userID);
                if (result == true)
                {
                    return Ok(new 
                    {
                        success = true,
                        message = "Note Archived successfully" 
                    });
                }
                else if (result == false)
                {
                    return Ok(new 
                    {
                        success = true, 
                        message = "Note UnArchived successfully." 
                    });
                }
                return BadRequest(new 
                {
                    success = false, 
                    message = "Task unsuccessful" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

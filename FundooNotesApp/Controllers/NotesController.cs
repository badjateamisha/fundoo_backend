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
            [HttpPost("Addnotes")]
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
        [HttpPost("ReadNotes")]
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

        }
    }

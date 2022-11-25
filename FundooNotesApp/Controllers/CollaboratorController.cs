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
    public class CollaboratorController : ControllerBase
    {
        ICollabBL collabBL;

        public CollaboratorController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollaborator(collaboratorModel collab,long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = collabBL.AddCollaborator(collab,UserID,NoteId);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Added Successfully",
                        data = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Failed to add collaborator",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpGet("Read")]
        public IActionResult ReadCollaborator()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = collabBL.ReadCollaborator(userId);
                /*if (result != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Collaborator for this note are: ",
                        data = result
                    });
                }
                else
                return BadRequest(new
                {
                    success = false,
                    message = "Error in reading collaborators!"
                });*/

                if(result != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Collaborator are:",
                        data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error"
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
        public IActionResult DeleteCollaborator(long NoteID,long CollaboratorId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = collabBL.DeleteCollaborator(userId, NoteID, CollaboratorId);
                if (result != false)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collaborator Deleted Successfully",

                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to delete collaborator.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}

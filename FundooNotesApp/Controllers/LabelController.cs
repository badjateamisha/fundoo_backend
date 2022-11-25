using Businesslayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult AddLabel(string Name, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.AddLabel(Name, NoteID,userID);
                if (result == true)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Label Created succesfully." 
                    });
                }
                else
                {
                    return BadRequest(new 
                    {
                        success = false, 
                        message = "Create Label Unsuccessful!"
                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("Read")]
        public IActionResult ReadLabel()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.ReadLabel(userId);
                if (result != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Label Received: ",
                        data = result
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    message = "Error in receiving labels!"
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

using Businesslayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}

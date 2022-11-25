using Businesslayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        
            INotesBL notesBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;

        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
            {
                this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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

        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var output = notesBL.Archive(userID, NoteID);
                if (output == true)
                {
                    return Ok(new 
                    {
                        success = true,
                        message = "Note Archived successfully" 
                    });
                }
                else if (output == false)
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
                    message = "Task unsuccessful"
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult Pin(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Pin(userID, NoteID);
                if (result == true)
                {
                    return Ok(new 
                    {
                        success = true, 
                        message = "Note Pinned Successfully" 
                    });
                }
                else if (result == false)
                {
                    return Ok(new 
                    {
                        success = true, 
                        message = "Note Unpinned successfully." 
                    });
                }
                return BadRequest(new 
                {
                    success = false, 
                    message = "Task unsuccessful." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Trash(userID, NoteID);
                if (result == true)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Note Trashed Successfully"
                    });
                }
                else if (result == false)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Note Untrashed successfully."
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    message = "Task unsuccessful."
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Color")]
        public IActionResult Color(long NoteID,string color)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Color(NoteID, color);
                if(result != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Note Colored Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Note Color change unsuccessful",
                    }); 
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile image, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Image(image, NoteID, userID);
                if (result != null)
                {
                    return Ok(new 
                    {
                        success = true, 
                        message = result 
                    });
                }
                else
                {
                    return BadRequest(new 
                    {
                        success = false, 
                        message = "Unable to upload image." 
                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}

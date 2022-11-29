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
    [Authorize]
    public class LabelController : ControllerBase
    {
        NLog log = new NLog();

        private readonly ILabelBL labelBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache; 
            this.distributedCache = distributedCache;   
            this.fundooContext = fundooContext;
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
                    log.LogInfo("Label Created succesfully.");
                    return Ok(new
                    {
                        success = true,
                        message = "Label Created succesfully." 
                    });
                }
                else
                {
                    log.LogInfo("Create Label Unsuccessful!");
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

        [HttpGet("Read")]
        public IActionResult ReadLabel()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.ReadLabel(userId);
                if (result != null)
                {
                    log.LogInfo("Label Received: ");

                    return Ok(new
                    {
                        success = true,
                        message = "Label Received: ",
                        data = result
                    });
                }

                log.LogInfo("Error in receiving labels!");
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

        [HttpPost("Update")]
        public IActionResult UpdateLabel(string name, long NoteID)
        {
            try
            {

                var result = labelBL.UpdateLabel(name, NoteID);
                if (result != null)
                {
                    log.LogInfo("Label Updated Successfully");

                    return this.Ok(new
                    {
                        success = true,
                        message = "Label Updated Successfully",
                        data = result
                    });

                }
                else
                {
                    log.LogInfo("Label Update Unsuccessfull");

                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Label Update Unsuccessfull",

                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("Delete")]
        public IActionResult DeleteLabel(long LabelID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.DeleteLabel(userId, LabelID);
                if (result != false)
                {
                    log.LogInfo("Label Deleted Successfully");

                    return this.Ok(new
                    {
                        success = true,
                        message = "Label Deleted Successfully",

                    });

                }
                else
                {
                    log.LogInfo("Unable to delete Label.");

                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to delete Label.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}

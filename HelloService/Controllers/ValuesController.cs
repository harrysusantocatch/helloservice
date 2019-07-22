using System;
using Catcher.DB.DAO;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Implement;
using HelloService.Entities.DB;
using Microsoft.AspNetCore.Mvc;

namespace HelloService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private StatusAppLogic statusAppLogic;
        public ValuesController()
        {
            statusAppLogic = new StatusAppLogic(new StatusAppDao());
        }

        // GET api/values
        [HttpGet("/")]
        public IActionResult Get()
        {
            try
            {
                return Ok($"Application is running & Database {statusAppLogic.GetStatus()}");
            }
            catch(Exception e)
            {
                return Ok($"Application is running & Database disconnected {System.Environment.NewLine}ERROR : {e.Message}");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

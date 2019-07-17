using Catcher.DB.DAO;
using HelloService.Entities.DB;
using Microsoft.AspNetCore.Mvc;

namespace HelloService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public static readonly IDao<StatusApp> statusAppDao = DaoContext.GetDao<StatusApp>();

        // GET api/values
        [HttpGet("/")]
        public IActionResult Get()
        {
            string status = "DEAD";
            var result = statusAppDao.GetAll();
            if (result.Count == 0) status = statusAppDao.InsertAndGet(new StatusApp() { Status = "RUNNING" }).Status;
            else status = result[0].Status;
            return Ok(status);
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

using Core.BusinessModels;
using Infrastructure.Data;
using LearnScapeAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnScapeAPI.Controllers
{
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        
        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretTest()
        {
            return "secret text";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            ProductBM thing = _context.Products.Find(42);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            ProductBM thing = _context.Products.Find(42);
            string thingsToReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();

        }

    }
}

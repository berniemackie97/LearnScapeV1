using Core.BusinessModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnScapeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserBM>>> GetUserList()
        {
            var products = await _userRepo.GetUserList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserBM>> GetUserById(int id)
        {
            var product = await _userRepo.GetUserById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}

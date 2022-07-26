using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesseesController : Controller
    {
        private readonly ILesseeRepository _lesseeRepository;

        public LesseesController(ILesseeRepository lesseeRepository)
        {
            _lesseeRepository = lesseeRepository;
        }

        [HttpGet]
        public IActionResult GetLessees()
        {
            return Ok(_lesseeRepository.GetAllWithUsers());
        }
    }
}

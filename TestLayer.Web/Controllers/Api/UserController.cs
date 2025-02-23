using Microsoft.AspNetCore.Mvc;

namespace TestLayer.Web.Controllers.Api
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add()
        {
            return Ok();
        }
    }
}

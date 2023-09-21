using Microsoft.AspNetCore.Mvc;

namespace ShareFileCloud.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return Ok(new { write = true });
        }
    }
}

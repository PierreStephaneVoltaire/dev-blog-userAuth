using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PasswordController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
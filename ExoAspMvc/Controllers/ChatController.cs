using Microsoft.AspNetCore.Mvc;

namespace ExoAspMvc.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}

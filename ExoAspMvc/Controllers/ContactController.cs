using Microsoft.AspNetCore.Mvc;

namespace ExoAspMvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
    }
}

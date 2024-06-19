using ExoAspMvc.Models.User;
using ExoAspMvc.Repository.AuthRepo;
using ExoAspMvc.SessionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ExoAspMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _services;

        public AuthController(IAuthServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            User u = _services.GetById(id);
            return View(u);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateUser user)
        {
                _services.Register(user);
                return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email,string Password)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(user);
            //}

            User u = _services.Login(Email,Password);
            //User? u = _services.GetById(5);

            // Enregistrer le membre dans la session (-> Evolution : SessionManager)
            //HttpContext.Session.Start(u.Id, u.Pseudo, u.Role);

            HttpContext.Session.SetInt32("Id", u.Id);
            HttpContext.Session.SetString("Pseudo",u.Pseudo);
            HttpContext.Session.SetString("Role",u.Role.ToString());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Efface la session (-> Evolution : SessionManager)
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("Id",0);

            return RedirectToAction("Index", "Home");
        }
    }
}

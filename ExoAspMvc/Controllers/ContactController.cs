using ExoAspMvc.Models;
using ExoAspMvc.Models.Contact;
using ExoAspMvc.Repository.ContactRepo;
using Microsoft.AspNetCore.Mvc;

namespace ExoAspMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactServices _service;

        public ContactController(IContactServices service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<Contact> contacts = _service.GetContacts();
            return View(contacts);
        }

        public IActionResult Delete(int Id)
        {
            Contact contact = _service.GetById(Id);
            if (contact == null)
            {
                return NotFound();
            }
            _service.Delete(Id);
            return RedirectToAction("Index");
        }


        public IActionResult Details(int Id)
        {
            Contact contact = _service.GetById(Id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Details(Contact contact , string action)
        {
            if (action == "Delete")
            {
                _service.Delete(contact.Id);
                return RedirectToAction("Index");
            }
            else if (action == "Save")
            {
                _service.Update(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }


        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(CreateContact contact)
        {
            if (ModelState.IsValid)
            {
                _service.AddContact(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

    }
}

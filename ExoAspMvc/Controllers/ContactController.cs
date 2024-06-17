using ExoAspMvc.Models;
using ExoAspMvc.Repository.ContactRepo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExoAspMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service) => _service = service;

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


        public IActionResult Details(Contact c)
        {
            Contact contact = _service.GetById(c.Id);
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

            return View("Index");
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

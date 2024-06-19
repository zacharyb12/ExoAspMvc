using ExoAspMvc.Models;
using ExoAspMvc.Models.Contact;

namespace ExoAspMvc.Repository.ContactRepo
{
    public interface IContactServices
    {
        List<Contact> GetContacts();

        void Delete(int Id);

        void AddContact(CreateContact contact);

        void Update(Contact contact);

        Contact GetById(int id);
    }
}

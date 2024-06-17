using ExoAspMvc.Models;

namespace ExoAspMvc.Repository.ContactRepo
{
    public interface IContactService
    {
        List<Contact> GetContacts();

        void Delete(int Id);

        void AddContact(CreateContact contact);

        void Update(Contact contact);

        Contact GetById(int id);
    }
}

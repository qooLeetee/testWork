using System.Data.Entity;
using testWork.Controllers.DTO;
using testWork.Controllers.DTO.Requests;
using testWork.Controllers.DTO.Responses;
using testWork.Models;

namespace testWork.Services
{
    public class ContactService
    {
        public List<ContactResponse> getContactList()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var contactResponses = new List<ContactResponse>();
                var Contacts = db.Contacts.ToList();
                foreach (var contact in Contacts)
                {
                    ContactResponse contactResponse = new ContactResponse();
                    contactResponse.Id = (int)contact.Id;
                    contactResponse.Name = contact.Name;
                    contactResponse.Email = contact.Email;
                    contactResponse.Phone = contact.Phone;
                    contactResponses.Add(contactResponse);
                }
                return contactResponses;
            }
        }

        public Contact createContact(ContactRequest contactRequest)
        {
            Contact contact = new Contact();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                contact.Name = contactRequest.Name;
                contact.Email = contactRequest.Email;
                contact.Phone = contactRequest.Phone;

                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            return contact;
        }
    }
}

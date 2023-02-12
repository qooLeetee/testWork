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
                    contactResponse.id = (int)contact.id;
                    contactResponse.name = contact.name;
                    contactResponse.email = contact.email;
                    contactResponse.phone = contact.phone;
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
                contact.name = contactRequest.name;
                contact.email = contactRequest.email;
                contact.phone = contactRequest.phone;

                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            return contact;
        }
    }
}

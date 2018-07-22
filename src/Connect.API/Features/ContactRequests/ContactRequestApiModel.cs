using Connect.Core.Models;

namespace Connect.API.Features.ContactRequests
{
    public class ContactRequestDto
    {        
        public int ContactRequestId { get; set; }
        public string Name { get; set; }

        public static ContactRequestDto FromContactRequest(ContactRequest contactRequest)
        {
            var model = new ContactRequestDto();
            model.ContactRequestId = contactRequest.ContactRequestId;
            model.Name = contactRequest.Name;
            return model;
        }
    }
}

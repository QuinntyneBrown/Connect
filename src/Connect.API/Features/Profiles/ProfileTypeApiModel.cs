using Connect.Core.Models;

namespace Connect.API.Features.Profiles
{
    public class ProfileTypeDto
    {
        public int ProfileTypeId { get; set; }
        public string Name { get; set; }        
        public static ProfileTypeDto FromProfileType(ProfileType profileType)
            => new ProfileTypeDto
            {
                ProfileTypeId = profileType.ProfileTypeId,
                Name = profileType.Name
            };
    }
}

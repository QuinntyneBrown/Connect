using Connect.Core.Models;

namespace Connect.API.Features.Profiles
{
    public class ProfileTypeApiModel
    {
        public int ProfileTypeId { get; set; }
        public string Name { get; set; }        
        public static ProfileTypeApiModel FromProfileType(ProfileType profileType)
            => new ProfileTypeApiModel
            {
                ProfileTypeId = profileType.ProfileTypeId,
                Name = profileType.Name
            };
    }
}

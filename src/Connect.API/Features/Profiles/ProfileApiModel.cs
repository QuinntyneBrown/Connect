using Connect.Core.Models;
using System.Collections.Generic;

namespace Connect.API.Features.Profiles
{
    public class ProfileDto
    {        
        public System.Guid ProfileId { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<ProfileImageDto> ProfileImages { get; set; }
        = new HashSet<ProfileImageDto>();
        public System.Guid ProfileTypeId { get; set; }
        public static ProfileDto FromProfile(Profile profile)
        {
            var model = new ProfileDto();
            model.ProfileId = profile.ProfileId;
            model.Name = profile.Name;
            model.Firstname = profile.Firstname;
            model.Lastname = profile.Lastname;            
            model.ProfileTypeId = profile.ProfileType.ProfileTypeId;
            return model;
        }
    }
}

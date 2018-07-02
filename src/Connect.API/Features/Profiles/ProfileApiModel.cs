using Connect.Core.Models;
using System.Collections.Generic;

namespace Connect.API.Features.Profiles
{
    public class ProfileApiModel
    {        
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ICollection<ProfileImageApiModel> ProfileImages { get; set; }
        = new HashSet<ProfileImageApiModel>();
        public string ProfileType { get; set; }
        public static ProfileApiModel FromProfile(Profile profile)
        {
            var model = new ProfileApiModel();
            model.ProfileId = profile.ProfileId;
            model.Name = profile.Name;
            model.Firstname = profile.Firstname;
            model.Lastname = profile.Lastname;            
            model.ProfileType = profile.ProfileType.Name;
            return model;
        }
    }
}

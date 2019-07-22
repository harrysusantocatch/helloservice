using HelloService.Entities.DB;
using HelloService.Entities.Model;

namespace HelloService.Entities.Response
{
    public class UserResponse
    {
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        public Blob ProfilePicture { get; set; }
        public bool Active { get; set; }

        public UserResponse(User user)
        {
            if(user != null)
            {
                Phone = user.Phone;
                FullName = user.FullName;
                About = user.About;
                ProfilePicture = user.ProfilePicture;
                Active = user.Active;
            }
        }
    }
}

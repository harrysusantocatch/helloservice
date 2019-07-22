using HelloService.Entities.DB;

namespace HelloService.DataAccess.Interface
{
    public interface IUserDao
    {
        User FindByPhoneNumber(string phoneNumber);
        User FindByID(string id);
    }
}

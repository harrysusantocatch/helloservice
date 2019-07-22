using Catcher.DB.DAO;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using HelloService.Helper;

namespace HelloService.DataAccess.Implement
{
    public class UserDao : BaseDao<User>, IUserDao
    {
        private static readonly IDao<User> dao = DaoContext.GetDao<User>();

        public User FindByID(string id)
        {
            var user = dao.Find.ByID(id);
            return user;
        }

        public User FindByPhoneNumber(string phoneNumber)
        {
            var user = dao.Find.ByUniqueID("Phone", Encryptor.EncryptSHA256(phoneNumber));
            return user;
        }
    }
}

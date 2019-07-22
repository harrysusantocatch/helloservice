using System;
using Catcher.DB.DAO;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using HelloService.Helper;

namespace HelloService.DataAccess.Implement
{
    public class RegistrationCodeDao : BaseDao<RegistrationCode>, IRegistrationCodeDao
    {
        private static readonly IDao<RegistrationCode> dao = DaoContext.GetDao<RegistrationCode>();

        public RegistrationCode FindByPhoneNumber(string phoneNumber)
        {
            var value = dao.Find.ByUniqueID("Phone", Encryptor.EncryptSHA256(phoneNumber));
            return value;
        }
    }
}

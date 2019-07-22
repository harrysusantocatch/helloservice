using System;
using HelloService.Entities.DB;

namespace HelloService.DataAccess.Interface
{
    public interface IRegistrationCodeDao
    {
        RegistrationCode FindByPhoneNumber(string phoneNumber);
    }
}

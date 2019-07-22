using System;
using HelloService.Helper;

namespace HelloService.Entities.Request
{
    public class LoginRequest
    {
        public string Phone { get; set; }
        private string _securityCode;
        public string SecurityCode {
            get
            {
                if (_securityCode != null)
                    return Encryptor.EncryptSHA256(_securityCode);
                return null;
            }
            set
            {
                _securityCode = value;
            }
        }
    }
}

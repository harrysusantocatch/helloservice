using System;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Interface;
using HelloService.Entities.DB;

namespace HelloService.DataLogic.Implement
{
    public class StatusAppLogic : IStatusAppLogic
    {
        private StatusAppDao _statusAppDao;
        public StatusAppLogic(StatusAppDao statusAppDao)
        {
            _statusAppDao = statusAppDao;
        }

        public string GetStatus()
        {
            string status = "???";
            var data = _statusAppDao.GetStatusApp();
            if (data.Count == 0) status = _statusAppDao.InsertAndGet(new StatusApp() { Status = "Connected" }).Status;
            else status = data[0].Status;
            return status;
        }
    }
}

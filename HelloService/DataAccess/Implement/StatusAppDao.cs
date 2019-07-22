using System;
using System.Collections.Generic;
using Catcher.DB.DAO;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;

namespace HelloService.DataAccess.Implement
{
    public class StatusAppDao : BaseDao<StatusApp>, IStatusAppDao
    {
        private static readonly IDao<StatusApp> dao = DaoContext.GetDao<StatusApp>();

        public IList<StatusApp> GetStatusApp()
        {
            var data = dao.Find.ByFieldValue("Status", "Connected");
            return data;
        }
    }
}

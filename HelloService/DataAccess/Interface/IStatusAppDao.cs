using System;
using System.Collections.Generic;
using HelloService.Entities.DB;

namespace HelloService.DataAccess.Interface
{
    public interface IStatusAppDao
    {
        IList<StatusApp> GetStatusApp();
    }
}

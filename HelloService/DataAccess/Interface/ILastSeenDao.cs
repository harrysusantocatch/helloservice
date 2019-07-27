using HelloService.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Interface
{
    interface ILastSeenDao : IBaseDao<LastSeen>
    {
    }
}

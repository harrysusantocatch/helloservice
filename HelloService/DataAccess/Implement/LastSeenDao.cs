using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Implement
{
    public class LastSeenDao : BaseDao<LastSeen>, ILastSeenDao
    {
    }
}

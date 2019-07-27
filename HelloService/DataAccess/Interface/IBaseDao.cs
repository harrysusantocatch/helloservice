using System;
using Catcher.DB.DTO;

namespace HelloService.DataAccess.Interface
{
    public interface IBaseDao<T> where T : Dto
    {
        bool Insert(T dto);
        T InsertAndGet(T dto);
        bool Update(T dto, string[] fields);
        bool Delete(T dto);
        T FindByID(string id);
        T FindByUniqueID<V>(string fieldName, V fieldValue);
    }
}

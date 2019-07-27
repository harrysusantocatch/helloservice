using System;
using Catcher.DB.DAO;
using Catcher.DB.DTO;
using HelloService.DataAccess.Interface;

namespace HelloService.DataAccess.Implement
{
    public class BaseDao<T> : IBaseDao<T> where T : Dto
    {
        public bool Delete(T dto)
        {
            var success = DaoContext.GetDao<T>().Delete(dto);
            return success;
        }

        public T FindByID(string id)
        {
            var value = DaoContext.GetDao<T>().Find.ByID(id);
            return value;
        }

        public T FindByUniqueID<V>(string fieldName, V fieldValue)
        {
            var value = DaoContext.GetDao<T>().Find.ByUniqueID(fieldName, fieldValue);
            return value;
        }

        public bool Insert(T dto)
        {
            var success = DaoContext.GetDao<T>().Insert(dto);
            return success;
        }

        public T InsertAndGet(T dto)
        {
            var value = DaoContext.GetDao<T>().InsertAndGet(dto);
            return value;
        }

        public bool Update(T dto, string[] fields)
        {
            var success = DaoContext.GetDao<T>().Update(dto, fields);
            return success;
        }
    }
}

using DataAccess.DAO;
using DTOs;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public abstract class CrudFactory
    {

        protected SqlDao Dao { get; } = SqlDao.GetInstance();

        public abstract void Create(BaseDTO entity);
        public abstract void Update(BaseDTO entity);
        public abstract void Delete(BaseDTO entity);

       
        public virtual T Retrieve<T>()
            => throw new System.NotSupportedException("Retrieve<T> no soportado en esta fábrica.");

        public abstract T RetrieveById<T>(int id);
        public abstract IList<T> RetrieveAll<T>();
    }
}
using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
            => _sqlDao = SqlDao.GetInstance();

        public override void Create(BaseDTO entity)
        {
            if (entity is not User user)
                throw new ArgumentException(nameof(entity));

            var op = new SqlOperation { ProcedureName = "CRE_USER_PR" };
            op.AddStringParameter("P_UserCode", user.UserCode);
            op.AddStringParameter("P_Name", user.Name);
            op.AddStringParameter("P_Email", user.Email);
            op.AddStringParameter("P_Password", user.Password);
            op.AddDateTimeParam("P_BirthDate", user.BirthDate);
            op.AddStringParameter("P_Status", user.Status);

            _sqlDao.ExecuteProcedure(op);
        }

        public override void Delete(BaseDTO entity)
            => throw new NotImplementedException();

        public override void Update(BaseDTO entity)
            => throw new NotImplementedException();

        public override T Retrieve<T>()
            => throw new NotImplementedException();

        public override T RetrieveById<T>(int id)
        {
            var op = new SqlOperation { ProcedureName = "RET_USER_BY_ID_PR" };
            op.AddIntParam("P_Id", id);

            var rows = _sqlDao.ExecuteQueryProcedure(op);
            if (rows.Count == 0)
                return default;

            return (T)Convert.ChangeType(BuildUser(rows[0]), typeof(T));
        }

        public override IList<T> RetrieveAll<T>()
        {
            var list = new List<T>();
            var rows = _sqlDao.ExecuteQueryProcedure(
                new SqlOperation { ProcedureName = "RET_ALL_USER_PR" });

            foreach (var row in rows)
                list.Add((T)Convert.ChangeType(BuildUser(row), typeof(T)));

            return list;
        }

        private static User BuildUser(Dictionary<string, object> row) => new()
        {
            Id = Convert.ToInt32(row["Id"]),
            Created = Convert.ToDateTime(row["Created"]),
            UserCode = row["UserCode"]?.ToString(),
            Name = row["Name"]?.ToString(),
            Email = row["Email"]?.ToString(),
            Password = row["Password"]?.ToString(),
            BirthDate = Convert.ToDateTime(row["BirthDate"]),
            Status = row["Status"]?.ToString()
        };
    }
}

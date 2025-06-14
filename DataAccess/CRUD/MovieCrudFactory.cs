using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public sealed class MovieCrudFactory : CrudFactory
    {
        public MovieCrudFactory()
            => _sqlDao = SqlDao.GetInstance();

        #region Create / Delete / Update

        public override void Create(BaseDTO baseDto)
        {
            if (baseDto is not Movie movie)
                throw new ArgumentException("Se esperaba un Movie", nameof(baseDto));

            var op = new SqlOperation("CRE_MOVIE_PR")
                .AddStringParameter("P_Title", movie.Title)
                .AddStringParameter("P_Description", movie.Description)
                .AddDateTimeParam("P_ReleaseDate", movie.ReleaseDate)
                .AddStringParameter("P_Genre", movie.Genre)
                .AddStringParameter("P_Director", movie.Director);

            _sqlDao.ExecuteProcedure(op);
        }

        public override void Delete(BaseDTO baseDto)
            => throw new NotImplementedException();

        public override void Update(BaseDTO baseDto)
            => throw new NotImplementedException();

        #endregion

        #region Retrieves

        public override T Retrieve<T>()
            => throw new NotImplementedException();

        public override T RetrieveById<T>(int id)
        {
            var op = new SqlOperation("RET_MOVIE_BY_ID_PR")
                .AddIntParam("P_Id", id);

            var rows = _sqlDao.ExecuteQueryProcedure(op);
            if (rows.Count == 0)
                return default;

            return (T)Convert.ChangeType(BuildMovie(rows[0]), typeof(T));
        }

        public override List<T> RetrieveAll<T>()
        {
            var list = new List<T>();
            var rows = _sqlDao.ExecuteQueryProcedure(
                new SqlOperation("RET_ALL_MOVIE_PR"));

            foreach (var row in rows)
            {
                list.Add((T)Convert.ChangeType(BuildMovie(row), typeof(T)));
            }

            return list;
        }

        #endregion

        #region Helper

        private static Movie BuildMovie(Dictionary<string, object> row) => new()
        {
            Id = Convert.ToInt32(row["Id"]),
            Created = Convert.ToDateTime(row["Created"]),
            Title = row["Title"]?.ToString(),
            Description = row["Description"]?.ToString(),
            ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]),
            Genre = row["Genre"]?.ToString(),
            Director = row["Director"]?.ToString()
        };

        #endregion
    }
}

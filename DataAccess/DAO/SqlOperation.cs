using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAO
{
    /*
     * Clase que define una operación SQL basada en stored procedures.
     */
    public class SqlOperation
    {

        public string ProcedureName { get; set; } = string.Empty;
        public List<SqlParameter> Parameters { get; set; }


        public SqlOperation()
        {
            Parameters = new List<SqlParameter>();
        }

        public SqlOperation(string procedureName) : this()
        {
            ProcedureName = procedureName;
        }

        public void AddStringParameter(string ParamName, string ParamValue)
            => Parameters.Add(new SqlParameter(ParamName, ParamValue));

        public void AddIntParam(string paramName, int paramValue)
            => Parameters.Add(new SqlParameter(paramName, paramValue));

        public void AddDoubleParam(string paramName, double paramValue)
            => Parameters.Add(new SqlParameter(paramName, paramValue));

        public void AddDateTimeParam(string paramName, DateTime paramValue)
            => Parameters.Add(new SqlParameter(paramName, paramValue));
    }
}
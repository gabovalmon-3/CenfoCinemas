using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAO
{

    /*
     * Clase u objeto que se encarga de la comunicación con la base de datos SQL Server.
     * Solo ejecuta stored procedures.
     * Esta clase implementa el patron de Singleton para asegurar la existencia de una única instancia.
     */
    public class SqlDao
    {
        // Paso 1: Crear una instacia privada estática de la clase SqlDao.

        private static SqlDao _instance;

        private string _connectionString;

        // Paso 2: Redefinir el constructor default como privado para evitar que se pueda crear una instancia de la clase desde fuera.

        private SqlDao()
        {
            _connectionString = @"Data Source=srv-sqldatabase-gvalverdem.database.windows.net;Initial Catalog=cenfocinemas-db;User ID=sysman;Password=Cenfotec123!;Trust Server Certificate=True";
        }

        // Paso 3: Definir el metodo que expondrá la instancia de la clase SqlDao.

        public static SqlDao GetInstance()
        {
            // Verificar si la instancia es nula, si lo es, crear una nueva instancia de la clase SqlDao.
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            // Retornar la instancia de la clase SqlDao.
            return _instance;
        }

        // Metodo para la ejecución de stored procedures sin retorno de datos.

        public void ExecuteProcedure(SqlOperation sqlOperation)
        {
            // Conectarse a la base de datos y ejecutar el stored procedure sin retorno de datos.
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    // Agregar los parámetros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    // Ejectura el SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Metodo para la ejecución de stored procedures con retorno de datos.

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            // Conectarse a la base de datos y ejecutar el stored procedure, capturar el resultado y convertirlo en DTOs
            // Actualmente no implementado, solo retorna una lista vacía.
            var list = new List<Dictionary<string, object>>();
            return list;
        }
    }
}
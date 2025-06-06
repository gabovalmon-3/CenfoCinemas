using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    /*
     * Clase u objeto responsable de la comunicación con la base de datos.
     * Solo ejecuta procedimientos almacenados.
     *
     * Esta clase implementa el patrón Singleton
     * para garantizar una única instancia.
     */
    public class SqlDao
    {
        // Paso 1: Crear instancia privada de la misma clase
        private static SqlDao instance;

        private string connectionString;

        // Paso 2: Ocultar el constructor predeterminado haciéndolo privado
        private SqlDao()
        {
            connectionString = string.Empty;
        }

        // Paso 3: Proporcionar el método que devuelve la instancia singleton
        public static SqlDao GetInstance()
        {
            if (instance == null)
            {
                instance = new SqlDao();
            }
            return instance;
        }

        // Método que ejecuta un procedimiento almacenado sin retornar datos
        public void ExecuteProcedure(SqlOperation operation)
        {
            // Abrir conexión a la base de datos
            // Ejecutar el procedimiento almacenado
        }

        // Método que ejecuta un procedimiento almacenado y devuelve resultados
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            // Abrir conexión a la base de datos
            // Ejecutar el procedimiento almacenado
            // Leer el conjunto de resultados
            // Convertir cada fila en un diccionario

            var list = new List<Dictionary<string, object>>();
            return list;
        }
    }
}

using DataAccess.DAO;
using System.Net.NetworkInformation;
using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "gvalverdem");
        sqlOperation.AddStringParameter("P_Name", "Gabriel");
        sqlOperation.AddStringParameter("P_Email", "gvalverdem@ucenfotec.ac.cr");
        sqlOperation.AddStringParameter("P_Password", "Gabriel123!");
        sqlOperation.AddDateTimeParam("P_BirthDate", new DateTime(2005, 03, 28 ));
        sqlOperation.AddStringParameter("P_Status", "AC");

        var sqlDao = SqlDao.GetInstance();
        sqlDao.ExecuteProcedure(sqlOperation);

        Console.WriteLine("Stored procedure ejecutado correctamente.");
    }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
        }
    }
}
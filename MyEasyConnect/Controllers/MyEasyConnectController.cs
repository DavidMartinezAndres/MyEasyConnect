using MyEasyConnect.Models;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Text;

using System.Web.Http;

namespace MyEasyConnect.Controllers
{
    public class MyEasyConnectController : ApiController
    {

        readonly string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ConfigurationManager.AppSettings["DBHost"] + ")" +
            "(PORT=" + ConfigurationManager.AppSettings["DBPort"] + "))" +
            "(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=" + ConfigurationManager.AppSettings["DBServiceName"] + ")));" +
            "User Id=" + ConfigurationManager.AppSettings["DBUser"] + ";Password=" + ConfigurationManager.AppSettings["DBPassword"] + ";";

        [Route("getWorker/{id}")]
        public Worker GetWorker(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT IDWORKER            AS \"IdWorker\", ");
            sql.Append("       W.NAME              AS \"Name\", ");
            sql.Append("       FIRST_SURNAME       AS \"FirstSurname\", ");
            sql.Append("       SECOND_SURNAME      AS \"SecondSurname\", ");
            sql.Append("       PROFILE_PICTURE     AS \"ProfilePicture\", ");
            sql.Append("       POINTS, ");
            sql.Append("       J.NAME              AS \"Job\" ");
            sql.Append("  FROM DMARTINEZ.WORKER W INNER JOIN JOB J ON W.IDJOB = J.ID ");
            sql.Append(" WHERE W.IDWORKER = :IDWORKER ");

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = sql.ToString();

                    cmd.BindByName = true;
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("IDWORKER", OracleDbType.Int32).Value = id;

                    DataTable table = new DataTable();

                    Worker response = new Worker();
                    return response;
                    /*using (OracleDataAdapter data = new OracleDataAdapter(cmd))
                    {
                        data.Fill(table);

                        return response;
                    }*/
                }
            }
        }

    }
}
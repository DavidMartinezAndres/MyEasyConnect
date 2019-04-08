using MyEasyConnect.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Http;

namespace MyEasyConnect.Controllers
{
    public class MyEasyConnectController : ApiController
    {
        [Route("getCorreos")]
        public List<Mail> getCorreos()
        {
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ConfigurationManager.AppSettings["DBHost"] + ")" +
          "(PORT=" + ConfigurationManager.AppSettings["DBPort"] + "))" +
          "(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=" + ConfigurationManager.AppSettings["DBServiceName"] + ")));" +
          "User Id=" + ConfigurationManager.AppSettings["DBUser"] + ";Password=" + ConfigurationManager.AppSettings["DBPassword"] + ";";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    StringBuilder sql = new StringBuilder();
                    sql.Append("SELECT id, ");
                    sql.Append("       status                   AS \"Estado\", ");
                    sql.Append("       sbuject                  AS \"Asunto\", ");
                    sql.Append("       sending_date             AS \"Fecha\", ");
                    sql.Append("       body_text                AS \"Mensaje\", ");
                    sql.Append("       WORKER.NAME              AS \"Nombre\", ");
                    sql.Append("       WORKER.first_surname     AS \"Apellido\" ");
                    sql.Append("  FROM dmartinez.MESSAGE INNER JOIN dmartinez.worker ON WORKER.IDWORKER = MESSAGE.SENDER_ID ");
                    sql.Append(" WHERE receiver_id = :receiver_id; ");

                    cmd.CommandText = sql.ToString();

                    cmd.BindByName = true;

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("receiver_id", 1);

                    List<Mail> mailList = new List<Mail>();

                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Mail mail = new Mail();
                        mail.Id = Convert.ToInt32(dr["Estado"]);
                        mail.Subject = Convert.ToString(dr["Asunto"]);
                        mail.MailDate = Convert.ToString(dr["Fecha"]);
                        mail.MessageBody = Convert.ToString(dr["Mensaje"]);
                        mail.SenderName = Convert.ToString(dr["Nombre"]);
                        mail.SenderSurname = Convert.ToString(dr["Apellido"]);

                        mailList.Add(mail);
                    }
                    return mailList;
                }
            }
                   
        }
      


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
using MyEasyConnect.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

        [Route("getCorreos")]
        [HttpPost]
        public GetCorreosRS GetCorreos([FromBody] GetCorreosRQ requestItem)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    StringBuilder sql = new StringBuilder();
                    sql.Append("SELECT id, ");
                    sql.Append("       status              Status, ");
                    sql.Append("       sbuject             Subject, ");
                    sql.Append("       sending_date        MailDate, ");
                    sql.Append("       body_text           MessageBody, ");
                    sql.Append("       W.NAME              Sender, ");
                    sql.Append("       W.FIRST_SURNAME     FirstSurname ");
                    sql.Append("  FROM dmartinez.MESSAGE M INNER JOIN DMARTINEZ.WORKER W ON W.IDWORKER = M.SENDER_ID ");
                    sql.Append(" WHERE receiver_id = :receiver_id ");


                    cmd.CommandText = sql.ToString();

                    cmd.BindByName = true;

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("receiver_id", requestItem.WorkerId);

                    List<Mail> mailList = new List<Mail>();

                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Mail mail = new Mail
                        {
                            Id = Convert.ToInt32(dr["STATUS"]),
                            Subject = Convert.ToString(dr["SUBJECT"]),
                            MailDate = Convert.ToString(dr["MAILDATE"]),
                            MessageBody = Convert.ToString(dr["MESSAGEBODY"]),
                            SenderName = Convert.ToString(dr["SENDER"]),
                            SenderSurname = Convert.ToString(dr["FIRSTSURNAME"])
                        };

                        mailList.Add(mail);
                    }
                    return new GetCorreosRS() { Mails = mailList };
                }
            }
                   
        }

        [Route("getWorker")]
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

                    using (OracleDataAdapter data = new OracleDataAdapter(cmd))
                    {
                        data.Fill(table);
                        return table.AsEnumerable().Select(dr =>
                            new Worker()
                            {
                                Id = Convert.ToInt32(dr["IdWorker"]),
                                Name = dr["Name"].ToString(),
                                FirstSurname = dr["FirstSurname"].ToString(),
                                SecondSurname = dr["SecondSurname"].ToString(),
                                ProfilePicture = dr["ProfilePicture"].ToString(),
                                Points = Convert.ToInt32(dr["POINTS"]),
                                Job = dr["Job"].ToString()
                            }
                        ).ToList().First();
                    }
                }
            }
        }

        [Route("getCircleCare")]
        public List<Worker> GetCircleCare(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT W.IDWORKER          AS \"IdWorker\", ");
            sql.Append("       W.NAME              AS \"Name\", ");
            sql.Append("       FIRST_SURNAME       AS \"FirstSurname\", ");
            sql.Append("       SECOND_SURNAME      AS \"SecondSurname\", ");
            sql.Append("       PROFILE_PICTURE     AS \"ProfilePicture\", ");
            sql.Append("       POINTS              AS \"Points\", ");
            sql.Append("       J.NAME              AS \"Job\" ");
            sql.Append("  FROM DMARTINEZ.WORKER  W ");
            sql.Append("       INNER JOIN CIRCLECARE CC ON W.IDWORKER = CC.IDFRIEND AND CC.IDWORKER = :IDWORKER ");
            sql.Append("       INNER JOIN JOB J ON W.IDJOB = J.ID ");


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

                    using (OracleDataAdapter data = new OracleDataAdapter(cmd))
                    {
                        data.Fill(table);
                        return table.AsEnumerable().Select(dr =>
                            new Worker()
                            {
                                Id = Convert.ToInt32(dr["IdWorker"]),
                                Name = dr["Name"].ToString(),
                                FirstSurname = dr["FirstSurname"].ToString(),
                                SecondSurname = dr["SecondSurname"].ToString(),
                                ProfilePicture = dr["ProfilePicture"].ToString(),
                                Points = Convert.ToInt32(dr["POINTS"]),
                                Job = dr["Job"].ToString()
                            }
                        ).ToList();

                    }
                }
            }
        }

        [Route("getReminders")]
        [HttpPost]
        public GetRemindersRS GetReminders([FromBody] GetRemindersRQ requestItem)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT idreminder           Id, ");
            sql.Append("       remindertitle        Title, ");
            sql.Append("       remindersubtitle     Subtitle, ");
            sql.Append("       reminderdate         EventDate, ");
            sql.Append("       description          Description ");
            sql.Append("  FROM dmartinez.reminder ");
            sql.Append(" WHERE idworker = :idworker ");


            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = sql.ToString();

                    cmd.BindByName = true;
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("idworker", requestItem.WorkerId);

                    List<Reminder> ResponseList = new List<Reminder>();

                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Reminder reminder = new Reminder
                        {
                            Id = Convert.ToInt32(dr["ID"]),
                            Title = Convert.ToString(dr["TITLE"]),
                            Subtitle = Convert.ToString(dr["SUBTITLE"]),
                            EventDate = Convert.ToString(dr["EVENTDATE"]),
                            Description = Convert.ToString(dr["DESCRIPTION"])
                        };

                        ResponseList.Add(reminder);
                    }

                    return new GetRemindersRS { ReminderList = ResponseList };
                }

            }
        } 

    }
}
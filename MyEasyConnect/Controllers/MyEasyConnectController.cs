using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MyEasyConnect.Controllers
{
    public class MyEasyConnectController : ApiController
    {
        readonly string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ConfigurationManager.AppSettings["DBHost"] + ")" +
            "(PORT=" + ConfigurationManager.AppSettings["DBPort"] + "))" +
            "(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=" + ConfigurationManager.AppSettings["DBServiceName"] + ")));" +
            "User Id=" + ConfigurationManager.AppSettings["DBUser"] + ";Password=" + ConfigurationManager.AppSettings["DBPassword"] + ";";

        [Route("helloworld")]
        public String Get()
        {
            String x = "Hello World!";
            return x;
        }

    }
}
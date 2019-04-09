using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class GetCorreosRQ
    {
        public Worker Worker { get; set; }


        public GetCorreosRQ(Worker Worker)
        {
            this.Worker = Worker;
        }

    }
}
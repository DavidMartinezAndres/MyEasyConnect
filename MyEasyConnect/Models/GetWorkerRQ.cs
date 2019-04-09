using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class GetWorkerRQ
    {
        public Worker Worker { get; set; }

        public GetWorkerRQ(Worker worker)
        {
            this.Worker = worker;
        }
    }
}
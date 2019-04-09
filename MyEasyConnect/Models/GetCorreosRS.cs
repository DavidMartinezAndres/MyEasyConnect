using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class GetCorreosRS
    {
        public List<Mail> Lista { get; set; }

        public GetCorreosRS(List<Mail> Lista) {
            this.Lista = Lista;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string status { get; set; }
        public string Subject { get; set; }
        public string MailDate { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
    }
}
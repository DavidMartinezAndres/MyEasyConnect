using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string ProfilePicture { get; set; }
        public int Points { get; set; }
        public string Job { get; set; }
    }
}
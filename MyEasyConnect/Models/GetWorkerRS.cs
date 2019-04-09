using MyEasyConnect.Models;

namespace MyEasyConnect.Controllers
{
    public class GetWorkerRS : GetWorkerRQ
    {
        public Worker Worker { get; set; }
    }
}
namespace MyEasyConnect.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string MailDate { get; set; }
        public string MessageBody { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
    }
}
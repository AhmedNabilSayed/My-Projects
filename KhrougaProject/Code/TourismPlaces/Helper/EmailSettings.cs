using System.Net;
using System.Net.Mail;

namespace TourismPlaces.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential
                ("ahmednetdeveloper6@gmail.com", "vqnaceonizthtmqo");
            client.Send("ahmednetdeveloper6@gmail.com", email.To, email.Title, email.Body);

        }
    }
}

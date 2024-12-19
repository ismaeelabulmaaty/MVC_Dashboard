using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.pl.Helpers
{
    public class EmailSettings
    {

        public static void sendEmail(Email email)
        {

            var clint = new SmtpClient("Smtp.gmail.com", 587);
            clint.EnableSsl = true;
            clint.Credentials = new NetworkCredential("ismaeelmatty@gmail.com", "ismaeelmatty01003793959");
            clint.Send("ismaeelmatty@gmail.com", email.Recipents, email.Subject, email.Body);
        }

    }
}

using System;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.comun.aplicacion.test
{
    [TestClass]
    public class EnviarCorreoTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }


        /// <summary>
        /// http://www.technical-recipes.com/2018/how-to-send-an-e-mail-via-google-smtp-using-c/
        /// </summary>
        [TestMethod]
        public void EnviarCorreoGmailTest()
        {
            //Cambiar informacion.
            var emailFrom = "ORIGEN@gmail.com";
            var password = "CAMBIAR";
            var emailTo = "DESTINO@gmail.com";

            // Credentials
            var credentials = new NetworkCredential(emailFrom, password);

            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress(emailTo),
                Subject = "Test email.",
                Body = "Test email body"
            };

            mail.To.Add(new MailAddress(emailTo));

            // Smtp client
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };

            // Send it...         
            client.Send(mail);

        }
    }
}

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialNetwork.Core.Domain.Settings;
using SocialNewtwork.Core.Application.Dtos.Email;
using SocialNewtwork.Core.Application.Interfaces.Services;


namespace SocialNetwork.Infraestructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                //Configuracion del enviador de correos.
                using var smtp = new SmtpClient();
                //Conexion del smtp, pasamos el host,puerto y protocolo de seguridad.
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                //Autenticacion para el envio de correos.
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                //Enviando el correo de manera async.
                await smtp.SendAsync(email);
                //Por ultimo desconectar el stmp
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

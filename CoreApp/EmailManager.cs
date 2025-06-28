using DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreApp
{
    public class EmailManager : BaseManager
    {
        private readonly string apiKey = "API_Key";
        private readonly string fromEmail = "gvalverdem@ucenfotec.ac.cr";
        private readonly string fromName = "CenfoCinemas";

        public async Task SendWelcomeEmail(string email, string name)
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var to = new EmailAddress(email, name);
                var subject = "¡Bienvenido a CenfoCinemas!";
                var plainTextContent = $"¡Hola {name}! Gracias por unirte.";
                var htmlContent = $"<p>¡Hola <strong>{name}</strong>! Bienvenido a CenfoCinemas.</p>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        public async Task SendNewMovie(string movieTitle, List<User> recipients)
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(fromEmail, fromName);
                var subject = $"¡Estreno: {movieTitle}!";
                var plainTextContent = $"Ya puedes ver '{movieTitle}' en CenfoCinemas.";
                var htmlContent = $"<p>¡Estreno!</p><p><strong>{movieTitle}</strong> ya está disponible.</p>";

                foreach (var user in recipients)
                {
                    var to = new EmailAddress(user.Email, user.Name ?? "Usuario");
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    await client.SendEmailAsync(msg).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }
    }
}
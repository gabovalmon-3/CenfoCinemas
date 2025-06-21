using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager : BaseManager
    {
        /*
         * Pasos para registro de usuario:
         * 1) Validar edad mínima (18+)
         * 2) Verificar código único
         * 3) Verificar email no usado
         * 4) Enviar email de bienvenida
         */
        public void Create(User user)
        {
            try
            {
                if (!IsOver18(user))
                    throw new Exception("Debes tener al menos 18 años.");

                var repo = new UserCrudFactory();
                // ya no existen RetrieveByUserCode ni RetrieveByEmail
                var byCode = repo.RetrieveAll<User>()
                                  .FirstOrDefault(u => u.UserCode == user.UserCode);
                if (byCode != null)
                    throw new Exception("El código ya está en uso.");

                var byEmail = repo.RetrieveAll<User>()
                                  .FirstOrDefault(u => u.Email == user.Email);
                if (byEmail != null)
                    throw new Exception("Ese correo ya está registrado.");

                repo.Create(user);

                // enviamos el welcome email
                var emailer = new EmailManager();
                emailer.SendWelcomeEmail(user.Email, user.Name)
                       .GetAwaiter()
                       .GetResult();
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        private bool IsOver18(User user)
        {
            var today = DateTime.Today;
            var age = today.Year - user.BirthDate.Year;
            if (user.BirthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
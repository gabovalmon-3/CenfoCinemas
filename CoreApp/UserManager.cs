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
         * Metodo para la creacion de un usuario
         * Valida que el usuario sea mayor de 18 años
         * Valida que el código de usuario está disponible
         * Valida que el correo electrónico no esté registrado
         * Envia un correo electrónico de bienvenida al usuario
         */

        public void Create(User user)
        {
            try
            {
                // Validar que el usuario sea mayor de 18 años
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();

                    // Consultamos en la base de datos si el código de usuario ya existe
                    var uExist = uCrud.RetrieveByUserCode<User>(user);

                    if (uExist == null)
                    {
                        // Consultamos en la base de datos si el correo electrónico ya existe
                        uExist = uCrud.RetrieveByEmail<User>(user);

                        if (uExist == null)
                        {
                            uCrud.Create(user);
                            //Enviar correo electrónico de bienvenida al usuario                            
                            //var emailManager = new EmailManager();
                            //emailManager.SendWelcomeEmail(user.Email, user.Name).GetAwaiter().GetResult();
                        }
                        else
                        {
                            throw new Exception("El correo electrónico ya está en uso. Intente nuevamente con otro correo electrónico");
                        }
                    }
                    else
                    {
                        throw new Exception("Ese código de usuario ya está en uso. Intente con uno diferente.");
                    }
                }
                else
                {
                    throw new Exception("La edad ingresada no es válida. Se requiere tener más de 18 años.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        public List<User> RetrieveAll()
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveAll<User>();
        }

        public User RetrieveById(int id)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveById<User>(id);
        }

        public User RetrieveByUserCode(string userCode)
        {
            var uCrud = new UserCrudFactory();
            var udto = new User { UserCode = userCode };
            return uCrud.RetrieveByUserCode<User>(udto);
        }

        public User RetrieveByEmail(string email)
        {
            var uCrud = new UserCrudFactory();
            var udto = new User { Email = email };
            return uCrud.RetrieveByEmail<User>(udto);
        }

        public User Update(User user)
        {
            var uCrud = new UserCrudFactory();
            uCrud.Update(user);
            return RetrieveById(user.Id);
        }

        public void Delete(int id)
        {
            var uCrud = new UserCrudFactory();
            uCrud.Delete(new User { Id = id });
        }

        private bool IsOver18(User user)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.BirthDate.Year;

            if (user.BirthDate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }

    }
}
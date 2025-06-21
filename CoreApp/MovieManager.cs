using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MovieManager : BaseManager
    {
        public void Create(Movie movie)
        {
            try
            {
                var repo = new MovieCrudFactory();

                // Buscamos duplicados en toda la lista
                var duplicate = repo
                    .RetrieveAll<Movie>()
                    .FirstOrDefault(m => m.Title.Equals(movie.Title, StringComparison.OrdinalIgnoreCase));

                if (duplicate != null)
                    throw new Exception("Ya existe una película con ese título.");

                // Insertamos la nueva
                repo.Create(movie);

                // Recuperamos todos los usuarios y convertimos a List<User>
                var users = new UserCrudFactory()
                                .RetrieveAll<User>()
                                .ToList();

                // Enviamos notificación
                var emailer = new EmailManager();
                emailer.SendNewMovie(movie.Title, users)
                       .GetAwaiter()
                       .GetResult();
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }
    }
}
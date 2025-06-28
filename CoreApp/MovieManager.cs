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
        /*
         * Metodo para la creacion de una pelicula
         * Valida que el título de la película no exista en la base de datos
         */

        public void Create(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveByTitle<Movie>(movie);

                if (mExist == null)
                {
                    mCrud.Create(movie);

                    // Aqui se llama a todos los usuarios para enviar un email diciendo que hay una nueva pelicula:

                }
                else
                {
                    throw new Exception("El título de la película ya existe. Por favor, ingrese otro título.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        public List<Movie> RetrieveAll()
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveAll<Movie>();
        }

        public Movie RetrieveById(int id)
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveById<Movie>(id);
        }

        public Movie RetrieveByTitle(Movie movie)
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveByTitle<Movie>(movie);
        }

        public Movie RetrieveByTitle(string title)
        {

            var dto = new Movie { Title = title };
            return RetrieveByTitle(dto);
        }

        public Movie Update(Movie movie)
        {
            var mCrud = new MovieCrudFactory();
            mCrud.Update(movie);
            return RetrieveById(movie.Id);
        }

        public void Delete(int id)
        {
            var mCrud = new MovieCrudFactory();
            mCrud.Delete(new Movie { Id = id });
        }
    }
}
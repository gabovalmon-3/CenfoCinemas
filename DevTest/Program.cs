using System;
using System.Collections.Generic;
using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Crear Usuario");
            Console.WriteLine("2. Consultar Usuarios");
            Console.WriteLine("3. Consultar Usuario por Id");
            Console.WriteLine("4. Actualizar Usuario");
            Console.WriteLine("5. Eliminar Usuario");
            Console.WriteLine("6. Registrar Película");
            Console.WriteLine("7. Consultar Películas");
            Console.WriteLine("8. Consultar Película por Id");
            Console.WriteLine("9. Actualizar Película");
            Console.WriteLine("10. Eliminar Película");
            Console.WriteLine("11. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1": CrearUsuario(); break;
                case "2": ConsultarUsuarios(); break;
                case "3": ConsultarUsuarioPorId(); break;
                case "4": ActualizarUsuario(); break;
                case "5": EliminarUsuario(); break;
                case "6": RegistrarPelicula(); break;
                case "7": ConsultarPeliculas(); break;
                case "8": ConsultarPeliculaPorId(); break;
                case "9": ActualizarPelicula(); break;
                case "10": EliminarPelicula(); break;
                case "11":
                    Console.WriteLine("¡Gracias por usar el menú!");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine();
        }
    }

    public static void CrearUsuario()
    {
        try
        {
            Console.WriteLine("=== Crear Nuevo Usuario ===");
            Console.Write("Código de Usuario: ");
            var userCode = Console.ReadLine();
            Console.Write("Nombre: ");
            var name = Console.ReadLine();
            Console.Write("Correo Electrónico: ");
            var email = Console.ReadLine();
            Console.Write("Contraseña: ");
            var password = Console.ReadLine();
            Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var birthDate))
            {
                Console.WriteLine("Fecha inválida. Debe tener el formato YYYY-MM-DD.");
                return;
            }
            Console.Write("Estado (ej: AC): ");
            var status = Console.ReadLine();

            var user = new User
            {
                UserCode = userCode,
                Name = name,
                Email = email,
                Password = password,
                BirthDate = birthDate,
                Status = status
            };

            new UserCrudFactory().Create(user);
            Console.WriteLine("Usuario creado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear usuario: {ex.Message}");
        }
    }

    public static void ConsultarUsuarios()
    {
        Console.WriteLine("=== Listado de Usuarios ===");
        var list = new UserCrudFactory().RetrieveAll<User>();
        foreach (var u in list)
            Console.WriteLine(JsonConvert.SerializeObject(u));
    }

    public static void ConsultarUsuarioPorId()
    {
        try
        {
            Console.WriteLine("=== Consultar Usuario por Id ===");
            Console.Write("Ingrese el ID del usuario: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var u = new UserCrudFactory().RetrieveById<User>(id);
            if (u == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.WriteLine("Usuario encontrado:");
            Console.WriteLine(JsonConvert.SerializeObject(u));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al consultar usuario: {ex.Message}");
        }
    }

    public static void ActualizarUsuario()
    {
        try
        {
            Console.WriteLine("=== Actualizar Usuario ===");
            Console.Write("Ingrese el ID del usuario a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var crud = new UserCrudFactory();
            var user = crud.RetrieveById<User>(id);
            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.Write($"Nuevo nombre (actual: {user.Name}): ");
            var newName = Console.ReadLine();
            Console.Write($"Nuevo correo (actual: {user.Email}): ");
            var newEmail = Console.ReadLine();
            Console.Write($"Nuevo estado (actual: {user.Status}): ");
            var newStatus = Console.ReadLine();

            user.Name = string.IsNullOrWhiteSpace(newName) ? user.Name : newName;
            user.Email = string.IsNullOrWhiteSpace(newEmail) ? user.Email : newEmail;
            user.Status = string.IsNullOrWhiteSpace(newStatus) ? user.Status : newStatus;

            crud.Update(user);
            Console.WriteLine("Usuario actualizado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
        }
    }

    public static void EliminarUsuario()
    {
        try
        {
            Console.WriteLine("=== Eliminar Usuario ===");
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var crud = new UserCrudFactory();
            var user = crud.RetrieveById<User>(id);
            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            crud.Delete(user);
            Console.WriteLine("Usuario eliminado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
        }
    }

    public static void RegistrarPelicula()
    {
        try
        {
            Console.WriteLine("=== Registrar Nueva Película ===");
            Console.Write("Título: ");
            var title = Console.ReadLine();
            Console.Write("Descripción: ");
            var desc = Console.ReadLine();
            Console.Write("Fecha de Estreno (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var releaseDate))
            {
                Console.WriteLine("Fecha inválida. Debe tener el formato YYYY-MM-DD.");
                return;
            }
            Console.Write("Género: ");
            var genre = Console.ReadLine();
            Console.Write("Director: ");
            var director = Console.ReadLine();

            var movie = new Movie
            {
                Title = title,
                Description = desc,
                ReleaseDate = releaseDate,
                Genre = genre,
                Director = director
            };

            new MovieCrudFactory().Create(movie);
            Console.WriteLine("Película registrada exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar película: {ex.Message}");
        }
    }

    public static void ConsultarPeliculas()
    {
        Console.WriteLine("=== Listado de Películas ===");
        var list = new MovieCrudFactory().RetrieveAll<Movie>();
        foreach (var m in list)
            Console.WriteLine(JsonConvert.SerializeObject(m));
    }

    public static void ConsultarPeliculaPorId()
    {
        try
        {
            Console.WriteLine("=== Consultar Película por Id ===");
            Console.Write("Ingrese el ID de la película: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var m = new MovieCrudFactory().RetrieveById<Movie>(id);
            if (m == null)
            {
                Console.WriteLine("Película no encontrada.");
                return;
            }

            Console.WriteLine("Película encontrada:");
            Console.WriteLine(JsonConvert.SerializeObject(m));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al consultar película: {ex.Message}");
        }
    }

    public static void ActualizarPelicula()
    {
        try
        {
            Console.WriteLine("=== Actualizar Película ===");
            Console.Write("Ingrese el ID de la película a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var crud = new MovieCrudFactory();
            var movie = crud.RetrieveById<Movie>(id);
            if (movie == null)
            {
                Console.WriteLine("Película no encontrada.");
                return;
            }

            Console.Write($"Nuevo título (actual: {movie.Title}): ");
            var newTitle = Console.ReadLine();
            Console.Write($"Nueva descripción (actual: {movie.Description}): ");
            var newDesc = Console.ReadLine();
            Console.Write($"Nueva fecha de estreno ({movie.ReleaseDate:yyyy-MM-dd}): ");
            var dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                if (!DateTime.TryParse(dateInput, out var parsed))
                {
                    Console.WriteLine("Fecha inválida."); return;
                }
                movie.ReleaseDate = parsed;
            }
            Console.Write($"Nuevo género (actual: {movie.Genre}): ");
            var newGenre = Console.ReadLine();
            Console.Write($"Nuevo director (actual: {movie.Director}): ");
            var newDir = Console.ReadLine();

            movie.Title = string.IsNullOrWhiteSpace(newTitle) ? movie.Title : newTitle;
            movie.Description = string.IsNullOrWhiteSpace(newDesc) ? movie.Description : newDesc;
            movie.Genre = string.IsNullOrWhiteSpace(newGenre) ? movie.Genre : newGenre;
            movie.Director = string.IsNullOrWhiteSpace(newDir) ? movie.Director : newDir;

            crud.Update(movie);
            Console.WriteLine("Película actualizada exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar película: {ex.Message}");
        }
    }

    public static void EliminarPelicula()
    {
        try
        {
            Console.WriteLine("=== Eliminar Película ===");
            Console.Write("Ingrese el ID de la película a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var crud = new MovieCrudFactory();
            var movie = crud.RetrieveById<Movie>(id);
            if (movie == null)
            {
                Console.WriteLine("Película no encontrada.");
                return;
            }

            crud.Delete(movie);
            Console.WriteLine("Película eliminada exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar película: {ex.Message}");
        }
    }
}

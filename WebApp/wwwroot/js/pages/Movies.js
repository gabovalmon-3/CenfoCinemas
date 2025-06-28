// JavaScript encargado de gestionar toda la lógica de la página de películas
// Definimos una clase usando prototipos

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    // Función de inicialización

    this.initView = function () {

        console.log("Movie init view --> OK");
        // Cargamos la tabla con los datos de las películas
        this.LoadTable();

        // Configuramos el botón Crear para invocar la creación de película
        $('#btnCreate').click(function () {
            var vc = new MoviesViewController();
            vc.Create();
        });

        // Configuramos el botón Actualizar para invocar la modificación de película
        $('#btnUpdate').click(function () {
            var vc = new MoviesViewController();
            vc.Update();
        });

        // Configuramos el botón Eliminar para invocar la eliminación de película
        $('#btnDelete').click(function () {
            var vc = new MoviesViewController();
            vc.Delete();
        });
    };

    // Método que llena la tabla de películas

    this.LoadTable = function () {

        // Endpoint para obtener todas las películas
        // https://localhost:7191/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        /*
            Estructura de cabeceras esperada:
            <tr>
                <th>ID</th>
                <th>Título</th>
                <th>Descripción</th>
                <th>Fecha de Estreno</th>
                <th>Género</th>
                <th>Director</th>
            </tr>
        */

        var columns = [
            { data: 'id' },
            { data: 'title' },
            { data: 'description' },
            { data: 'releaseDate' },
            { data: 'genre' },
            { data: 'director' }
        ];

        // Inicializamos DataTable para mostrar las películas de forma dinámica
        $('#tblMovies').DataTable({
            ajax: {
                url: urlService,
                dataSrc: ""
            },
            columns: columns
        });

        // Cuando se hace clic en una fila, rellenamos el formulario con esos datos
        $('#tblMovies tbody').on('click', 'tr', function () {
            var movieDTO = $('#tblMovies').DataTable().row(this).data();
            $('#txtId').val(movieDTO.id);
            $('#txtTitle').val(movieDTO.title);
            $('#txtDescription').val(movieDTO.description);
            $('#txtGenre').val(movieDTO.genre);
            $('#txtDirector').val(movieDTO.director);

            // Formateamos la fecha para mostrar solo la parte de día
            var datePart = movieDTO.releaseDate.split('T')[0];
            $('#txtRDate').val(datePart);
        });
    };

    // Método para enviar al API una nueva película

    this.Create = function () {
        var movieDTO = {};
        // Valores predeterminados gestionados por el servidor
        movieDTO.id = 0;
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Recogemos los datos del formulario
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtRDate').val();

        // Llamada al endpoint de creación
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, movieDTO, function () {
            // Volvemos a cargar la tabla tras exitoso guardado
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    // Método para actualizar una película existente

    this.Update = function () {
        var movieDTO = {};
        // ID y fechas de control
        movieDTO.id = $('#txtId').val();
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Nuevos valores del formulario
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtRDate').val();

        // Llamada al endpoint de actualización
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, movieDTO, function () {
            // Refrescamos la tabla con los cambios
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    // Método para borrar una película

    this.Delete = function () {
        var movieDTO = {};
        // ID y seguimiento de la entidad
        movieDTO.id = $('#txtId').val();
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Llamada al endpoint de eliminación
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        ca.DeleteToAPI(urlService, movieDTO, function () {
            // Actualizamos la tabla después de borrar
            $('#tblMovies').DataTable().ajax.reload();
        });
    };
}

// Inicializamos la vista cuando el documento está listo
$(document).ready(function () {
    var vc = new MoviesViewController();
    vc.initView();
});

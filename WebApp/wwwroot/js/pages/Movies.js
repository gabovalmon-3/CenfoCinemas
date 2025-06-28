// JavaScript encargado de gestionar toda la l�gica de la p�gina de pel�culas
// Definimos una clase usando prototipos

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    // Funci�n de inicializaci�n

    this.initView = function () {

        console.log("Movie init view --> OK");
        // Cargamos la tabla con los datos de las pel�culas
        this.LoadTable();

        // Configuramos el bot�n Crear para invocar la creaci�n de pel�cula
        $('#btnCreate').click(function () {
            var vc = new MoviesViewController();
            vc.Create();
        });

        // Configuramos el bot�n Actualizar para invocar la modificaci�n de pel�cula
        $('#btnUpdate').click(function () {
            var vc = new MoviesViewController();
            vc.Update();
        });

        // Configuramos el bot�n Eliminar para invocar la eliminaci�n de pel�cula
        $('#btnDelete').click(function () {
            var vc = new MoviesViewController();
            vc.Delete();
        });
    };

    // M�todo que llena la tabla de pel�culas

    this.LoadTable = function () {

        // Endpoint para obtener todas las pel�culas
        // https://localhost:7191/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";
        var urlService = ca.GetUrlApiService(service);

        /*
            Estructura de cabeceras esperada:
            <tr>
                <th>ID</th>
                <th>T�tulo</th>
                <th>Descripci�n</th>
                <th>Fecha de Estreno</th>
                <th>G�nero</th>
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

        // Inicializamos DataTable para mostrar las pel�culas de forma din�mica
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

            // Formateamos la fecha para mostrar solo la parte de d�a
            var datePart = movieDTO.releaseDate.split('T')[0];
            $('#txtRDate').val(datePart);
        });
    };

    // M�todo para enviar al API una nueva pel�cula

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

        // Llamada al endpoint de creaci�n
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, movieDTO, function () {
            // Volvemos a cargar la tabla tras exitoso guardado
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    // M�todo para actualizar una pel�cula existente

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

        // Llamada al endpoint de actualizaci�n
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, movieDTO, function () {
            // Refrescamos la tabla con los cambios
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    // M�todo para borrar una pel�cula

    this.Delete = function () {
        var movieDTO = {};
        // ID y seguimiento de la entidad
        movieDTO.id = $('#txtId').val();
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Llamada al endpoint de eliminaci�n
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        ca.DeleteToAPI(urlService, movieDTO, function () {
            // Actualizamos la tabla despu�s de borrar
            $('#tblMovies').DataTable().ajax.reload();
        });
    };
}

// Inicializamos la vista cuando el documento est� listo
$(document).ready(function () {
    var vc = new MoviesViewController();
    vc.initView();
});

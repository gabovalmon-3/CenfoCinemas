// JS que maneja todo el comportamiento de la página de movies
// Definir una clase JS, usando prototype

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    //Metodo constructor

    this.initView = function () {

        console.log("Movie init view --> Ok");
        // Llamar al método para llenar la tabla de peliculas
        this.LoadTable();
    };

    // Método para llenar la tabla de peliculas

    this.LoadTable = function () {

        // URL del servicio API para obtener las peliculas
        //https://localhost:7216/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /**
            {
                "title": "Coraline y la puerta secreta",
                "description": "Una niña descubre una puerta secreta en su nueva casa y entra a una realidad alterna que la refleja fielmente de muchas formas",
                "releaseDate": "2009-02-06T00:00:00",
                "genre": "Animación",
                "director": "Henry Selick",
                "id": 1,
                "created": "2025-06-14T20:13:50.13",
                "updated": "0001-01-01T00:00:00"
            }

                <tr>
                     <th>Title</th>
                     <th>Description</th>
                     <th>Release Date</th>
                     <th>Genre</th>
                     <th>Director</th>
                </tr>
         */

        var columns = [];
        columns[0] = { 'data': 'title' }
        columns[1] = { 'data': 'description' }
        columns[2] = { 'data': 'releaseDate' }
        columns[3] = { 'data': 'genre' }
        columns[4] = { 'data': 'director' }

        // Invocamos a DataTable para llenar la tabla de peliculas más robusta
        $('#tblMovies').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });
    }
}

$(document).ready(function () {

    // Crear una instancia de la clase MoviesViewController y llamar al método initView
    var vc = new MoviesViewController();
    vc.initView();

})
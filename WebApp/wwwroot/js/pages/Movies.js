// JS que maneja todo el comportamiento de la p�gina de movies
// Definir una clase JS, usando prototype

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    //Metodo constructor

    this.initView = function () {

        console.log("Movie init view --> Ok");
        // Llamar al m�todo para llenar la tabla de peliculas
        this.LoadTable();
    };

    // M�todo para llenar la tabla de peliculas

    this.LoadTable = function () {

        // URL del servicio API para obtener las peliculas
        //https://localhost:7191/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /**

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

        // Invocamos a DataTable para llenar la tabla de peliculas m�s robusta
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

    // Crear una instancia de la clase MoviesViewController y llamar al m�todo initView
    var vc = new MoviesViewController();
    vc.initView();

})
// JS que maneja todo el comportamiento de la página de usuarios
// Definir una clase JS, usando prototype

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    //Metodo constructor

    this.initView = function () {

        console.log("User init view --> Ok");
        // Llamar al método para llenar la tabla de usuarios
        this.LoadTable();
    };

    // Método para llenar la tabla de usuarios

    this.LoadTable = function () {

        // URL del servicio API para obtener los usuarios
        //https://localhost:7216/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /**
            {
            "userCode": "fzunigav",
            "name": "Fabiola Zúñiga",
            "email": "fzunigav@ucenfotec.ac.cr",
            "password": "Test123!",
            "birthDate": "2000-06-08T00:00:00",
            "status": "Active",
            "id": 1,
            "created": "2025-06-22T03:43:31.523",
            "updated": "0001-01-01T00:00:00"
            }

            <tr>
                  <th>Id</th>
                  <th>User Code</th>
                  <th>Name</th>
                  <th>Email</th>
                  <th>Birth Date</th>
                  <th>Status</th>
            </tr>
         */

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birthDate' }
        columns[5] = { 'data': 'status' }

        // Invocamos a DataTable para llenar la tabla de usuarios más robusta
        $('#tblUsers').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });
    }
}

$(document).ready(function () {

    // Crear una instancia de la clase UsersViewController y llamar al método initView
    var vc = new UsersViewController();
    vc.initView();

})
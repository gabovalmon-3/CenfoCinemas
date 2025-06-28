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

        // URL del servicio API para obtener usuarios
        //https://localhost:7191/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /*[
  {
    "userCode": "gvalverde",
    "name": "Gabriel",
    "email": "gvalverdem@ucenfotec.ac.cr",
    "password": "Cenfotec123!",
    "birthDate": "2005-03-28T00:00:00",
    "status": "AC",
    "id": 1,
    "created": "2025-06-28T00:12:02.477",
    "updated": "0001-01-01T00:00:00"
  }
]
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
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

        // Asignar el evento click al botón de crear usuario
        $('#btnCreate').click(function () {
            // Llamar al método para crear un usuario
            var vc = new UsersViewController();
            vc.Create();
        });

        // Asignar el evento click al botón de editar usuario
        $('#btnUpdate').click(function () {
            // Llamar al método para actualizar un usuario
            var vc = new UsersViewController();
            vc.Update();
        });

        // Asignar el evento click al botón de eliminar usuario
        $('#btnDelete').click(function () {
            // Llamar al método para eliminar un usuario
            var vc = new UsersViewController();
            vc.Delete();
        });
    };

    // Método para llenar la tabla de usuarios

    this.LoadTable = function () {

        // URL del servicio API para obtener los usuarios
        //https://localhost:7191/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /* Ejemplo de respuesta JSON:
       [
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

        // Asignar eventos de carga de datos o binding según el click en la tabla

        $('#tblUsers tbody').on('click', 'tr', function () {
            //Extraemos la fila seleccionada
            var row = $(this).closest('tr');
            // Extraemos el DTO, esto nos devuelve el JSON del usuario seleccionado por el usuario
            // Segun la data devuela por el API
            var userDTO = $('#tblUsers').DataTable().row(row).data();
            // Binding con el form
            $('#txtId').val(userDTO.id);
            $('#txtUserCode').val(userDTO.userCode);
            $('#txtName').val(userDTO.name);
            $('#txtEmail').val(userDTO.email);
            $('#txtStatus').val(userDTO.status);

            // La fecha tiene un formato
            var onlyDate = userDTO.birthDate.split('T');
            $('#txtBDate').val(onlyDate[0]);
        })
    }
    // Función para enviar un nuevo usuario al servidor
    this.Create = function () {
        var userDTO = {};
        // Propiedades iniciales definidas por el servidor
        userDTO.id = 0;
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        // Datos recogidos del formulario
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBDate').val();
        userDTO.password = $('#txtPass').val();

        // Invoca al endpoint para crear el usuario
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, userDTO, function () {
            // Refresca la tabla de usuarios después de guardar
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    // Función que envía los cambios de un usuario existente
    this.Update = function () {
        var userDTO = {};
        // Identificador y marcas de tiempo gestionados por el servidor
        userDTO.id = $('#txtId').val();
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        // Valores actualizados del formulario
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBDate').val();
        userDTO.password = $('#txtPass').val();

        // Llama al endpoint de actualización
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, userDTO, function () {
            // Vuelve a cargar la tabla de usuarios tras actualizar
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    // Función para eliminar un usuario existente
    this.Delete = function () {
        var userDTO = {};
        // Identificador y marcas de tiempo gestionados por el servidor
        userDTO.id = $('#txtId').val();
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        // Recogemos los datos del formulario (si fueran necesarios)
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBDate').val();
        userDTO.password = $('#txtPass').val();

        // Llama al endpoint para eliminar el usuario
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        ca.DeleteToAPI(urlService, userDTO, function () {
            // Actualiza la vista tras la eliminación
            $('#tblUsers').DataTable().ajax.reload();
        });
    };

    $(document).ready(function () {
        // Inicializar controlador cuando el DOM esté listo
        var vc = new UsersViewController();
        vc.initView();
    });

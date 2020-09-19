$(document).ready(function () {
    $("#customerDatatable").DataTable({
        "processing": true, // for show progress bar    
        "serverSide": true, // for process server side    
        "filter": true, // this is for disable filter (search box)    
        "orderMulti": false, // for disable multiple column at once    
        "deferRender": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": {
            "url": "/Customers/LoadData",
            "type": "POST",
            "dataType": "json"
        },
        // Columns Setups
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "address", "name": "Address", "autoWidth": true },
            { "data": "neighborhood", "name": "Neighborhood", "autoWidth": true },
            { "data": "city", "name": "City", "autoWidth": true },
            { "data": "phoneNumber", "name": "Phone Number", "autoWidth": true },
            { "data": "status", "name": "Status", "autoWidth": true },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-outline-info" href="/Customers/Edit/' + full.id + '">Editar</a>'; }
            },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-outline-warning" href="/Customers/Details/' + full.id + '">Detalles</a>'; }
            },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-outline-danger" href="/Customers/Delete/' + full.id + '">Eliminar</a>'; }
            }
        ],
    });
});
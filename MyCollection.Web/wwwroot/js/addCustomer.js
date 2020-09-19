    $(document).ready(function() {
        $("#addCustomers").DataTable({
            "processing": true, // for show progress bar    
            "serverSide": true, // for process server side    
            "filter": true, // this is for disable filter (search box)    
            "orderMulti": false, // for disable multiple column at once    
            "deferRender": true,
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
            },
            "ajax": {
                "url": "/Sales/LoadData",
                "type": "POST",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "name": "Id", "autoWidth": true },
                { "data": "name", "name": "Name", "autoWidth": true },
                { "data": "address", "name": "Address", "autoWidth": true },
                { "data": "neighborhood", "name": "Neighborhood", "autoWidth": true },
                { "data": "city", "name": "City", "autoWidth": true },
                { "data": "phoneNumber", "name": "PhoneNumber", "autoWidth": true },
                { "data": "status", "name": "Status", "autoWidth": true },
                {
                    "render": function (data, type, full, meta) { return '<a class="btn btn-outline-success" href="/Sales/AddCustomers/' + full.id + '">Agregar Cliente</a>'; }                    
                }, 
            ]

        });
    });

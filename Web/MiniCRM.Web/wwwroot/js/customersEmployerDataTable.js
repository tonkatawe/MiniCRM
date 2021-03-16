$(document).ready(function () {
    var token = $("#customersTableForm input[name=__RequestVerificationToken]").val();

    var table = $("#customersTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,


        "lengthMenu": [10, 15, 25, 50], "language": {
            "info": "Showing _START_ to _END_ of _TOTAL_ customers",
            "zeroRecords": "No matching customer found",
            "sLengthMenu": "Show _MENU_ customers",
        },

        "ajax": {
            "url": "/employees/customers/getCustomers",
            "type": "POST",
            "datatype": "json",
            "headers": { 'X-CSRF-TOKEN': token }



        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [

            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "fullName", "name": "FirstName", "autoWidth": true },
            { "data": "jobTitleName", "name": "JobTitleName", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true, "orderable": false, "targets": 0 },
            { "data": "phoneNumber", "name": "Phone", "autoWidth": true, "orderable": false, "targets": 0 },
            {
                "data": "ordersCount", "name": "OrdersCount", "autoWidth": true,
                "render": function (data, type, row) {
                    return '<a href="/orders/customer/?customerId=' + row.id + '">' + row.ordersCount + '</a>';

                }
            },
            {
                "orderable": false, "targets": 0,
                "render": function (data, type, row) {
                    return '<a href="/sales/create/?customerId=' + row.id + '" class="btn btn-primary">Add order</a> <a href="/employees/customers/Edit/?customerId=' + row.id + '" class="btn btn-primary">Edit</a>';
                }
            }
        ],
    });

});



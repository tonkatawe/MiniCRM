$(document).ready(function () {
    var token = $("#employeesTableForm input[name=__RequestVerificationToken]").val();

    $("#employeesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/employer",
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
                "data": "customersCount", "name": "CustomersCount", "autoWidth": true,
                "render": function (data, type, row) {
                    return '<a href="/owners/customerManager?employerId=' + row.id + '">' + row.customersCount + '</a>';

                }
            },
            { "data": "salesCount", "name": "SalesCount", "autoWidth": true },
            {
                "orderable": false, "targets": 0,
                "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
            },
        ]
    });
});  
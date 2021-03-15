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
            "url": "/owners/CustomerManager/getCustomers",
            "type": "POST",
            "datatype": "json",
            "data": { "employerId": $("#employerId").val() },
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
                "data": "employerFullName", "name": "EmployerFullName", "autoWidth": true,
                "render": function (data, type, row) {
                    if ($("#employerId").val() && $("#employerName").is(':empty')) {

                        $("#employerName").append("<b>" + row.employerFullName + "</b>" + " " + "customers list");

                        return table.column(5).visible(false);
                    }
                    else {
                        return row.employerFullName;
                    }
                }
            },
            {
                "data": "ordersCount", "name": "OrdersCount", "autoWidth": true,
                "render": function (data, type, row) {
                    return '<a href="/owners/?employerId=' + row.id + '">' + row.ordersCount + '</a>';

                }
            },
            {
                "orderable": false, "targets": 0,
                "render": function (data, type, row) {
                    return '<a href="/owners/employeesManager/DetailsPartial?id=' + row.id + '" id="employerRowAction" class="btn btn-primary">Details</a>';
                }
            }
        ],
    });



    //if ($("#employerId").val()) {
    //    table.column(5).visible(false);
    //    console.log("asdasd");
    //    console.log(test);
    //    console.log(table.rows(0).column(5).data());

    //}

    $('#customersTable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
});



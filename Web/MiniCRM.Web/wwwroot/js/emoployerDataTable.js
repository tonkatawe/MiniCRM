$(document).ready(function () {
    var token = $("#employeesTableForm input[name=__RequestVerificationToken]").val();

    var table = $("#employeesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "lengthMenu": [10, 15, 25, 50],
        "language": {
            "info": "Showing _START_ to _END_ of _TOTAL_ employees",
            "zeroRecords": "No matching employer found",
            "sLengthMenu": "Show _MENU_ employees",
        },
        "ajax": {
            "url": "/owners/employeesManager/getEmployees",
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
                    return '<a href="/owners/customerManager/?employerId=' + row.id + '">' + row.customersCount + '</a>';

                }
            },
            { "data": "salesCount", "name": "SalesCount", "autoWidth": true },
            {
                "orderable": false, "targets": 0,
                "render": function (data, type, row) {
                    return '<a href="/owners/employeesManager/DetailsPartial?id=' + row.id + '" id="employerRowAction" class="btn btn-primary">Details</a>';
                }
            }
        ],
    });

    $('#employeesTable').on("click", "#employerRowAction", function (event) {

        event.preventDefault();

        var url = $(this).attr("href");

        $.get(url, function (data) {
            $('#employerActionsContainer').html(data);

            $('#employerActionsModal').modal('show');
        });

    });

});


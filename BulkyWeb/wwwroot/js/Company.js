var dataTable;
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        ajax: { url: '/admin/company/GetAll' },
        "columns": [
            { data: 'Name', "width": "20%"},
            { data: 'streetAddress', "width": "10%"},
            { data: 'City', "width": "20%" },
            { data: 'State', "width": "20%" },
            { data: 'PhoneNumber', "width": "15%" }, {
                data: 'id',
                "render": function (data)
                {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/company/upsert?id=${data}" class="btn btn-primary  mx-2"><i class="bi bi-pencil-square"></i> Edit </a>
                    <a onClick=Delete('/admin/company/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Edit </a>
                    </div>`
                },
                "width" : "15%"
            }

        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toaster.success(data.message);
                } 
            })
        }
    });
}



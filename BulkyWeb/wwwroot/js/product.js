
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        ajax: { url: '/admin/product/GetAll' },
        "columns": [
            { data: 'title', "width": "20%"},
            { data: 'isbn', "width": "20%"},
            { data: 'listPrice', "width": "20%" },
            { data: 'author', "width": "20%" },
            { data: 'category.id', "width": "20%" },

        ]
    });
}



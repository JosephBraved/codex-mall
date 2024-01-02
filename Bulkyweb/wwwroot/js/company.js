var Datatable
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'streetAddress', "width": "15%" },
            { data: 'city', "width": "10%" },
            { data: 'state', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'postalCode', "width": "20%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/comapny/upsert/?id=${data}" class="btn btn-primary mx-2"><i class="bi-pencil-square"></i> Edit</a>
                    <a OnClick=Delete('/admin/comapany/delete/?id=${data}') class="btn btn-danger mx-2"><i class="bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "35%",
            }
        ]
    });
}
const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
    },
    buttonsStyling: false
});

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
            Swal.fire({
                url: url,
                type: "DELETE",
                success: function (data) {
                    datatable.ajax.reload
                    toastr.success(data.message)
                }
            });
        }
    });
}




$(function () {

    $("#AttendentFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#AttendentFilter div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#AttendentFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/AttendentFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');
    var host_name = "https://localhost:44350";
    var service = hRMapp.attendents.attendent;
    var createModal = new abp.ModalManager(abp.appPath + 'Attendents/Attendent/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Attendents/Attendent/EditModal');
    /*var viewModal = new abp.ModalManager(host_name + '/Attendents/Attendent/ViewModal');*/
    var dataTable = $('#AttendentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        columnDefs: [
            
            {
                title: l('AttendentDate'),
                data: "date",
                render: function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },
            {
                title: l('EmployeeName'),
                data: "employeeId"
            },
            {
                title: l('AttendentMissingIn'),
                data: "missingIn"
            },
            {
                title: l('AttendentMissingOut'),
                data: "missingOut"
            },
            {
                title: l('AttendentAttendentLines'),
                data: "attendentLines"
            },
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Attendent.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Attendent.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            },
        ]
    }));
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    // delete record
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('AttendentDeletionConfirmationMessage',id))
            .then(function(confirmed){
                if(confirmed){
                    service.delete(id)
                        .then(function () {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            dataTable.ajax.reload();
                        });
                }
            });
    });

    dataTable.column([]).visible(false, false);
    
    
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    
    $('#NewAttendentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('input.customcolumn').on('click', function (e) {
        // e.preventDefault();

        // Get the column API object
        var column = dataTable.column($(this).attr('id'));

        // Toggle the visibility
        column.visible(!column.visible());
    });
});

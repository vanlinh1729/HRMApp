$(function () {

    $("#AttendentForMonthFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#AttendentForMonthCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#AttendentForMonthFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/AttendentForMonthFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');
    var host_name = "https://localhost:44350";
    var service = hRMapp.attendents.attendentForMonth;
    var createModal = new abp.ModalManager(abp.appPath + 'Attendents/AttendentForMonth/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Attendents/AttendentForMonth/EditModal');

    var dataTable = $('#AttendentForMonthTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                /*rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('HRMapp.AttendentForMonth.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.AttendentForMonth.Delete'),
                                confirmMessage: function (data) {
                                    return l('AttendentForMonthDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }*/
            },
            {
                title: l('AttendentForMonthEmployeeId'),
                data: "employeeId"
            },
            {
                title: l('AttendentForMonthMonth'),
                data: "month",
                render: function (data, type, full, meta) {
                    return data != null ? moment(data).format("MM-YYYY") : "";
                }
            },
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.AttendentForMonth.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.AttendentForMonth.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            }
        ]
    }));
    
    
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('ContactDeletionConfirmationMessage',id))
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

    $('#NewAttendentForMonthButton').click(function (e) {
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

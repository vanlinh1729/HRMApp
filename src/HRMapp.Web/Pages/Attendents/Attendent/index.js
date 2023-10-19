$(function () {

    $("#AttendentFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#AttendentCollapse div').addClass('col-sm-3').parent().addClass('row');

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

    var service = hRMapp.attendents.attendent;
    var createModal = new abp.ModalManager(abp.appPath + 'Attendents/Attendent/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Attendents/Attendent/EditModal');

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
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('HRMapp.Attendent.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.Attendent.Delete'),
                                confirmMessage: function (data) {
                                    return l('AttendentDeletionConfirmationMessage', data.record.id);
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
                }
            },
            {
                title: l('AttendentDate'),
                data: "date"
            },
            {
                title: l('AttendentEmployeeId'),
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
        ]
    }));

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
});

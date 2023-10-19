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
                rowAction: {
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
                }
            },
            {
                title: l('AttendentForMonthEmployeeId'),
                data: "employeeId"
            },
            {
                title: l('AttendentForMonthMonth'),
                data: "month"
            },
        ]
    }));

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
});

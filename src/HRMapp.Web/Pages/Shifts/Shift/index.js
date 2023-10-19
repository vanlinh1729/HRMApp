$(function () {

    $("#ShiftFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#ShiftCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#ShiftFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/ShiftFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.shifts.shift;
    var createModal = new abp.ModalManager(abp.appPath + 'Shifts/Shift/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Shifts/Shift/EditModal');

    var dataTable = $('#ShiftTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('HRMapp.Shift.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.Shift.Delete'),
                                confirmMessage: function (data) {
                                    return l('ShiftDeletionConfirmationMessage', data.record.id);
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
                title: l('ShiftName'),
                data: "name"
            },
            {
                title: l('ShiftStart'),
                data: "start"
            },
            {
                title: l('ShiftEnd'),
                data: "end"
            },
            {
                title: l('ShiftTimeStartCheckin'),
                data: "timeStartCheckin"
            },
            {
                title: l('ShiftTimeStopCheckout'),
                data: "timeStopCheckout"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewShiftButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

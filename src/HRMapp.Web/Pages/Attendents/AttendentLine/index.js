$(function () {

    $("#AttendentLineFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#AttendentLineCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#AttendentLineFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/AttendentLineFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.attendents.attendentLine;
    var createModal = new abp.ModalManager(abp.appPath + 'Attendents/AttendentLine/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Attendents/AttendentLine/EditModal');

    var dataTable = $('#AttendentLineTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('HRMapp.AttendentLine.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.AttendentLine.Delete'),
                                confirmMessage: function (data) {
                                    return l('AttendentLineDeletionConfirmationMessage', data.record.id);
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
                title: l('AttendentLineAttendentId'),
                data: "attendentId"
            },
            {
                title: l('AttendentLineTimeCheck'),
                data: "timeCheck"
            },
            {
                title: l('AttendentLineType'),
                data: "type"
            },
            {
                title: l('AttendentLineShiftId'),
                data: "shiftId"
            },
            {
                title: l('AttendentLineTimeMissingIn'),
                data: "timeMissingIn"
            },
            {
                title: l('AttendentLineTimeMissingOut'),
                data: "timeMissingOut"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewAttendentLineButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

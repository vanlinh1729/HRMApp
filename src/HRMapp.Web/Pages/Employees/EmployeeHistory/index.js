$(function () {

    $("#EmployeeHistoryFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#EmployeeHistoryCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#EmployeeHistoryFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/EmployeeHistoryFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.employees.employeeHistory;
    var createModal = new abp.ModalManager(abp.appPath + 'Employees/EmployeeHistory/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Employees/EmployeeHistory/EditModal');

    var dataTable = $('#EmployeeHistoryTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('HRMapp.EmployeeHistory.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.EmployeeHistory.Delete'),
                                confirmMessage: function (data) {
                                    return l('EmployeeHistoryDeletionConfirmationMessage', data.record.id);
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
                title: l('EmployeeHistoryStart'),
                data: "start"
            },
            {
                title: l('EmployeeHistoryEnd'),
                data: "end"
            },
            {
                title: l('EmployeeHistoryOrganization'),
                data: "organization"
            },
            {
                title: l('EmployeeHistoryDescription'),
                data: "description"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewEmployeeHistoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

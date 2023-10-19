$(function () {

    $("#ContractFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#ContractCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#ContractFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/ContractFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.contracts.contract;
    var createModal = new abp.ModalManager(abp.appPath + 'Contracts/Contract/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Contracts/Contract/EditModal');

    var dataTable = $('#ContractTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('HRMapp.Contract.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.Contract.Delete'),
                                confirmMessage: function (data) {
                                    return l('ContractDeletionConfirmationMessage', data.record.id);
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
                title: l('ContractEmployeeId'),
                data: "employeeId"
            },
            {
                title: l('ContractTimeContract'),
                data: "timeContract"
            },
            {
                title: l('ContractSignDate'),
                data: "signDate"
            },
            {
                title: l('ContractCoefficientSalary'),
                data: "coefficientSalary"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewContractButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

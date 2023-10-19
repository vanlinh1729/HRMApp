$(function () {

    $("#ContactFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#ContactCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#ContactFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/ContactFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.contacts.contact;
    var createModal = new abp.ModalManager(abp.appPath + 'Contacts/Contact/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Contacts/Contact/EditModal');

    var dataTable = $('#ContactTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('HRMapp.Contact.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('HRMapp.Contact.Delete'),
                                confirmMessage: function (data) {
                                    return l('ContactDeletionConfirmationMessage', data.record.id);
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
                title: l('ContactName'),
                data: "name"
            },
            {
                title: l('ContactGender'),
                data: "gender"
            },
            {
                title: l('ContactBirthDay'),
                data: "birthDay"
            },
            {
                title: l('ContactActive'),
                data: "active"
            },
            {
                title: l('ContactEmail'),
                data: "email"
            },
            {
                title: l('ContactPhoneNumber'),
                data: "phoneNumber"
            },
            {
                title: l('ContactAddress'),
                data: "address"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewContactButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

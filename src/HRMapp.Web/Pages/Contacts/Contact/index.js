$(function () {

    $("#ContactFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#ContactFilter div').addClass('col-sm-3').parent().addClass('row');

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
    var host_name = "https://localhost:44350";
    var createModal = new abp.ModalManager(host_name + '/Contacts/Contact/CreateModal');
    var editModal = new abp.ModalManager(host_name + '/Contacts/Contact/EditModal');
    var viewModal = new abp.ModalManager(host_name + '/Contacts/Contact/ViewModal');

    var dataTable = $('#ContactTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        dom: 'Bfrtilp',
        buttons: [
            'copyHtml5',
            'excelHtml5',
            'pdfHtml5'
        ],
        lengthMenu: [
            [10, 25, 50, 9999999],
            [10, 25, 50, 'All']
        ],
        columnDefs: [
                {
                    title: l('ContactName'),
                    orderable: false,
                    data: "name",
                    render: function(data, type, row){
                        return data ? "<a href='javascript:void(0);' class='ViewContactBtn' data-id='"+row.id+"'  " +
                            "style=\"text-decoration: none\">"+data+"</a>" : "";
                    }
            },


            {
                width: "1%",
                title: l('ContactGender'),
                data: "gender",
                render: function (data, type, row) {
                    return data != null ? l('gender:' + data) : "";
                }
            },


            {
                width: "1%",
                title: l('ContactBirthDay'),
                data: "birthDay",
                render: function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },


            {
                width: "1%",
                title: l('ContactActive'),
                data: "active",
                render: function (data, type, row) {
                    return data != null ? l('active:' + data) : "";
                }
            },

            {
                width: "1%",
                title: l('Email'),
                data: "email",
            },
            {
                width: "1%",
                title: l('PhoneNumber'),
                data: "phoneNumber"
            },

            {
                width: "1%",
                title: l('ContactAddress'),
                data: "address"
            },
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Contact.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Contact.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            }
        ]
    }));

    // edit record
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    // delete record
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

    //visible column
    dataTable.column([]).visible(false, false);


    
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

    $(document).on('click','.ViewContactBtn', function (e) {
        e.preventDefault();
        console.log(e);
        var id = this.dataset.id;
        viewModal.open({id});
    });

    $('input.customcolumn').on('click', function (e) {
        // e.preventDefault();

        // Get the column API object
        var column = dataTable.column($(this).attr('id'));

        // Toggle the visibility
        column.visible(!column.visible());
    });
});

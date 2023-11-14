$(function () {

    $("#ContractFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#ContractFilter div').addClass('col-sm-3').parent().addClass('row');

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
    var host_name = abp.appPath;
    var createModal = new abp.ModalManager(host_name + 'Contracts/Contract/CreateModal');
    var editModal = new abp.ModalManager(host_name + 'Contracts/Contract/EditModal');
    var viewModal = new abp.ModalManager(host_name + 'Contracts/Contract/ViewModal');

    var dataTable = $('#ContractTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        lengthMenu: [
            [10, 25, 50, 9999999],
            [10, 25, 50, 'All']
        ],
        columnDefs: [
            {
                title: l('EmployeeName'),
                orderable: false,
                data: "employeeName",
                render: function(data, type, row){
                    return data ? "<a href='javascript:void(0);' class='ViewContractBtn' data-id='"+row.id+"'  " +
                        "style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },


            {
                width: "1%",
                title: l('TimeContract'),
                data: "timeContract",
                render: function (data, type, row) {
                    return data != null ? l('timeContract:' + data) : "";
                }
            },


            {
                width: "1%",
                title: l('ContractSignDate'),
                data: "signDate",
                render: function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },


            {
                width: "1%",
                title: l('ContractCoefficientSalary'),
                data: "coefficientSalary"
            },

            
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Contract.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Contract.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
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
        abp.message.confirm(l('ContractDeletionConfirmationMessage',id))
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

    $('#NewContractButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click','.ViewContractBtn', function (e) {
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

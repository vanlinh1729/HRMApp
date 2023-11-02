$(function () {

    $("#SalaryFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });
    $('#SalaryFilter div').addClass('col-sm-3').parent().addClass('row');


    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#SalaryCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#SalaryFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/SalaryFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.salarys.salary;
    var host_name = "https://localhost:44350";

    var createModal = new abp.ModalManager(host_name + '/Salarys/Salary/CreateModal');
    var editModal = new abp.ModalManager(host_name + '/Salarys/Salary/EditModal');
    var viewModal = new abp.ModalManager(host_name + '/Salarys/Salary/ViewModal');

    var dataTable = $('#SalaryTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                title: l('EmployeeName'),
                data: "employeeName",
                render: function(data, type, row){
                    return data ? "<a href='javascript:void(0);' class='ViewSalaryBtn' data-id='"+row.id+"'  style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },
            
            {
                width: "1%",
                title: l('AttendentForMonthMonth'),
                data: "attendentForMonthMonth",
                "render": function (data, type, full, meta) {
                    return data != null ? moment(data).format("MM-YYYY") : "";
                }
            },
            {
                width: "1%",
                title: l('TotalSalary'),
                data: "totalSalary"
            },
            {
                className: "dt-center",
                width: "1%",
                title: l('Edit'),
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Salary.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {
                className: "dt-center",
                width: "1%",
                title: l('Delete'),
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Salary.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            },
        ]
    }));


    // edit record
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    // delete record
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('SalaryDeletionConfirmationMessage',id))
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
    dataTable.column([]).visible( false, false );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewSalaryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click','.ViewSalaryBtn', function (e) {
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

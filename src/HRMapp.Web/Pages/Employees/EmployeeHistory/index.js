
$(function () {
    var dateformat = abp.localization.currentCulture.dateTimeFormat.shortDatePattern.toUpperCase();


    $("#EmployeeHistoryFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#EmployeeHistoryFilter div').addClass('col-sm-3').parent().addClass('row');
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
    var host_name = "https://localhost:44350";
    var createModal = new abp.ModalManager(host_name + '/Employees/EmployeeHistory/CreateModal');
    var editModal = new abp.ModalManager(host_name + '/Employees/EmployeeHistory/EditModal');
    var viewModal = new abp.ModalManager(host_name + '/Employees/EmployeeHistory/ViewModal');

    var dataTable = $('#EmployeeHistoryTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                orderable: false,
                data: "employeeName",
                render: function(data, type, row){
                    return data ? "<a href='javascript:void(0);' class='ViewEmployeeHistoryBtn' data-id='"+row.id+"'  " +
                        "style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },


            {
                width: "1%",
                title: l('EmployeeHistoryStart'),
                data: "start",
                "render": function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },
            {
                width: "1%",
                title: l('EmployeeHistoryEnd'),
                data: "end",
                "render": function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },
            {
                width: "1%",
                title: l('EmployeeHistoryJobPosition'),
                data: "jobPosition"
            },
            {
                width: "1%",
                title: l('EmployeeHistoryOrganization'),
                data: "organization"
            },{
                width: "1%",
                title: l('EmployeeHistoryDescription'),
                data: "description"
            },
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.EmployeeHistory.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },
            {    
                width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.EmployeeHistory.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            },
        ]
    }));

    //date range
    $('#EmployeeHistoryFilter_Datetime').daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        /*
                $(this).val(start.format(dateformat) + "-" + end.format(dateformat));
        */
        /*
                dataTable.ajax.reload()
        */
        console.log(start.format(dateformat) + "-" + end.format(dateformat));
    });
    $('#EmployeeHistoryFilter_Datetime').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format(dateformat) + ' - ' + picker.endDate.format(dateformat));
        $(this).trigger("change");
    });
    $('#EmployeeHistoryFilter_Datetime').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $(this).trigger("change");
    });
    
    
    // edit record
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    // delete record
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('EmployeeHistoryDeletionConfirmationMessage',id))
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

    $('#NewEmployeeHistoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $(document).on('click','.ViewEmployeeHistoryBtn', function (e) {
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

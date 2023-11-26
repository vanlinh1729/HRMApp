
$(function () {
    $("#EmployeeFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#EmployeeFilter div').addClass('col-sm-3').parent().addClass('row');

  
    var getFilter = function () {
        var input = {};
        $("#EmployeeFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/EmployeeFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');
    var host_name = abp.appPath;
    var service = hRMapp.employees.employee;
    var createModal = new abp.ModalManager(host_name + 'Employees/Employee/CreateModal');
    var editModal = new abp.ModalManager(host_name + 'Employees/Employee/EditModal');
    var viewModal = new abp.ModalManager(host_name + 'Employees/Employee/ViewModal');

    var dataTable = $('#EmployeeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                data: "name",
                render: function(data, type, row){
                    return data ? "<a href='javascript:void(0);' class='ViewEmployeeBtn' data-id='"+row.id+"'  " +
                        "style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },


            {
                width: "1%",
                title: l('EmployeeOtherName'),
                data: "otherName"
            },


            {
                width: "1%",
                title: l('HrmUserName'),
                data: "userName"
            },


            {
                width: "1%",
                title: l('HrmContactName'),
                data: "contactName"
            },

            {
                width: "1%",
                title: l('Gender'),
                data: "gender",
                render: function (data, type, row) {
                    return data != null ? l('gender:' + data) : "";
                }
            },
            {
                width: "1%",
                title: l('BirthDay'),
                data: "birthDay",
                "render": function (data, type, full, meta) {
                    return data != null ? moment(data).format("DD-MM-YYYY") : "";
                }
            },
            {
                width: "1%",
                title: l('PhoneNumber'),
                data: "phoneNumber"
            },

            {
                width: "1%",
                title: l('DepartmentName'),
                data: "departmentName"
            },


            {
                width: "1%",
                title: l('EmployeeStatus'),
                data: "status",
                render: function (data, type, row){
                    return l('EmployeeStatus:'+data)
                }
            },
            
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Employee.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Employee.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
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
        abp.message.confirm(l('EmployeeDeletionConfirmationMessage',id))
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

    $('#NewEmployeeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click','.ViewEmployeeBtn', function (e) {
        e.preventDefault();
        console.log(e);
        var id = this.dataset.id;
        viewModal.open({id});
    });
    
    viewModal.onOpen(function () {
        console.log("ab123c da mo modal");
        $('#exportPdfButton').on('click', function () {
            var element = $(".modal-body").html();
            console.log("danhannut");
            var opt = {
                margin: 10,
                filename: 'CV'+jQuery.now()+'.pdf',
                image: {type: 'jpeg', quality: 1},
                html2canvas: {scale: 2},
                jsPDF: {unit: 'mm', format: 'a4', orientation: 'landscape'}
            };
            html2pdf().set(opt).from(element).save();
        });
    });
    $('input.customcolumn').on('click', function (e) {
        // e.preventDefault();

        // Get the column API object
        var column = dataTable.column($(this).attr('id'));

        // Toggle the visibility
        column.visible(!column.visible());
    });
});

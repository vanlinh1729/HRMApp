function loadsetEmployee() {
    var setEmployee = new Set();
    for (let index = 0; index < $("[name = 'ViewModel.employeeId']").length ; index ++)
    {
        setEmployee.add($("[name = 'ViewModel.employeeId']")[index].value);
    }
    return setEmployee;
}
$(function () {

    $("#DepartmentFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#DepartmentFilter div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#DepartmentFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/DepartmentFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');
    var DepartmentId = '';
    var host_name = abp.appPath;
    var service = hRMapp.departments.department;
    var createModal = new abp.ModalManager(host_name + 'Departments/Department/CreateModal');
    var editModal = new abp.ModalManager(host_name + 'Departments/Department/EditModal');
    var editOwnerModal = new abp.ModalManager(host_name + 'Departments/Department/EditOwnerModal');
    var viewModal = new abp.ModalManager(host_name + 'Departments/Department/ViewModal');
    var updateEmployeeModal = new abp.ModalManager(host_name + 'Departments/Department/UpdateEmployee');
    var dataTable = $('#DepartmentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, getFilter),
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
                title: l('DepartmentName'),
                data: "name",
                className: 'row-border',
                render: function (data, type, row) {
                    return data ? "<a href='javascript:void(0);' class='ViewDepartmentBtn' data-id='" + row.id + "'  style=\"text-decoration: none\">" + data + "</a>" : "";
                }
            },
            {
                title: l('Employee'), data: "count",
                render: function (data, type, row) {
                    var count = data ? data : 0;
                    return  "<a href='javascript:void(0);' class='UpdateEmployee' data-id='" + row.id + "'  style=\"text-decoration: none\"><i class=\"fa fa-users\"></i>" +"("+data+")" + "</a>" ;

                },
                className: "dt-center",
                width: "1%"
            },

            {
                title: l('ownerName'), data: "ownerName",
                // render: function (data, type, row) {
                //     return data ? "<a href='javascript:void(0);' class='EditOwnerBtn' data-id='" + row.id + "'  style=\"text-decoration: none\">" + data + "</a>" : "<a href='javascript:void(0);' class='EditOwnerBtn' data-id='" + row.id + "'  style=\"text-decoration: none\">Thêm trưởng phòng</a>";
                //
                // },
                width: "1%"
            },
            {
                title: l('parentName'),
                data: "parentName",
                width: "1%"
            },
            {
                width: "1%",
                title: l('Edit'),
                orderable: false,
                className: "dt-center",
                render: function (data, type, row) {
                    return abp.auth.isGranted('HRMapp.Department.Update') ? ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>` : "";
                }
            },
            {
                width: "1%",
                orderable: false,
                className: "dt-center",
                title: l('Delete'),
                render: function (data, type, row) {
                    return abp.auth.isGranted('HRMapp.Department.Delete') ? ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>` : "";
                }
            },]
    }));
    // edit record
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    // delete record
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('DepartmentDeletionConfirmationMessage', id))
            .then(function (confirmed) {
                if (confirmed) {
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

    createModal.onOpen(function () {
        console.log('opened the modal...');
        var l = abp.localization.getResource('HRMapp');
        var employeetable = $('#DepartmentUser1').DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: false,
            paging: true,
            searching: true,//disable default searchbox
            targets: 0,
            autoWidth: true,
            scrollCollapse: true,
            order: [[0, "asc"]],
            lengthMenu: [
                [10, 25, 50, 9999999],
                [10, 25, 50, 'All']
            ],
            ajax: abp.libs.datatables.createAjax(service.getListUsersDepartmentEdit, {employeeName: $('#DepartmentUser1_filter > label > input').val()}),
            columnDefs: [{
                title: l('EmployeeName'), data: "employeeName",
            }, {
                title: l('PhoneNumber'), data: "phoneNumber",
            }, {
                title: "Email", data: "email",
            }, {
                width: "1%",
                orderable: false,
                className: "dt-center",
                title: "Chọn", data: "id", render: function (data, type, row) {
                    console.log(row)
                    return "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"><i class=\"fa fa-check-circle\"></i></a>"
                }
            },]


        }));

    });

    editModal.onOpen(function () {
        console.log('opened the modal...');
        var l = abp.localization.getResource('HRMapp');
        var employeetable = $('#DepartmentUser1').DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: false,
            paging: true,
            searching: true,//disable default searchbox
            targets: 0,
            autoWidth: true,
            scrollCollapse: true,
            order: [[0, "asc"]],
            lengthMenu: [
                [10, 25, 50, 9999999],
                [10, 25, 50, 'All']
            ],
            ajax: abp.libs.datatables.createAjax(service.getListUsersDepartmentEdit, {employeeName: $('#DepartmentUser1_filter > label > input').val()}),
            columnDefs: [{
                title: l('EmployeeName'), data: "employeeName",
            }, {
                title: l('PhoneNumber'), data: "phoneNumber",
            }, {
                title: "Email", data: "email",
            }, {
                width: "1%",
                orderable: false,
                className: "dt-center",
                title: "Chọn", data: "id", render: function (data, type, row) {
                    console.log(row)
                    var checked =
                        loadsetEmployee().has(row.id)? "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"></a>"

                            :                     "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"><i class=\"fa fa-check-circle\"></i></a>";;

                    return   checked
                }
            },]


        }));

    });

    updateEmployeeModal.onOpen(function () {
        console.log('opened the modal...');
        var l = abp.localization.getResource('HRMapp');
        var employeetable = $('#DepartmentUser1').DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: false,
            paging: true,
            searching: true,//disable default searchbox
            targets: 0,
            autoWidth: true,
            scrollCollapse: true,
            order: [[0, "asc"]],
            lengthMenu: [
                [10, 25, 50, 9999999],
                [10, 25, 50, 'All']
            ],
            ajax: abp.libs.datatables.createAjax(service.getListUsersDepartmentEdit, {employeeName: $('#DepartmentUser1_filter > label > input').val()}),
            columnDefs: [{
                title: l('EmployeeName'), data: "employeeName",
            }, {
                title: l('PhoneNumber'), data: "phoneNumber",
            }, {
                title: "Email", data: "email",
            }, {
                width: "1%",
                orderable: false,
                className: "dt-center",
                title: "Chọn", data: "id", render: function (data, type, row) {
                    console.log(row)
                    var checked =
                        loadsetEmployee().has(row.id)? "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"></a>"

                            :                     "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"><i class=\"fa fa-check-circle\"></i></a>";;

                    return   checked
                }
            },]


        }));

    });


    createModal.onResult(function () {
        /* const text = $("#nhanVienPhongBanInput").val();
         console.log(text)*/
        dataTable.ajax.reload();
        abp.notify.success(l('SuccessfullyCreateDepartment!'));
    });
    updateEmployeeModal.onResult(function () {
        dataTable.ajax.reload();
    });


    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    editOwnerModal.onResult(function () {
        dataTable.ajax.reload();
        abp.notify.success(l('SuccessfullyEditOwner!'));
    });
    $('#NewDepartmentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click', '.ViewDepartmentBtn', function (e) {
        e.preventDefault();
        console.log(e);
        var id = this.dataset.id;
        viewModal.open({id});
    });
    $(document).on('click', '.EditOwnerBtn', function (e) {
        e.preventDefault();
        console.log(e);
        var id = this.dataset.id;
        editOwnerModal.open({id});
    });
    $(document).on('click', '.UpdateEmployee', function (e) {

        e.preventDefault();
        console.log(e);
        var id = this.dataset.id;
        updateEmployeeModal.open({id})
    });

    $('input.customcolumn').on('click', function (e) {
        // e.preventDefault();

        // Get the column API object
        var column = dataTable.column($(this).attr('id'));

        // Toggle the visibility
        column.visible(!column.visible());
    });


    viewModal.onOpen(function () {
        console.log('opened the modal...');
        var l = abp.localization.getResource('HRMapp');
        var employeeInDepartment = $('#DepartmentUser').DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,//disable default searchbox
            autoWidth: false,
            scrollCollapse: true,
            order: [[0, "asc"]],
            lengthMenu: [
                [10, 25, 50, 9999999],
                [10, 25, 50, 'All']
            ],
            ajax: abp.libs.datatables.createAjax(service.getListUsersDepartment, {id: $('#departmenId').val()}),
            columnDefs: [{
                title: l('EmployeeName'), data: "employeeName",
            }, {
                title: l('PhoneNumber'), data: "phoneNumber",
            }, {
                title: "Email", data: "email",
            },{
                title: "EmployeePosition", data: "employeePosition",
                render: function (data, type, row) {
                    return data != null ? l('employeePosition:' + data) : "";
                }
            }]


        }));
        // var employeetable = $('#DepartmentUser1').DataTable(abp.libs.datatables.normalizeConfiguration({
        //     processing: true,
        //     serverSide: true,
        //     paging: true,
        //     searching: false,//disable default searchbox
        //     autoWidth: true,
        //     scrollCollapse: true,
        //     order: [[0, "asc"]],
        //     ajax: abp.libs.datatables.createAjax(service.getListUsersDepartmentEdit, {id: $('#departmenId').val()}),
        //     columnDefs: [{
        //         title: l('EmployeeName'), data: "employeeName",
        //     }, {
        //         title: l('PhoneNumber'), data: "phoneNumber",
        //     }, {
        //         title: "Email", data: "email",
        //     }, {
        //         width: "1%",
        //         className: "dt-center "+row.id,
        //         orderable: false,
        //         title: "Chọn", data: "id", render: function (data, type, row) {
        //             console.log(row)
        //             return "<a class='selectToDepartment' data-name='" + row.employeeName + "'  data-id='" + row.id + "' style=\"text-decoration: none\"><i class=\"fa fa-check-circle\"></i></a>"
        //         }
        //     },]
        //
        //
        // }));

        $('#exportPdfButton').on('click', function () {
            var element = $(".modal-body").html();
            console.log("danhannut");
            var opt = {
                margin: 10,
                filename: 'DepartmentInfo'+jQuery.now()+'.pdf',
                image: {type: 'jpeg', quality: 1},
                html2canvas: {scale: 2},
                jsPDF: {unit: 'mm', format: 'a4', orientation: 'landscape'}
            };
            html2pdf().set(opt).from(element).save();
        });
    });

    $(document).on('click', '.deleteemployeebtn', function (e) {
        e.preventDefault();
        var id = this.dataset.id;
        var name = this.dataset.name;
        var element = document.getElementById(id)
        var deletebutton = document.getElementById("ViewModel_employeeId+"+id);

        /* var xmlString = `<tr id='${id}' >\n" +
             "                                        <td> ${name} </td>\n
             
             " +
             "                                        <td style=\"text-align: center\"><a class=\"deleteemployeebtn\" href=\"javascript:void(0);\" data-name=${name} data-id=${id}><i class=\"text-danger fa fa-trash\"></i></a></td>\n" +
             "                                    </tr>`;*/

        var selectStr = `<i class="fa fa-check-circle"></i>`;
        element.remove();
        $(`[data-id=${id}]`).append(selectStr);
        deletebutton.remove();
        // xoa nhan vien ra khoi phong ban  
        /*$.ajax(url, {
            type: 'PUT',
            data: JSON.stringify(data),
            contentType: 'application/json',
            processData: false,
            success: function (data, status, xhr) {
                console.log(data)
                element.remove();
                dataTable.ajax.reload();
                employeetable.ajax.reload();
                employeeInDepartment.ajax.reload();
                /!*
                                    abp.notify.success(l("SuccessDeleted"));
                *!/
            },
            error: function (jqXhr, textStatus, errorMessage) {
                console.log(errorMessage);
                /!*abp.notify.error(l('ErrorDelete'));*!/
            }
        });*/
    });

    $(document).on('click', '.selectToDepartment', function (e) {
        e.preventDefault();
        var id = this.dataset.id;
        var name = this.dataset.name;
        var xmlString = `<tr id='${id}' >\n" +
            "                                        <td> ${name} </td>\n
            
            " +
            "                                        <td style=\"text-align: center\"><a class=\"deleteemployeebtn\" href=\"javascript:void(0);\" data-name=${name} data-id=${id}><i class=\"text-danger fa fa-trash\"></i></a></td>\n" +
            "                                    </tr>`;
        var elementClick = $(this);
        var inputStr =`<input type="text" id="ViewModel_employeeId+${id}" name="ViewModel.employeeId" value="${id}" class="form-control form-control-sm" hidden="hidden">`;
        elementClick.children().remove()
        $("#employeelistview > tbody").append(xmlString);
        $("#employeelistview").parent().append(inputStr);

    });



});

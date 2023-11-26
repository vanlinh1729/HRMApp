$(function () {

    var date;
    $("#AttendentForMonthFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    $('#AttendentForMonthFilter div').addClass('col-sm-3').parent().addClass('row');

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#AttendentForMonthCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#AttendentForMonthFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/AttendentForMonthFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');
    var host_name = abp.appPath;
    var service = hRMapp.attendentForMonths.attendentForMonth;
    var createModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/CreateModal');
    var createManyModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/CreateManyAttendenForMonthModal');
    var editModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/EditModal');
    var viewModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ViewModal');
    var exportAll = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ExportAllAttendentForMonthModal');
    var viewAllModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ViewAllAttendentForMonthModal');

    var dataTable = $('#AttendentForMonthTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                    return data ? "<a href='javascript:void(0);' class='ViewAttendentForMonthBtn' data-id='"+row.id+"'  " +
                        "style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },


            {
                width: "1%",
                title: l('Month'),
                data: "month",
                "render": function (data, type, full, meta) {
                    return data != null ? moment(data).format("MM-YYYY") : "";
                }
            },


            {
                width: "1%",
                title: l('Count'),
                data: "count"
            },
            {    width: "1%",
                title: l('Edit'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.AttendentForMonth.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {    width: "1%",
                title: l('Delete'),
                className: "dt-center",
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.AttendentForMonth.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
                }
            }
        ]
    }));
    
    
    $(document).on('click', '.edit-button', function (e) {
        editModal.open({id: this.dataset.id});
    });
    
    $(document).on('click', '.delete-button', function (e) {
        var id = this.dataset.id;
        abp.message.confirm(l('AttendentForMonthDeletionConfirmationMessage',id))
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

   
    
    dataTable.column([]).visible(false, false);
    
    
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    createManyModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    }); 
    exportAll.onResult(function (e) {
        e.preventDefault();
        console.log("abc");
        date = $("#ViewMonthModel_Month").val();
        // Gọi AJAX để lấy dữ liệu từ server
        viewAllModal.open();
        

    });

    function fillModalWithData(data) {
        $('#viewAttendentforMonthBody').empty(); // Xóa dữ liệu cũ trong bảng

        // Duyệt qua danh sách đối tượng và thêm dữ liệu vào bảng trong modal
        for (var i = 0; i < data.listAttendentForMonth.length; i++) {
            var item = data.listAttendentForMonth[i];
            var departmentNames = item.departmentName != null ? item.departmentName : ''
            var month = moment(item.month).format("MM-YYYY")
            var row = '<tr>' +
                '<td>' + item.employeeName + '</td>' +
                '<td>' + departmentNames + '</td>' +
                '<td>' + month + '</td>' +
                '<td>' + item.count + '</td>' +
                '</tr>';

            $('#viewAttendentforMonthBody').append(row);
        }

        // Cập nhật thông tin về tháng lương trong phần tử có ID là #SalaryMonth (ví dụ: lấy thông tin từ phần tử đầu tiên trong danh sách)
        if (data.listAttendentForMonth.length > 0) {
            $("#AttendentMonth").text("Chấm công tháng "+moment(data.listAttendentForMonth[0].month).format("MM-YYYY"));
        } else {
            // Nếu không có dữ liệu, có thể cập nhật thông báo hoặc giá trị mặc định
            $("#AttendentMonth").text("Không có dữ liệu cho tháng này");
        }
    }

    $('#NewAttendentForMonthButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $('#NewManyAttendentForMonthButton').click(function (e) {
        e.preventDefault();
        createManyModal.open();
    }); 
    $('#CreateAllAttendentForMonthInMonthButton').click(function (e) {
        e.preventDefault();
        exportAll.open();
    });

    $(document).on('click','.ViewAttendentForMonthBtn', function (e) {
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
                filename: 'ChamCongThang'+jQuery.now()+'.pdf',
                image: {type: 'jpeg', quality: 1},
                html2canvas: {scale: 2},
                jsPDF: {unit: 'mm', format: 'a4', orientation: 'landscape'}
            };
            html2pdf().set(opt).from(element).save();
        });
    });
    viewAllModal.onOpen(function () {
        $.ajax({
            url: '/api/app/attendent-for-month/many-attendent-for-month',
            method: 'GET',
            data: {  Month: date  },
            success:  function(data) {
                console.log("Received data:", data);

                if (data) {
                    console.log("Data is truthy. Filling modal...");
                    fillModalWithData(data);
                } else {
                    console.log("Data is falsy. Cannot fill modal.");
                }
            },
            error: function(error) {
                console.log(error);
            }
        });
        console.log("ab123c da mo modal");
        $('#exportAttendentForMonthPdfButton').on('click', function () {
            var element = $(".modal-body").html();
            console.log("danhannut");
            var opt = {
                margin: 10,
                filename: 'ChamCongThang'+jQuery.now()+'.pdf',
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

$(function () {

    var date,dateD, departmentName;
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
    var exportAllForDepartment = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ExportAllAttendentForMonthForDepartmentModal');
    var viewAllModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ViewAllAttendentForMonthModal');
    var viewAllForDepartmentModal = new abp.ModalManager(host_name + 'Attendents/AttendentForMonth/ViewAllAttendentForMonthForDepartmentModal');

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

    
    function fillModalWithData(apiData) {
        generateTableBody(apiData) ;


        if (apiData.length >0) 
        {
            $("#AttendentMonth").text("Chấm công tháng "+moment(apiData[0].attendentLines[0].timeCheck).format("MM-YYYY"));
        } 
        else 
        {
            $("#AttendentMonth").text("Không có dữ liệu cho tháng này");
        }
    }
    
    function fillModalWithDatas(apiData) {
        generateTableBodys(apiData) ;


        if (apiData.length >0) 
        {
            $("#AttendentMonthDepartment").text("Chấm công tháng "+moment(apiData[0].attendentLines[0].timeCheck).format("MM-YYYY"));
        } 
        else 
        {
            $("#AttendentMonthDepartment").text("Không có dữ liệu cho tháng này");
        }
    }
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
    exportAllForDepartment.onResult(function (e) {
        e.preventDefault();
        console.log("abc");
        dateD = $("#ViewMonthAndDepartmentModel_Month").val();
        departmentName = $("#ViewMonthAndDepartmentModel_DepartmentName").val();
        // Gọi AJAX để lấy dữ liệu từ server
        viewAllForDepartmentModal.open();

    });

    function generateTableBody(data) {
        const table = $("#attendentForMonthDetailTable");

        // Find the maximum dayInMonth from all entries
        const maxDays = Math.max(...data.flatMap(entry => entry.dayCheckDtos.map(dayCheck => dayCheck.dayInMonth)));

        // Create header row
        const headers = ["Tên Nhân Viên", "Tên Phòng Ban", "Chức vụ", ...Array.from({ length: maxDays }, (_, i) => i + 1),"Tổng cộng","Nghỉ có phép", "Nghỉ không lương", "Nghỉ lễ"]; // Dynamic days
        const headerRow = $("<tr></tr>");

        headers.forEach(headerText => {
            const th = $("<th></th>").text(headerText);
            headerRow.append(th);
        });

        table.append(headerRow);

        // Create data rows
        data.forEach(entry => {
            const row = $("<tr></tr>");

            // Add employee name and department name
            const cellEmployeeName = $("<td></td>").text(entry.employeeName);
            row.append(cellEmployeeName);

            const cellDepartmentName = $("<td></td>").text(entry.departmentName);
            row.append(cellDepartmentName);
 
            const cellEmployeePosition = $("<td></td>").text(l('employeePosition:' + entry.employeePosition));
            row.append(cellEmployeePosition);

            // Add attendance data
            const attendanceData = Array.from({ length: maxDays }, (_, i) => i + 1).map(day => {
                const dayCheck = entry.dayCheckDtos.find(dayCheck => dayCheck.dayInMonth === day);
                return dayCheck ? dayCheck.totalInDay : 0;
            });

            attendanceData.forEach(totalInDay => {
                const cell = $("<td></td>").text(totalInDay);

                // Customize the cell based on attendance status (e.g., apply styling)
                row.append(cell);
            });
            const cellTotal = $("<td></td>").text(entry.countAtt);
            row.append(cellTotal);
            const timeoff1 = $("<td></td>").text(" ");
            row.append(timeoff1);
            const timeoff2 = $("<td></td>").text(" ");
            row.append(timeoff2);
            const timeoff3 = $("<td></td>").text(" ");
            row.append(timeoff3);


            table.append(row);
        });
    }
    
function generateTableBodys(data) {
        const table = $("#attendentForMonthDetailForDepartmentTable");

        // Find the maximum dayInMonth from all entries
        const maxDays = Math.max(...data.flatMap(entry => entry.dayCheckDtos.map(dayCheck => dayCheck.dayInMonth)));

        // Create header row
        const headers = ["Tên Nhân Viên", "Tên Phòng Ban", "Chức vụ", ...Array.from({ length: maxDays }, (_, i) => i + 1),"Tổng cộng","Nghỉ có phép", "Nghỉ không lương", "Nghỉ lễ"]; // Dynamic days
        const headerRow = $("<tr></tr>");

        headers.forEach(headerText => {
            const th = $("<th></th>").text(headerText);
            headerRow.append(th);
        });

        table.append(headerRow);

        // Create data rows
        data.forEach(entry => {
            const row = $("<tr></tr>");

            // Add employee name and department name
            const cellEmployeeName = $("<td></td>").text(entry.employeeName);
            row.append(cellEmployeeName);

            const cellDepartmentName = $("<td></td>").text(entry.departmentName);
            row.append(cellDepartmentName);
 
            const cellEmployeePosition = $("<td></td>").text(l('employeePosition:' + entry.employeePosition));
            row.append(cellEmployeePosition);

            // Add attendance data
            const attendanceData = Array.from({ length: maxDays }, (_, i) => i + 1).map(day => {
                const dayCheck = entry.dayCheckDtos.find(dayCheck => dayCheck.dayInMonth === day);
                return dayCheck ? dayCheck.totalInDay : 0;
            });

            attendanceData.forEach(totalInDay => {
                const cell = $("<td></td>").text(totalInDay);

                // Customize the cell based on attendance status (e.g., apply styling)
                row.append(cell);
            });
            const cellTotal = $("<td></td>").text(entry.countAtt);
            row.append(cellTotal);

            const timeoff1 = $("<td></td>").text(" ");
            row.append(timeoff1);
            const timeoff2 = $("<td></td>").text(" ");
            row.append(timeoff2);
            const timeoff3 = $("<td></td>").text(" ");
            row.append(timeoff3);
            

            table.append(row);
        });
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
    $('#CreateAllAttendentForMonthForDepartmentButton').click(function (e) {
        e.preventDefault();
        exportAllForDepartment.open();
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
            url:'/api/app/attendent-for-month/attendent-for-month-detail',
            method: 'GET',
            data: {  Date: date  },
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
            const {width,height} = document.body.getBoundingClientRect();
            var opt = {
                margin: 10,
                onePage:true,
                filename: 'ChamCongThang'+jQuery.now()+'.pdf',
                image: {type: 'jpeg', quality: 1},
                html2canvas: {scale: 1},
                jsPDF: {unit: 'mm', format: 'a2', orientation: 'landscape'}
            };
            html2pdf().set(opt).from(element).save();
        });
    });
    
    viewAllForDepartmentModal.onOpen(function () {
        $.ajax({
            url:'/api/app/attendent-for-month/attendent-for-month-for-department-detail',
            method: 'GET',
            data: {  Date: dateD, DepartmentName : departmentName  },
            success:  function(data) {
                console.log("Received data:", data);

                if (data) {
                    console.log("Data is truthy. Filling modal...");
                    fillModalWithDatas(data);
                } else {
                    console.log("Data is falsy. Cannot fill modal.");
                }
            },
            error: function(error) {
                console.log(error);
            }
        });
        console.log("ab123c da mo modal");
        $('#exportAttendentForMonthForDepartmentPdfButton').on('click', function () {
            var element = $(".modal-body").html();
            console.log("danhannut");
            const {width,height} = document.body.getBoundingClientRect();
            var opt = {
                margin: 10,
                onePage:true,
                filename: 'ChamCongThang'+jQuery.now()+'.pdf',
                image: {type: 'jpeg', quality: 1},
                html2canvas: {scale: 1},
                jsPDF: {unit: 'mm', format: 'a2', orientation: 'landscape'}
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

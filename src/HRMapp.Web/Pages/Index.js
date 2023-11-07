$(function () {
    abp.log.debug('Index.js initialized!');

    $.ajax({
        url: 'api/app/department/department-count',
        method: 'POST',
        success: function (data) {
            $('#departmentCount').text(data);
        },
        error: function () {
            $('#departmentCount').text('Không thể lấy dữ liệu');
        }
    });
    $.ajax({
        url: 'api/app/contract/contract-count',
        method: 'POST',
        success: function (data) {
            $('#contractCount').text(data);
        },
        error: function () {
            $('#contractCount').text('Không thể lấy dữ liệu');
        }
    });
    $.ajax({
        url: 'api/app/contact/contact-count',
        method: 'POST',
        success: function (data) {
            $('#contactCount').text(data);
        },
        error: function () {
            $('#contactCount').text('Không thể lấy dữ liệu');
        }
    });
    $.ajax({
        url: 'api/app/employee/employee-count',
        method: 'POST',
        success: function (data) {
            $('#employeeCount').text(data);
        },
        error: function () {
            $('#employeeCount').text('Không thể lấy dữ liệu');
        }
    });
    
});

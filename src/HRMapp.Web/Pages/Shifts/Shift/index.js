$(function () {

    $("#ShiftFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });
    $('#ShiftFilter div').addClass('col-sm-3').parent().addClass('row');


    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#ShiftCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#ShiftFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/ShiftFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('HRMapp');

    var service = hRMapp.shifts.shift;
    var host_name = abp.appPath;
    var createModal = new abp.ModalManager(host_name + 'Shifts/Shift/CreateModal');
    var editModal = new abp.ModalManager(host_name + 'Shifts/Shift/EditModal');
    var viewModal = new abp.ModalManager(host_name + 'Shifts/Shift/ViewModal');

    var dataTable = $('#ShiftTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                title: l('ShiftName'),
                data: "name",
                render: function(data, type, row){
                    return data ? "<a href='javascript:void(0);' class='ViewShiftBtn' data-id='"+row.id+"'  style=\"text-decoration: none\">"+data+"</a>" : "";
                }
            },


            {
                width: "1%",
                title: l('Start'),
                data: "start"
            },


            {
                width: "1%",
                title: l('End'),
                data: "end"
            },
            {
                width: "1%",
                title: l('TimeStartCheckin'),
                data: "timeStartCheckin"
            },
            {
                width: "1%",
                title: l('TimeStopCheckout'),
                data: "timeStopCheckout"
            },
            {
                className: "dt-center",
                width: "1%",
                title: l('Edit'),
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Shift.Update') ?  ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>`: "" ;
                }
            },

            {
                className: "dt-center",
                width: "1%",
                title: l('Delete'),
                orderable: false,
                render: function (data,type,row) {
                    return abp.auth.isGranted('HRMapp.Shift.Delete') ?  ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>`: "" ;
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
        abp.message.confirm(l('ShiftDeletionConfirmationMessage',id))
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

    $('#NewShiftButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click','.ViewShiftBtn', function (e) {
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



/*
$('input[name="starttime"]').daterangepicker({
    singleTimePicker: true,
    timePicker24Hour: true,
    timePickerIncrement: 1,
    timePickerSeconds: true,
    locale: {
        format: 'HH:mm:ss'
    }
}).on('show.daterangepicker', function (ev, picker) {
    picker.container.find(".calendar-table").hide();
});*/

var dataTable;
var dateformat = abp.localization.currentCulture.dateTimeFormat.shortDatePattern.toUpperCase();
var l = abp.localization.getResource('HRMapp');
var service = hRMapp.attendents.attendent;
var host_name = abp.appPath;

var createModal = new abp.ModalManager(host_name + 'Attendents/Attendent/CreateModal');
var editModal = new abp.ModalManager(host_name + 'Attendents/Attendent/EditModal');
var viewModal = new abp.ModalManager(host_name + 'Attendents/Attendent/ViewModal');

$(function () {


    $("#AttendentFilter :input").change(function () {
        dataTable.ajax.reload();
    });
    $('#AttendentFilter div').addClass('col-sm-3').parent().addClass('row');


    var getFilter = function () {
        var input = {};
        $("#AttendentFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/AttendentFilter./g, ''))] = data.value;
                }
            })
        return input;
    };


    async function ColumnAttendentLine() {
        await fetch(abp.appPath + "api/app/shift")
            .then((response) => response.json())
            .then((data) => {
                    /*console.log(data);*/
                    /* data.items.forEach(addcolumn)*/
                    /*  console.log(Column);*/
                    GetDatatable(data.items);
                }
            );
    }

    ColumnAttendentLine();


    function columnnew(list) {
        result = [
            {
                title: l('Date'),
                data: "date",
                "render": function (data, type, full, meta) {
                    return moment(data).format("DD-MM-YYYY");
                }

            },


            {
                width: "1%",
                title: l('EmployeeName'),
                data: "employeeName"
            },


           /* {
                width: "1%",
                title: l('MissingIn'),
                data: "missingIn"
            },


            {
                width: "1%",
                title: l('MissingOut'),
                data: "missingOut"
            }*/
        ];
        // for (const item in list) {
        //     result.push({
        //
        //         width: "1%",
        //         title: l('CheckIn'),
        //         /*
        //                          data: "detail["+'['+list[item].code+']'+']',
        //         */
        //         orderable: false,
        //         render: function (data, type, row) {
        //             return row.attendentLines?.[item]?.['timeCheck'] ? moment(row.attendentLines?.[item]?.['timeCheck']).format("HH:mm:ss") : "";
        //         }
        //     });
        //     result.push({
        //         width: "1%",
        //         title: l('CheckOut'),
        //         /*
        //                          data: "detail["+'['+list[item].code+']'+']',
        //         */
        //         orderable: false,
        //         render: function (data, type, row) {
        //             return row.attendentLines?.[1 + item]?.['timeCheck'] ? moment(row.attendentLines?.[1 + item]?.['timeCheck']).format("HH:mm:ss") : "";
        //
        //         }
        //     });
        // }


        for (let i = 0; i < list.length; i+=3) {
            result.push({
                width: "1%",
                title: l('CheckIn'),
                orderable: false,
                render: function (data, type, row) {
                    return row.attendentLines?.[0]?.['timeCheck'] ? moment(row.attendentLines?.[0]?.['timeCheck']).format("HH:mm:ss") : "";
                }
            });
            result.push({
                width: "1%",
                title: l('CheckOut'),
                orderable: false,
                render: function (data, type, row) {
                    return row.attendentLines?.[1]?.['timeCheck'] ? moment(row.attendentLines?.[1]?.['timeCheck']).format("HH:mm:ss") : "";
                }
            });
            result.push({
                width: "1%",
                title: l('CheckIn'),
                orderable: false,
                render: function (data, type, row) {
                    return row.attendentLines?.[2]?.['timeCheck'] ? moment(row.attendentLines?.[2]?.['timeCheck']).format("HH:mm:ss") : "";
                }
            });
            result.push({
                width: "1%",
                title: l('CheckOut'),
                orderable: false,
                render: function (data, type, row) {
                    return row.attendentLines?.[3]?.['timeCheck'] ? moment(row.attendentLines?.[3]?.['timeCheck']).format("HH:mm:ss") : "";
                }
            });
        }
        result.push({

            width: "1%",
            title: l('Edit'),
            className: "dt-center",
            orderable: false,
            render: function (data, type, row) {
                return abp.auth.isGranted('HRMapp.Attendent.Update') ? ` <a data-id="${row.id}" class="edit-button" href="#" > <i  class="fa fa-edit"></i> </a>` : "";
            }
        });
        result.push({
            width: "1%",
            title: l('Delete'),
            orderable: false,
            className: "dt-center",
            render: function (data, type, row) {
                return abp.auth.isGranted('HRMapp.Attendent.Delete') ? ` <a data-id="${row.id}" class="delete-button text-danger" href="#" > <i  class="fa fa-trash"></i> </a>` : "";
            }
        })

        return result;
    }

    /*  var dataTable = $('#AttendentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                  rowAction: {
                      items:
                          [
                              {
                                  text: l('Edit'),
                                  visible: abp.auth.isGranted('HRMapp.Attendent.Update'),
                                  action: function (data) {
                                      editModal.open({ id: data.record.id });
                                  }
                              },
                              {
                                  text: l('Delete'),
                                  visible: abp.auth.isGranted('HRMapp.Attendent.Delete'),
                                  confirmMessage: function (data) {
                                      return l('AttendentDeletionConfirmationMessage', data.record.id);
                                  },
                                  action: function (data) {
                                      service.delete(data.record.id)
                                          .then(function () {
                                              abp.notify.info(l('SuccessfullyDeleted'));
                                              dataTable.ajax.reload();
                                          });
                                  }
                              }
                          ]
                  }
              },
  
  
              {
                  title: l('Date'),
                  data: "date",
                  "render": function (data, type, full, meta) {
                      return moment(data).format("DD-MM-YYYY");
                  }
                  
              },
  
  
              {
                  title: l('EmployeeName'),
                  data: "employeeName"
              },
  
  
              {
                  title: l('MissingIn'),
                  data: "missingIn"
              },
  
  
              {
                  title: l('MissingOut'),
                  data: "missingOut"
              }/!*,
              {
                  title: l('timeMissingIn'),
                  data: "timeMissingIn"
              },
              {
                  title: l('timeMissingOut'),
                  data: "timeMissingOut"
              }*!/
          ]
      }));*/

    function GetDatatable(column) {
        let newcolumnnew = columnnew(column);
        dataTable = $('#AttendentTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
            columnDefs: newcolumnnew

        }));
        //visible column
        dataTable.column([]).visible(false, false);

    }

    createModal.onResult(function () {
        dataTable.ajax.reload();

    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewAttendentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click', '.ViewAttendentBtn', function (e) {
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

    //date range
    $('#AttendentFilter_Datetime').daterangepicker({
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
    $('#AttendentFilter_Datetime').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format(dateformat) + ' - ' + picker.endDate.format(dateformat));
        $(this).trigger("change");
    });
    $('#AttendentFilter_Datetime').on('cancel.daterangepicker', function (ev, picker) {
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
        abp.message.confirm(l('AttendentDeletionConfirmationMessage', id))
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


    //end

});



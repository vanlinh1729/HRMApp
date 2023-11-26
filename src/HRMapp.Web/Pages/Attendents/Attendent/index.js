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

    createModal.onOpen(function () {
        $.ajax({
            url: '/api/app/department', // Đổi đường dẫn API của bạn tại đây
            method: 'GET',
            success: function (data) {
                // Xóa các option cũ
                $('#searchSelect').empty();

                // Thêm option mới từ dữ liệu API
                $('#searchSelect').append('<option value="">Tất cả</option>');
                $.each(data.items, function (index, item) {
                    $('#searchSelect').append('<option value="' + item.name + '">' + item.name + '</option>');
                });
                // Attach an event listener to handle changes in the dropdown
                $('#searchSelect').on('change', function () {
                    var selectedDepartmentName = $(this).val();
                    // Update the DataTable with the new search filter
                    allEmployee.column(1).search(selectedDepartmentName).draw();
                });
            },
            error: function (error) {
                console.log('Lỗi khi gọi API: ', error);
            }
        });
        console.log('opened the modal...');
        var l = abp.localization.getResource('HRMapp');
        var departmentName = $('#searchSelect').val();
        console.log('Department Name:', departmentName);
        var allEmployee = $('#AllEmployeeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
            ajax: abp.libs.datatables.createAjax(service.getAllEmployeeIntoAttendent, {departmentName:departmentName}),
            columnDefs: [{
                orderable: false,
                title: l('EmployeeName'), data: "name",
            }, 
                {
                orderable: false,
                title: l('DepartmentName'), data: "departmentName"
            }, 
                {
                    width: "1%",
                    orderable: false,
                    className: "dt-center",
                    title: "Chọn", data: "id", render: function (data, type, row) {
                        console.log(row)
                        return "<a class='selectToAttendent' data-name='" + row.name + "'  data-id='" + row.id + "' style=\"text-decoration: none\"><i class=\"fa fa-check-circle\"></i></a>"
                    }
                }
            ]


        }));
    });
    
    
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

    $(document).on('click', '.selectToAttendent', function (e) {
        e.preventDefault();
        var id = this.dataset.id;
        var name = this.dataset.name;
        var xmlString = `<tr id='${id}' >\n" +
            "                                        <td> ${name} </td>\n
            
            " +
            "                                        <td style=\"text-align: center\"><a class=\"deleteemployeebtn\" href=\"javascript:void(0);\" data-name=${name} data-id=${id}><i class=\"text-danger fa fa-trash\"></i></a></td>\n" +
            "                                    </tr>`;
        var elementClick = $(this);
        var inputStr =`<input type="text" id="ViewCreateModel_employeeId+${id}" name="ViewCreateModel.employeeId" value="${id}" class="form-control form-control-sm" hidden="hidden">`;
        elementClick.children().remove()
        $("#EmployeeAbsent > tbody").append(xmlString);
        $("#EmployeeAbsent").parent().append(inputStr);

    });
    $(document).on('click', '.deleteemployeebtn', function (e) {
        e.preventDefault();
        var id = this.dataset.id;
        var name = this.dataset.name;
        var element = document.getElementById(id)
        var deletebutton = document.getElementById("ViewCreateModel_employeeId+"+id);
        var selectStr = `<i class="fa fa-check-circle"></i>`;
        element.remove();
        $(`[data-id=${id}]`).append(selectStr);
        deletebutton.remove();
    });

    //end

});



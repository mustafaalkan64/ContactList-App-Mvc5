//$(document).ready(function () {

    var EmployeeTags = [];
    var contactTypes = [];
    var EmployeeContactTypeIds = [];
    var tags = [];
    $("#Tag").on("change", function () {
        
        var selectedvalue = $(this).find("option:selected").val();
        var selectedtext = $(this).find("option:selected").text();
        if ($.inArray(selectedtext, EmployeeTags) == -1) {

            if (selectedvalue != '') {
                EmployeeTags.push(selectedvalue);
                tags.push({ "TagId": selectedvalue, "TagName": selectedtext });
                $("#Tag option[value='" + selectedvalue + "']").remove();
                $("#TagList").show();
                $("#TagList").find("h4").append("<span class='label label-warning'>" + selectedtext + "<img id='" + selectedvalue + "_closeimg' class='clssimgbtn' tooltip=" + selectedvalue + " src='/Images/close1.png'/></span> ")
                    .after(function () {
                        $(".clssimgbtn").click(function () {
                            debugger;
                            var selectedtagvalue = $(this).attr("tooltip");
                            var selectedtagtext = $(this).parent().text();

                            if ($.inArray(selectedtagvalue, EmployeeTags) != -1) {
                                
                                if (selectedvalue != '') {
                                    EmployeeTags.splice(EmployeeTags.indexOf(selectedtagvalue), 1);
                                    $.each(tags, function (i, el) {
                                        if (this.TagId == selectedtagvalue) {
                                            tags.splice(i, 1);
                                        }
                                    });
                                    $("#Tag").append(
                                            $('<option></option>').val(selectedtagvalue).html(selectedtagtext)
                                    );
                                    $(this).parent().remove();
                                    if (EmployeeTags.length == 0) {
                                        $("#TagList").hide();
                                    }
                                }
                            }
                        });
                    });
            }
        }
    });


    $("#ContactType").on("change", function () {

        var selectedvalue = $(this).find("option:selected").val();
        var selectedtext = $(this).find("option:selected").text();
        if ($.inArray(selectedvalue, EmployeeContactTypeIds) == -1) {

            if (selectedvalue != '') {

                EmployeeContactTypeIds.push(selectedvalue);
                //console.log(EmployeeContactTypes);
                $("#ContactType option[value='" + selectedvalue + "']").remove();
                $("#ContactList").show();
                $("#ContactTypeDiv").after('<div class="form-group"><label class="control-label col-md-2">' + selectedtext + '</label>' +
                    '<div class="col-md-6">' +
                    '<input class="form-control text-box single-line contactypeclass" data-val="true" data-val-required="This Field Is Required" type="text" id=' + selectedvalue + ' name = ' + selectedvalue + ' value="" />' +
                    '</div>' +
                    '<div class="col-md-2">' +
                    '<img id="' + selectedvalue + '_closecontactimg" class="clssimgcontactbtn" tooltip=' + selectedvalue + ' src="/Images/close1.png"/>' +
                    '</div>' +
                    '<div class="col-md-2">' +
                    '<span class="field-validation-valid text-danger" data-valmsg-for=' + selectedvalue + ' data-valmsg-replace="true"></span>' +                    '</div>' +
                    '</div>')
                    .after(function () {
                        $(".clssimgcontactbtn").click(function () {
                            var selectedcontactvalue = $(this).attr("tooltip");
                            var selectedcontacttext = $(this).parent().parent().first("label").text();

                            if ($.inArray(selectedcontactvalue, EmployeeContactTypeIds) != -1) {

                                if (selectedvalue != '') {
                                    EmployeeContactTypeIds.splice(EmployeeContactTypeIds.indexOf(selectedvalue), 1);
                                    //console.log(EmployeeContactTypes);
                                    $("#ContactType").append(
                                            $('<option></option>').val(selectedcontactvalue).html(selectedcontacttext)
                                    );
                                    $(this).parent().parent().remove();
                                    if (EmployeeContactTypeIds.length == 0) {
                                        $("#ContactList").hide();
                                    }
                                }
                            }
                        });
                    });
            }
        }
    });

    $(".clssimgcontactbtn").click(function () {
        var selectedcontactvalue = $(this).attr("tooltip");
        var selectedcontacttext = $(this).parent().parent().first("label").text();
        //var selectedvalue = $("#ContactType").find("option:selected").val();

        if ($.inArray(selectedcontactvalue, EmployeeContactTypeIds) != -1) {

            EmployeeContactTypeIds.splice(EmployeeContactTypeIds.indexOf(selectedcontactvalue), 1);
                //console.log(EmployeeContactTypes);
                $("#ContactType").append(
                        $('<option></option>').val(selectedcontactvalue).html(selectedcontacttext)
                );
                $(this).parent().parent().remove();
                if (EmployeeContactTypeIds.length == 0) {
                    $("#ContactList").hide();
                }
            }
    });

    $(".clssimgbtn").click(function () {
        var selectedtagvalue = $(this).attr("tooltip");
        var selectedtagtext = $(this).parent().text();
        if ($.inArray(selectedtagvalue, EmployeeTags) != -1) {

                EmployeeTags.splice(EmployeeTags.indexOf(selectedtagvalue), 1);
                $.each(tags, function (i, el) {
                    if (this.TagId == selectedtagvalue) {
                        tags.splice(i, 1);
                    }
                });
                $("#Tag").append(
                        $('<option></option>').val(selectedtagvalue).html(selectedtagtext)
                );
                $(this).parent().remove();
                if (EmployeeTags.length == 0) {
                    $("#TagList").hide();
                }
        }
    });


    $("#CreateEmployeeForm").submit(function (e) {

        var employee = {
            "Name": $("#Name").val(),
            "Surname": $("#Surname").val()
        };

        contactTypes = [];
        $(".contactypeclass").each(function () {
            contactTypes.push({ "ContactTypeName": $(this).attr('name'), "ContactTypeValue": $(this).val() });
        });

        $.ajax({
            url: httpurl + 'api/employees/create',
            type: 'post',
            data: {
                employee: employee,
                contactTypes: contactTypes,
                tags: tags
            },
            ContentType: 'application/json;utf-8',
            datatype: 'json',
            async: false

        }).done(function (resp) {
            alert(resp);
            e.preventDefault();

        }).error(function (err) {
            console.log(err);
            alert("Error " + err.status);
            e.preventDefault();

        });
    });

    $("#EditEmployeeForm").submit(function (e) {

        var employee = {
            "Name": $("#Name").val(),
            "Surname": $("#Surname").val()
        };

        contactTypes = [];
        $(".contactypeclass").each(function () {
            contactTypes.push({ "ContactTypeName": $(this).attr('name'), "ContactTypeValue": $(this).val() });
        });

        $.ajax({
            url: httpurl + 'api/employees/update?id=' + $("#Id").val(),
            type: 'post',
            data: {
                employee: employee,
                contactTypes: contactTypes,
                tags: tags
            },
            ContentType: 'application/json;utf-8',
            datatype: 'json',
            async: false

        }).done(function (resp) {
            alert(resp);
            e.preventDefault();

        }).error(function (err) {
            console.log(err);
            alert("Error " + err.status);
            e.preventDefault();

        });
    });

    
//});

function DeleteEmployee(id) {

    var r = confirm("Are You Sure to Delete ?");
    if (r == true) {
        $.ajax({
            url: httpurl + 'api/employees/delete?id=' + id,
            type: 'post',
            ContentType: 'application/json;utf-8',
            datatype: 'json',
            async: false

        }).done(function (resp) {
            alert(resp);
            window.location.reload();

        }).error(function (err) {
            console.log(err);
            alert("Error " + err.responseText);
        });
    }
}

function ViewEmployeeDetail(id) {

        $.ajax({
            url: httpurl + 'api/employees/details?id=' + id,
            type: 'get',
            ContentType: 'application/json;utf-8',
            datatype: 'json',
            async: false

        }).done(function (resp) {

            $("#EmployeeName").text(resp.Name);
            $("#EmployeeSurname").text(resp.Surname);
            $("#_Tags").text(resp.Tags);
            $("#ContactInfoHeader").next("div").remove();
            $.each(resp.EmployeeProperties, function( key, value ) {

                $("#ContactInfoHeader").after('<div class="row row-bosluk">' +
                        '<div class="col-md-4">' +
                            '<strong>'+value.ContactType+'</strong>' +
                        '</div>' +
                        '<div class="col-md-8">' +
                            '<span>'+ value.Value +'</span>' +
                        '</div>' +
                    '</div>');
            });
            
            $("#kayitFormu").modal("show");

        }).error(function (err) {
            console.log(err);
            alert("Error " + err.responseText);
        });
}

function InitEditPage() {

    $(".contactypeclass").each(function () {
        contactTypes.push({ "ContactTypeName": $(this).attr('name'), "ContactTypeValue": $(this).val() });
        EmployeeContactTypeIds.push($(this).attr('name'));
    });
    $(".label-warning").each(function () {
        tags.push({ "TagId": $(this).find("img").attr("tooltip"), "TagName": $(this).text() });
        EmployeeTags.push($(this).find("img").attr("tooltip"));
    });
    console.log(contactTypes);
    console.log(tags);
    if (tags.length > 0)
    {
        $("#TagList").show();
    }
    console.log(EmployeeTags);
    console.log(EmployeeContactTypeIds);
}
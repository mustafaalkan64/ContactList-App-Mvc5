function DeleteTag(id) {
    var r = confirm("Are You Sure to Delete ?");
    if (r == true) {
        $.ajax({
            url: httpurl + 'api/tags/delete?Id=' + id,
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
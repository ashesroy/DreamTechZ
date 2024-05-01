$(document).ready(function () {
    $('#opsection').val('INSERT');
});

function GetStudent(studentid) {
    $.ajax({
        type: "GET",
        url: "/Student/GetStudentByID?studentid=" + studentid,
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            $('#s_id').val(result.s_id);
            $('#s_name').val(result.s_name);
            $('#s_Age').val(result.s_Age);
            $('#s_Address_id').val(result.s_Address_id);
            $('#s_SubjectID').val(result.s_SubjectID);
            $('#opsection').val("UPDATE");
        },
        Error: function (e) {
            console.log(e);
        }
    });
}

function RemoveStudent(id) {
    if (confirm("Are you sure? you want to remove this student.")) {
        $.ajax({
            type: "DELETE",
            url: "/Student/StudentRemove?id=" + id,
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);
                window.location.reload();
            },
            Error: function (e) {
                console.log(e);
            }
        });
    } else {
        return false;
    }
    
}
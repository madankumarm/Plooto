
function taskList() {
    // Call Web API to get a list of Product
    $.ajax({
        url: '/api/Task/',
        type: 'GET',
        dataType: 'json',
        success: function (tasks) {
            taskListSuccess(tasks);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}
function taskListSuccess(tasks) {
    // Iterate over the collection of data
    $.each(tasks, function (index, task) {
        // Add a row to the task table
        taskAddRow(task);
    });
}
function taskAddRow(task) {
    // Check if <tbody> tag exists, add one if not

    // Append row to <table>
    $("#taskTable").append(
        taskBuildTableRow(task));
}

function taskBuildTableRow(task) {
    var ret =
        "<tr>" +
        "<td>" +
        "<button type='button' " +
        "id='btnEdit" + task.Id + "' " +
        "onclick='taskGet(this);' " +
        "data-id='" + task.Id + "'>" +
        "Edit" +
        "</button>" +
        "</td >" +
        "<td><textarea id='TextArea" + task.Id + "' rows='1' cols='100' disabled='true' >" + task.Description + "</textarea></td>" +
        "<td>" +
        "<button type='button' " +
        "id='btnComplete" + task.Id + "' " +
        "onclick='taskComplete(this);' " +
        "data-id='" + task.Id + "'>" +
        "Complete" +
        "</button>" +
        "</td>" +
        "<td>" +
        "<button type='button' " +
        "onclick='taskDelete(this);' " +
        "data-id='" + task.Id + "'>" +
        "Delete" +
        "</button>" +
        "</td>" +
        "<td><input type='hidden' id='taskId' value='" + task.Id + "' /></td>"
    "</tr>";
    return ret;
}
function taskComplete(ctl) {
    // Get task id from data- attribute
    var id = $(ctl).data("id");
    var complete = false;
    task = new Object();

    task.Id = id;
    task.Description = document.getElementById("TextArea" + id).textContent;

    if (document.getElementById("btnComplete" + id).textContent == "Complete") {

        complete = true;
        task.Complete = complete;


        $.ajax({
            url: "/api/task/" + id,
            type: 'PUT',
            dataType: 'json',
            contentType:
                "application/json;charset=utf-8",
            data: JSON.stringify(task),
            success: function (task) {
                $("#btnComplete" + id).text("InComplete");
                $("#TextArea" + id).css("text-decoration", "line-through");
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });

    }
    else {
        complete = false;
        task.Complete = complete;


        $.ajax({
            url: "/api/task/" + id,
            type: 'PUT',
            dataType: 'json',
            contentType:
                "application/json;charset=utf-8",
            data: JSON.stringify(task),
            success: function (task) {

                $("#btnComplete" + id).text("Complete");
                $("#TextArea" + id).css("text-decoration", "none");
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
    }




}
function taskGet(ctl) {
    // Get task id from data- attribute
    var id = $(ctl).data("id");

    if (document.getElementById("btnEdit" + id).textContent == "Edit") {
        $("#btnEdit" + id).text("Update");
        document.getElementById("TextArea" + id).disabled = false;
    }
    else {
        task = new Object();
        task.Id = id;
        task.Description = document.getElementById("TextArea" + id).textContent;

        $.ajax({
            url: "/api/task/" + id,
            type: 'PUT',
            dataType: 'json',
            contentType:
                "application/json;charset=utf-8",
            data: JSON.stringify(task),
            success: function (task) {
                //$("#tbTask").val(task.Description);

                // Change Update Button Text
                $("#btnEdit" + id).text("Edit");
                document.getElementById("TextArea" + id).disabled = true;
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
    }

}

function taskDelete(ctl) {
    var id = $(ctl).data("id");
    $.ajax({
        url: "/api/Task/" + id,
        type: 'DELETE',
        success: function (task) {
            $(ctl).parents("tr").remove();
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function taskAdd(task) {
    $.ajax({
        url: "/api/Task",
        type: 'POST',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(task),
        success: function (task) {
            taskAddSuccess(task);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}
function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}
function taskAddSuccess(task) {
    taskAddRow(task);
    formClear();
}
function formClear() {
    $("#tbTask").val("");

}
var task = {
    Id: 0,
    Complete: false,
    Description: ""
}

function addClick() {
    if (document.getElementById('tbTask').value == "") {
        alert("Cannot Add empty task!");
    }
    else {


        task = new Object();
        task.Description = $("#tbTask").val();
        taskAdd(task);
    }
}

$(document).ready(function () {
    taskList();
});

﻿function serverResponse(response) {
    var jsonData = JSON.stringify(response);
    var Obj = JSON.parse(jsonData);
    if (Obj.msgtype == "e") {
        $("#alertBox").attr("class", "alert alert-danger");
        $("#alertBox").html("<p><b>" + Obj.msg + "</b></p>");
    }
    if (Obj.msgtype == "s") {
        $("#cnfm").modal('show');
        $("#cnfmBox").html("<p><b>" + Obj.msg + "</b></p><button class='btn btn-success' onclick='rediectToLogin()'>Ok</button>");
    }
}
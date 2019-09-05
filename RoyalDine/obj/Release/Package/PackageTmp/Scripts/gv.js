function getMealData(response) {
    var jsonData = JSON.stringify(response);
    var Obj = JSON.parse(jsonData);
    if (Obj.msgtype == "e") {
        $("#alertBox").attr("class", "alert alert-danger");
        $("#dataViewer").attr("class", "hidden");
        $("#alertBox").html("<p><b>" + Obj.msg + "</b></p>");
    }

    if (Obj.msgtype == "s") {
        $("#mealEditor").modal('show');
        $("#alertBox").attr("class", "hidden");
        $("#dataViewer").attr("class", "");
        $("#dataViewer").html(Obj.msg);
    }
}
function confirmRemovingB(i) {
    var btnTxt = $('#b_' + i).attr('name');
    var btnLink = $('#b_' + i).attr('value');
    $("#confmInfo").html("<p>Are you sure to remove <b>" + btnTxt + "</b> from breakfast?</p>" + "<a class='btn btn-danger' data-ajax='true' data-ajax-success='serverResponse'  href='" + btnLink + "'>Remove</a>");
    $("#confm").modal('show');
}

function confirmRemovingL(i) {
    var btnTxt = $('#l_' + i).attr('name');
    var btnLink = $('#l_' + i).attr('value');
    $("#confmInfo").html("<p>Are you sure to remove <b>" + btnTxt + "</b> from lunch?</p>" + "<a class='btn btn-danger' data-ajax='true' data-ajax-success='serverResponse' href='" + btnLink + "'>Remove</a>");
    $("#confm").modal('show');
}

function confirmRemovingD(i) {
    var btnTxt = $('#d_' + i).attr('name');
    var btnLink = $('#d_' + i).attr('value');
    $("#confmInfo").html("<p>Are you sure to remove <b>" + btnTxt + "</b> from dinner?</p>" + "<a class='btn btn-danger' data-ajax='true' data-ajax-success='serverResponse' href='" + btnLink + "'>Remove</a>");
    $("#confm").modal('show');
}


function sendcomment(id, iduser) {
    var x = document.getElementById(id);
    var fa = new FormData();
    fa.append("idpost", x.id);
    fa.append("comment", x.value);
    fa.append("iduser", iduser)
    $.ajax({
        url: 'https://localhost:44359/api/Comments',
        type: 'POST',
        data: fa,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            x.value = "";
        }
    });
}
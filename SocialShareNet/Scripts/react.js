


function sendReact(idpost, iduser, react) {

   
    var fa = new FormData();
    fa.append("idpost", idpost);
    fa.append("react", react);
    fa.append("iduser", iduser)


    $.ajax({
        url: 'https://localhost:44359/api/react',
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
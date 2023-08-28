


ShowInPopUp = (url, title) => {
    $.ajax({
        type: "get",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").show();
        }
    })

}
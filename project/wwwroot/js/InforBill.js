$(document).ready(function () {
    var _id;
    $('.item-click').click(function (e) {
        _id = e.target.dataset.id;
    });
    $('.btn-submit').click(function () {
        console.log(_id);
        $.ajax({
            type: "POST",
            url: '/Bill/DeleteInfor',
            dataType: 'Json',
            data: {
                id: _id
            },
            success: function (res) {
                if (res.status == true) {
                    window.location.href = "/Bill";
                } else {
                    console.log("Không thành công");
                }
            }
        });
    })
})
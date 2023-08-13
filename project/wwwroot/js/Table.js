﻿$(document).ready(function () {
    var _id;
    $('.item-click').click(function (e) {
        _id = e.target.dataset.id;
    });
    $('.btn-submit').click(function () {
        $.ajax({
            type: "POST",
            url: '/Table/Delete',
            dataType: 'Json',
            data: {
                id: _id
            },
            success: function (res) {
                if (res.status == true) {
                    var _elementString = ".item-" + _id;
                    $('#deleteModal').modal('hide');
                    $(_elementString).remove();
                } else {
                    console.log("Không thành công");
                }
            }
        });
    })
})
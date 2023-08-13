$(document).ready(function () {
    var _id;
    $('.click-item').on('click', function (e) {
        _id = e.target.dataset.id;
        console.log(_id);
    });
    $('.btn-submit').click(function () {
        console.log(_id);
        $.ajax({
            type: "POST",
            url: '/CategoryFood/Delete',
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
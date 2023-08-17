$(document).ready(function () {
    var _id;
    $('.item-click').click(function (e) {
        _id = e.target.dataset.id;
    });
    function deleteDate() {
        return $.ajax({
            type: "POST",
            url: '/Bill/DeleteInfor',
            dataType: 'Json',
            data: {
                id: _id
            },

        });
    }
    $('.btn-submit').click(async function () {
        try {
            const res = await deleteDate();
            if (res.status == true) {
                if (res.isReload == true)
                    window.location.href = "/Bill";
                else {
                    var _elementString = ".item-" + _id;
                    $('#deleteModal').modal('hide');
                    $(_elementString).remove();
                }
            } else {
                console.log("Không thành công");
            }
        }
        catch (err) {
            console.log(err);
        }
    })
})
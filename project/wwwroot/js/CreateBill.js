$(document).ready(function () {
    var listMonAn = [];
  
    $('.btn-add').on('click', function () {
        var listTest = $('#listMonAnOrder')[0].options;
        var nameOption;

        var itemMonAn = $('#listMonAnOrder').val();
        if (listMonAn.find(x => x == itemMonAn)) {
            return;
        }
        for (var i = 0; i < listTest.length; i++) {
            if (listTest[i].value == itemMonAn) {
                nameOption = listTest[i].innerText;
                break;
            }
        }
        listMonAn.push($('#listMonAnOrder').val());
        var root = document.getElementById('listHoaDon');
        var li = document.createElement('li');
        var span = document.createElement('span');
        var input = document.createElement('input');
        var button = document.createElement('button');
        button.classList.add('btn', 'btn-danger', 'ml-2', 'btn-delete');
        button.innerText = "Xoá";
        button.addEventListener('click', function handleClick(event) {
            listMonAn.pop(itemMonAn);
            li.remove();
        })
        input.classList.add('form-control', 'w-25', 'ml-2', 'list-input');
        input.value = 0;
        input.type = "number";
        span.innerText = nameOption;
        li.classList.add('mt-2', 'd-flex', 'align-items-center');
        li.appendChild(span);
        li.appendChild(input);
        li.appendChild(button);
        root.appendChild(li);
    });

    $('.btn-submit').on('click', function () {
        var listHoaDon = $('.list-input');
        console.log(listMonAn);
        var objectDatas = [];
        if (listMonAn.length == 0) {
            alert("Vui lòng chọn món ăn");
        }
        else {
            for (var i = 0; i < listHoaDon.length; i++) {
                console.log(listMonAn[i])
                let itemObject = {
                    Item: listMonAn[i],
                    Amount: listHoaDon[i].value
                };
                objectDatas.push(itemObject);
            }
        }
        console.log(objectDatas);
        $.ajax({
            url: '/Bill/Create',
            type: "POST",
            dataType: "Json",
            data: {
                VAT: $('#ThueVAT').val(),
                BanAnID: $('#BanAnID').val(),
                ObjectDatas: objectDatas
            },
            success: function (res) {
                if (res.result) {
                    window.location.href = "/Bill"
                }
                else {
                    alert("Thêm mới thất bại ");
                }
            }
        })

    })
})
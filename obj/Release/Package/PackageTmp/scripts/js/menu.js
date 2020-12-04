// Menu Update
$(".editBtn").click(function () {
    var str = ""
    var tdArr = new Array();
    var editBtn = $(this);

    var tr = editBtn.parent().parent();
    var td = tr.children();

    var intMenuNo = td.eq(0).text();
    var strMenuName = td.eq(1).text();
    var strMenuENName = td.eq(2).text();
    var strMenuLink = td.eq(3).text();

    console.log("클릭한 ROW의 모든 데이터 : " + tr.text());

    console.log(intMenuNo);
    $("#MenuNo").val(intMenuNo);
    $("#MenuName").val(strMenuName);
    $("#MenuENName").val(strMenuENName);
    $("#MenuLink").val(strMenuLink);
});

function Update() {
    var menuList = {
        MenuNo: $('#MenuNo').val(),
        MenuName: $('#MenuName').val(),
        MenuENName: $('#MenuENName').val(),
        MenuLink: $('#MenuLink').val(),
        MenuDesc: $('#MenuDesc').val(),
        UseStateCode: $('#UseStateCode').val(),
        MenuENDesc: $('#MenuENDesc').val(),
    };
    $.ajax({
        url: "/Menu/MenuUpdate",
        data: JSON.stringify(menuList),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert("success");
            $('#UpdMenuModal').modal('hide');
            $('#MenuName').val("");
            $('#MenuENName').val("");
            $('#MenuLink').val("");
            $('#UseStateCode').val("");
            $('#MenuDesc').val("");
            $('#MenuENDesc').val("");
            location.reload();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
// End Menu Update 

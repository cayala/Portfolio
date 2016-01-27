
function createCookie(data) {

    var options = {
        type: "POST",
        url: "ExampleWF.aspx/GetTableData",
        dataType: "json",
        data: "{data:" + data +"}",
        contentType: "application/json; charset=utf-8",
        Success: success,
        Error: error
    }
    $.ajax(options);
}

function storeTable() {
    var myRows = [];
    var headers = $("th");
    var rows = $("tbody tr").each(function (index) {
        var cells = $(this).find("td");
        myRows[index] = {};
        cells.each(function (cellIndex) {
            myRows[index][$(headers[cellIndex]).html()] = $(this).html();
        });
    });

    var obj = {};
    obj.myrows = JSON.stringify(myRows);
    console.log(obj);
    document.cookie = "table =" + obj;
    createCookie(obj);
}
function success(data) {
    console.log(data.d);
    console.log("success was hit");
}
function error(data) {
    console.log(data.d);
    console.log("error was hit");
}

$(document).ready(function () {

    $("#ajax").click(createCookie);
    $("#storeTable").click(storeTable);
    console.log("ready");
});
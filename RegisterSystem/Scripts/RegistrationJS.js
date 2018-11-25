$(document).ready(function () {
    var pageNums = 0; //记录查询结果总页数
    var currentPage = 0; //当前第几页


    //分页显示方法
    function paging() {
        for (var i = 1; i <= pageNums; i++) {
            $("#page").append(' <li><a  class="pageclick" >' + i + '</a ></li > ');
        }
    }
    //分页点击执行方法
    function pagingclick() {
        currentPage = parseInt($(this).text());
        var bname = $("#nameID").val();
        var bbeginTime = $("#beginTimeID").val();
        var bendTime = $("#endTimeID").val();
        $(".pageclick").click(function () {
            find(bname, bbeginTime, bendTime, currentPage);
        });
    }
    //分页按钮绑定方法
    function pagingbind() {

        paging();
        pagingclick();
    }

    //查询数据显示回调方法
    function showdata(data) {
        $("#table").append("<thead><th>#</th><th>姓名</th><th>预约原因</th><th>借出时间</th> <th>预约归还时间</th><th>实际归还时间</th><th>操作</th></thead >");
        for (var i = 0; i < data.length; i++) {
            $("#table").append("<tr><td>" + data[i].ID + "</td><td>" + data[i].Name + "</td><td>" + data[i].Reason + "</td><td>" + data[i].BeforeTime + "</td><td>" + data[i].BeforeAdvanceTime + "</td><td>" + data[i].AfterTime + "</td><td><button type='button' id='" + data[i].ID + "' class='btn btn-danger deleteData'>删除</button></td></tr>");
        }
        deleteData()
    }
    //删除按钮执行方法
    function deleteData() {
        $(".deleteData").click(function () {
            var myid = $(this).attr("id");
            $.post("/Look/DeleteRegistration", { id: myid }, function (data) {
                if ("true" == data) {
                    var bname = $("#nameID").val();
                    var bbeginTime = $("#beginTimeID").val();
                    var bendTime = $("#endTimeID").val();
                    $("#table").html("");
                    $("#page").html("");
                    btnClick(bname, bbeginTime, bendTime);
                } else {
                    alert("删除失败");
                }
            });
        });
    }
    //单页查询
    function find(fname, fbeginTime, fendTime) {
        $.post("/Look/FindRegistration", { name: fname, beginTime: fbeginTime, endTime: fendTime, page: currentPage }, function (data) {
            showdata(data);
        });
    }



    //按钮点击执行方法
    function btnClick(bname, bbeginTime, bendTime) {
        $.post("/Look/GetRegistrationPageNum", { name: bname, beginTime: bbeginTime, endTime: bendTime }, function (data) {
            pageNums = parseInt(data);
            if (pageNums < 1) {
                return;
            }
            currentPage = 1;
            find(bname, bbeginTime, bendTime);
            pagingbind();

        });
    };
    //按钮点击事件
    $("#btn").click(function () {
        var bname = $("#nameID").val();
        var bbeginTime = $("#beginTimeID").val();
        var bendTime = $("#endTimeID").val();
        $("#table").html("");
        $("#page").html("");
        btnClick(bname, bbeginTime, bendTime);
    });

});
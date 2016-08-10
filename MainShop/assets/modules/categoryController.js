if (typeof (CmsShop) == "undefined") CmsShop = {};
if (typeof (CmsShop.Category) == "undefined") CmsShop.Category = {};

CmsShop.Category = {
    PageSize: 10,
    PageIndex:1
}

CmsShop.Category.Init = function () {
    var $this = CmsShop.Category;
    
    $this.LoadAllCategory(function (data) {
        $this.LoadDropdowListCategory(data);
    });

    $this.RegisterEvents();
};

CmsShop.Category.RegisterEvents = function () {
    var $this = CmsShop.Category;

    $("#btnAddNewCategory").off("click").on("click", function () {
        $this.AddNewCategory();
    });
};

CmsShop.Category.AddNewCategory = function () {
    var $this = CmsShop.Category;

    var name = $("#txtNameCategory").val();
    var parentId = $("#sltCategory").val();

    var data = { name: name, parentId: parentId };

    $.post("/Category/Add", data, function (res) {
        if (res.status) {
            $("#txtNameCategory").val("");
            $("#sltCategory").val(0);

            $this.LoadAllCategory(function (data) {
                $this.LoadDropdowListCategory(data);
            });
        }
    }, "json");
};

CmsShop.Category.LoadAllCategory = function (callback) {
    var $this = CmsShop.Category;

    var data = { pageIndex: $this.PageIndex, pageSize: $this.PageSize };

    $.get("/Category/ListAllPaging", data, function (res) {
        if (res.status) {
            var data = res.Data;
            $("#listAllCategory").html("");
            $.each(data, function (i,item) {
                var temp = '<tr>'+
                        '<td>'+(i+1)+'</td>'+
                        '<td>' + item.Name + '</td>' +
                        '<td>'+
                            '<a href="#" class="btn btn-info btn-xs edit" data-id="' + item.Id + '"><i class="fa fa-edit"></i> Edit</a>' +
                            '<a href="#" class="btn btn-danger btn-xs delete" data-id="' + item.Id + '"><i class="fa fa-trash-o"></i> Delete</a>' +
                        '</td>' +
                    '</tr>';
                $("#listAllCategory").append(temp);
            });
            if(typeof(callback)=="function"){
                callback(data);
            }
        } 
    }, "json");
};

CmsShop.Category.LoadDropdowListCategory = function (data) {
    if (data != null) {        
        $("#sltCategory").html("");
        var temp = '<option value="0">Danh mục</option>';
        $("#sltCategory").append(temp);
        $.each(data, function (i, item) {
            temp = '<option value="' + item.Id + '">' + item.Name + '</option>';
            $("#sltCategory").append(temp);
        });
    }
};

$(function(){
    CmsShop.Category.Init();
});
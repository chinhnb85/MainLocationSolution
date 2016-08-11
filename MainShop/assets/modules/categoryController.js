if (typeof (CmsShop) == "undefined") CmsShop = {};
if (typeof (CmsShop.Category) == "undefined") CmsShop.Category = {};

CmsShop.Category = {
    PageSize: 100,
    PageIndex:1
}

CmsShop.Category.Init = function () {
    var $this = CmsShop.Category;    

    $this.LoadAllCategory(function (data) {
        $this.LoadDropdowListCategory(data);
        //$("#sltCategory").chosen();
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
                //$("#sltCategory").chosen();
                //$("#sltCategory").trigger('liszt:updated');
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
        var temp = '<option value="0">Danh mục</option>';
        
        $.each(data, function (i, item) {
            var name = item.Name;
            if (item.ParentId === 0) {
                temp += '<option value="' + item.Id + '">' + name + '</option>';
                $.each(data, function (i, item1) {
                    if (item.Id === item1.ParentId) {
                        name = "--- " + item1.Name;
                        temp += '<option value="' + item1.Id + '">' + name + '</option>';
                        $.each(data, function (i, item2) {
                            if (item1.Id === item2.ParentId) {
                                name = "------ " + item2.Name;
                                temp += '<option value="' + item2.Id + '">' + name + '</option>';
                                $.each(data, function (i, item3) {
                                    if (item2.Id === item3.ParentId) {
                                        name = "--------- " + item3.Name;
                                        temp += '<option value="' + item3.Id + '">' + name + '</option>';
                                    }
                                });
                            }
                        });
                    }
                });                
            }
        });

        $("#sltCategory").empty().html(temp);
        
        $("#sltCategory").chosen();
        $("#sltCategory").trigger('liszt:updated');
    }
};

$(function(){
    CmsShop.Category.Init();
});
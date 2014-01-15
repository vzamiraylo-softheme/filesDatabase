$(document).ready(function () {
    var categoriesToAdd = new Array;

    GetCategories();
    GetFilesForCategory(-1);

    $('body').on('click', '.nav-tabs li', function() {
        $('.active').removeClass('active');
        $(this).addClass('active');
    });
    
    $('body').on('click', '.itemCaptionHover', function () {
        //$(this).closest('.item').find('.fancybox').trigger('click');
        $.ajax({
            url: "/LargeView/ajaxLargeView",
            type: "POST",
            data : {
                id : $(this).attr('document-id')
            },
            success: function (data) {
                $.fancybox({
                    padding : 0,
                    content: data
                    //width : '90%',
                    //height: '90%',
                    //autoSize : false
                });
            },
            error: function () {
                showMessage("Something went wrong!");
            }

        });
    });

    $('.sharedContent, #RefreshShared').click(function () {
        $.ajax({
            url: "/Share/GetSharedContent",
            type: "POST",
            success: function (data) {
                $('#grid .row').empty();
                $('#grid .row').append(data);

                $('#actionButtons #myContentButtons').hide();
                $('#actionButtons #sharedContentButtons').show();
                $('#actionButtons #searchButtons').hide();
            },
            error: function () {
                showMessage("Something went wrong!");
            }

        });
    });
    
    $('.myContent').click(function () {
        GetFilesForCategory(-1);
        $('#actionButtons #myContentButtons').show();
        $('#actionButtons #sharedContentButtons').hide();
        $('#actionButtons #searchButtons').hide();
    });
    
    $('.newsTab').click(function () {
        $('#actionButtons #myContentButtons').hide();
        $('#actionButtons #sharedContentButtons').hide();
        $('#actionButtons #searchButtons').hide();
        $('#grid .row').empty();
        GetNewsAjax(1, 10);
    });

    $('body').on('click', '.loadMore', function() {
        GetNewsAjax($('.newsItem').length, 10);
    });

    $('.searchTab').click(function () {
        $('#grid .row').empty();
        $('#actionButtons #myContentButtons').hide();
        $('#actionButtons #sharedContentButtons').hide();
        $('#actionButtons #searchButtons').show();
    });

    $('body').on('click', '.filesCategories li a.category', function() {
        var $this = $(this);
        $('.filesCategories li a.selected').removeClass('selected');
        $this.addClass('selected');
        $('.filesCategories button:first').text($this.text());
        GetFilesForCategory($this.attr('category-id'));
        
    });
    
    $('body').on('click', '#newFileCat_wrapper .categoriesList li', function () {
        $(this).toggleClass('selected');

        categoriesToAdd = [];
        $('#newFileCat_wrapper .categoriesList li.selected').each(function () {
            categoriesToAdd.push($(this).attr('category-id'));
        });
        $('#newFileCat_wrapper input#categories').val(categoriesToAdd);
    });

    $('body').on('click', '.filesCategories a.addNewCat', function() {
        showModal("#addNewCatWrapper");
    });
    
    $('body').on('click', '#filesSection .shareTpl_Item', function () {
        $(this).find('div.shareTpl_hover').toggleClass('checked');
    });
    
    $('body').on('click', '#userSection .row', function () {
        $(this).children('.userSearchHover').toggleClass('checked');
    });

    $('body').on('click', '#shareContent', function () {
        $.ajax({
            url: "/Share/GetThumbsForShare",
            type: "POST",
            data: {
                id : -1
            },
            success: function (data) {
                $('#filesSection').empty();
                $('#filesSection').append(data);
            }
        });
        $.ajax({
            url: "/User/UserSearch",
            type: "POST",
            data: {
                type: "share",
                str: ""
            },
            success: function (data) {
                $('#userSection #userSearchResult').empty();
                $('#userSection #userSearchResult').append(data);
            }
        });
        showModal("#shareContentWrapper");
    });

    $('body').on('click', '#shareContentButton', function () {
        var filesId = new Array();
        var usersId = new Array();
        $('#filesSection .shareTpl_hover.checked').each(function() {
            filesId.push($(this).attr('data-id'));
        });
        $('#userSection .userSearchHover.checked').each(function () {
            usersId.push($(this).parent().attr('user-id'));
        });

        $.ajax({
            url: "/Share/ShareContent",
            type: "POST",
            data: {
                'files' : filesId.join(),
                'users' : usersId.join()
            },
            success: function (data) {
                if (data.result) closeModal('#shareContentWrapper');
                showMessage(data.message);
            }
        });
    });
    
    $('body').on('click', '#userAvatar', function () {
        showModal("#avatarUploadWrapper");
    });

    $('#avatar_big').mouseenter(function() {
        $(this).toggleClass('img-circle');
    });
    
    $('#avatar_big').mouseleave(function () {
        $(this).toggleClass('img-circle');
    });
    
    $('#avatar_big').click(function () {
        $('#avatar_input').trigger('click');
    });

    $('#deleteAllButton').click(function()
    {
        if (confirm("Are you sure to delete all your content? (Files, Groups)")) {
            $.ajax({
                url: "/File/deleteAllFiles",
                type: "POST",
                success: function (data) {
                    if (data.result) {
                        $('#grid .row').empty();
                        $('.categoriesListItem').remove();
                    }
                    showMessage(data.message);
                }
            });
        }
    });
    
    $('#deleteAllSharedButton').click(function () {
        if (confirm("Are you sure to delete all your content? (Files, Groups)")) {
            $.ajax({
                url: "/Share/deleteAllSharedFiles",
                type: "POST",
                success: function (data) {
                    if (data.result) {
                        $('#grid .row').empty();
                    }
                    showMessage(data.message);
                }
            });
        }
    });
    
    $('#avatarForm').ajaxForm({
        success: function(data) {
            if (data.result) {
                $('#avatarUploadContent #avatar_big').attr("src", data.avatar);
                $('#userAvatar img').attr("src", data.avatar);
        }
            else showMessage(data.message);
        }
    });

    $('body').on('change', '#avatarUploadContent #avatar_input', function () {
        $('#submitAvatarUpload').click();
    });
    
    $('body').on('keyup', '#shareContentWrapper #userShareInput', function () {
        ajaxSearch("userShare", $('#userSection #userShareInput').val());
    });
    
    $('body').on('keyup', '#searchInput', function () {
        ajaxSearch("userSearch", $('#searchInput').val());
    });
    
    $('body').on('click', '.searchButton', function () {
        ajaxSearch("userSearch", $('#searchInput').val());
    });

    function ajaxSearch(type, str) {
        $.ajax({
            url: "/Home/Search",
            type: "POST",
            data: {
                type: type,
                str: str
            },
            success: function (data) {
                if (type == "userSearch") {
                    $('#grid .row').empty();
                    $('#grid .row').append('<div class="searchBody"></div>');
                    $('#grid .row .searchBody').append(data);
                }
                if (type == "userShare") {
                    $('#userSection #userSearchResult').empty();
                    $('#userSection #userSearchResult').append(data);
                }
            }
        });
    }

    $('body').on('click', '#avatarUploadContent button.btn-danger', function () {
        $.ajax({
            url: "/User/DeleteAvatar",
            type: "POST",
            success: function (data) {
                if (data.result) {
                    $('#avatarUploadContent img').attr("src", "").attr("src", data.avatar);
                    $('#userAvatar img').attr("src", "").attr("src", data.avatar);
                }
                showMessage(data.message);
            }
        });
    });
    
    $('body').on('click', '.docCategory a.addCatToFile', function () {
        getCategoriesList(".categoriesList");
        $("#newFileCat_wrapper").attr({ "doc-id": $(this).parent().parent().attr('doc-id') });
        showModal("#newFileCat_wrapper");
    });
    
    $('body').on('click', '.filesCategories span.renameCategory', function () {
        $("#renameCatButton").attr({ "category-id": $(this).prev('a').attr('category-id') });
        showModal("#renameCatWrapper");
    });

    $('#addNewCatButton').click(function() {
        $.ajax({
            url: "/Categories/AddNewCategory",
            type: "POST",
            data: {
                catName: $('#addNewCatWrapper input#catName').val()
            },
            success: function (data) {
                GetCategories();
                if (data.result)closeModal('#addNewCatWrapper');
                showMessage(data.message);
            }

        });
    });

    $('body').on('click', '.deleteDoc', function () {
        var item = $(this).closest('.item');
        if(confirm("Are you sure?")){
        $.ajax({
            url: "/File/DeleteFile",
            type: "POST",
            data: {
                id: $(this).attr('document-id')
            },
            success: function (data) {
                if(data.result)item.remove();
                showMessage(data.message);
            }
        });
    }
    });

    $('body').on('click', '.item .dropdown-menu button.dropdown-toggle', function(e) {
        e.stopPropagation();
        $(this).parent().toggleClass('open');
    });
    
    $('body').on('click', '.deleteSharedDoc', function () {
        var item = $(this).closest('.item');
        if (confirm("Are you sure?")) {
            $.ajax({
                url: "/Share/DeleteSharedFile",
                type: "POST",
                data: {
                    id: $(this).attr('document-id')
                },
                success: function (data) {
                    if (data.result) item.remove();
                    showMessage(data.message);
                }
            });
        }
    });
    
    $('body').on('click', '#addCatToFile', function () {
            $.ajax({
                url: "/Categories/AddCategoriesToFile",
                type: "POST",
                data: {
                    cat_ids : $(this).parent().find('input#categories').val(),
                    file_id: $('#newFileCat_wrapper').attr('doc-id')
                },
                success: function (data) {
                    if (data.result) {
                        closeModal("#newFileCat_wrapper");
                        refreshGrid();
                    }
                    showMessage(data.message);
                }
            });
    });
    
    $('body').on('click', '.deleteFileCategory', function () {
        var target = $(this);
        if (confirm('Are you sure?')) {
            $.ajax({
                url: "/Categories/DeleteFileFromCategory",
                type: "POST",
                data: {
                    cat_id : target.attr('cat-id'),
                    doc_id : target.parent().parent().attr('doc-id')
                },
                success: function (data) {
                    if (data.result) {
                        target.parent().remove();
                    }
                    showMessage(data.message);
                }
            });
        }
    });

    $('body').on('click', '.deleteCategory', function () {
        var target = $(this);
        if (confirm('Are you sure want to delete ' + $(this).parent().find('a').text() + ' category?')) {
            $.ajax({
                url: "/Categories/DeleteCategory",
                type: "POST",
                data: {
                    id: target.parent().find('a').attr('category-id')
                },
                success: function (data) {
                    if (data.result) {
                        target.parent().remove();
                        refreshGrid();
                    } 
                    showMessage(data.message);
                }
            });
        }
    });
    
    $('body').on('click', '#renameCatButton', function () {
        var targetId = $(this).attr('category-id');
            $.ajax({
                url: "/Categories/RenameCategory",
                type: "POST",
                data: {
                    id: targetId,
                    newName: $('#renameCatWrapper input#catName').val()
                },
                success: function (data) {
                    if (data.result) {
                        refreshGrid();
                        GetCategories();
                        closeModal('#renameCatWrapper');
                    }
                    showMessage(data.message);
                },
                error: function() {
                    showMessage('Something went wrong!');
                }
            });
    });

});

function GetFilesForCategory(id) {
    $.ajax({
        url: "/Categories/GetChannelByCategory",
        type: "POST",
        data: {
            catId: id
        },
        success: function (data) {
            $('#grid .row').empty();
            $('#grid .row').append(data);
        },
        error: function () {
            showMessage("Something went wrong!");
        }

    });
}

function GetCategories() {

    $(".filesCategories ul").empty();

    $.ajax({
        url: "/Categories/GetCategories",
        type: "POST",
        beforeSend: function () {
            $(".filesCategories ul").append('<li><a class="borderBottom category selected" category-id="-1" href="#">All</a></li>');
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var listItem = '<li class="categoriesListItem"><a class="category" href="#" category-id="' + data[i].id + '">' + data[i].catName + '</a><span class="renameCategory">rename</span><span class="deleteCategory">x</span></li>';
                $(".filesCategories ul").append(listItem);

            }
            $(".filesCategories ul").append('<li><a class="borderTop addNewCat" href="#">Add new</a></li>');
        }

    });
}

function showMessage(message) {
    $('#messageBox span').text(message);
    $('#messageBox').show();
    setTimeout(function () { $('#messageBox').hide("slow"); }, 2000);
}

function refreshGrid() {
    var category = $('.filesCategories .selected').attr('category-id');
    $.ajax({
        url: "/Categories/GetChannelByCategory",
        type: "POST",
        data: {
            catId: category ? category : -1
        },
        success: function (data) {
            $('#grid .row').empty();
            $('#grid .row').append(data);
        }
    });
}

function showModal(id) {
    $(id + ", #bgShadow").show();
    $("#bgShadow").animate({ opacity: 0.5 }, 400);
    $(id).animate({ opacity: 1 }, 400);
}

function closeModal() {
    $(".modalWindow, #bgShadow").animate({ opacity: 0 }, 400);
    $(".modalWindow, #bgShadow").hide();
}

function GetNewsAjax(start, offset) {
    $.ajax({
        url: "/News/GetNewsAjax",
        type: "POST",
        data: {
            start: start,
            offset: offset
        },
        success: function (data) {
            if ($('#grid .row .newsBody').length == 0) {
                $('#grid .row').append('<div class="newsBody"></div>');
                $('#grid .row .newsBody').append('<button type="button" class="btn btn-primary loadMore center-block">Load more</button>');
            }
            $('#grid .row .loadMore').before(data);
            if (data.isLast != undefined && data.isLast == true) {
                $('.loadMore').removeClass('btn-primary').addClass('btn-default').text("No more files");
            }
        }
    });
}




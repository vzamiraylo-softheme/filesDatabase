﻿$(document).ready(function () {
    var categoriesToAdd = new Array;
    
    
    GetCategories();

    $('body').on('click', '.nav-tabs li', function() {
        $('.active').removeClass('active');
        $(this).addClass('active');
    });

    $('.sharedContent').click(function() {
        $('#sharedContent').show();
        $('#myContent').hide();
    });
    
    $('.myContent').click(function () {
        $('#myContent').show();
        $('#sharedContent').hide();
    });

    $('body').on('click', '.filesCategories li a.category', function() {
        var $this = $(this);
        $('.filesCategories li a.selected').removeClass('selected');
        $this.addClass('selected');
        $.ajax({
            url: "/Home/GetChannelByCategory",
            type: "POST",
            data: {
                catId :  $this.attr('category-id')
            },
            success: function (data) {
                $('.filesCategories button:first').text($this.text());
                $('#grid').empty();
                $('#grid').append(data);
            },
            error: function (){
                showMessage("Something went wrong!");
            }

        });
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
            url: "/Home/AddNewCategory",
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

    $('body').on('click', '.deleteDoc', function() {
        if(confirm("Are you sure?")){
        $.ajax({
            url: "/Home/DeleteFile",
            type: "POST",
            data: {
                id: $(this).parent().attr('document-id')
            },
            success: function (data) {
                if(data.result)refreshGrid();
                showMessage(data.message);
            }
        });
    }
    });
    
    $('body').on('click', '#addCatToFile', function () {
            $.ajax({
                url: "/Home/AddCategoriesToFile",
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
                url: "/Home/DeleteFileFromCategory",
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
                url: "/Home/DeleteCategory",
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
                url: "/Home/RenameCategory",
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

function GetCategories() {

    $(".filesCategories ul").empty();

    $.ajax({
        url: "/Home/GetCategories",
        type: "POST",
        beforeSend: function () {
            $(".filesCategories ul").append('<li><a class="borderBottom category selected" category-id="-1" href="#">All</a></li>');
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var listItem = '<li><a class="category" href="#" category-id="' + data[i].id + '">' + data[i].catName + '</a><span class="renameCategory">rename</span><span class="deleteCategory">x</span></li>';
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
        url: "/Home/GetChannelByCategory",
        type: "POST",
        data: {
            catId: category ? category : -1
        },
        success: function (data) {
            $('#grid').empty();
            $('#grid').append(data);
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



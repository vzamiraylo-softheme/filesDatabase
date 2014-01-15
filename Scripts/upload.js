$(document).ready(function () {
    var categories = new Array;
    
    $('body').on('click', '#uploadWrapper .categoriesList li', function () {
        $(this).toggleClass('selected');

        categories = [];
        $('#uploadWrapper .categoriesList li.selected').each(function () {
            categories.push($(this).attr('category-id'));
        });
        $('#uploadWrapper input#categories').val(categories);
    });


    $('#myForm').ajaxForm({
        success:function(data) {
            $('#grid .row').prepend(data);
            closeModal('#uploadWrapper');
        },
        error: function() {
            showMessage('Something went wrong!');
        }
    });
    
    
    $("#uploadFile").click(function ()
    {
        getCategoriesList(".categoriesList");

        showModal("#uploadWrapper");
    });

    $('#bgShadow, #modalHeader span').click(function() {
        closeModal();
    });

});

function getCategoriesList(target) {
    
    $.ajax({
        url: "/Categories/GetCategories",
        type: "POST",
        success: function (data) {

            $(target + " ul").empty();

            for (var i = 0; i < data.length; i++) {
                var listItem = '<li category-id="' + data[i].id + '">' + data[i].catName + '</li>';
                $(target + " ul").prepend(listItem);

            }
        },
        error: function () {
            alert("Something went wrong!");
        }

    });
}






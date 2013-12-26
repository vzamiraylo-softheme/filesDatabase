$(document).ready(function () {
    
        $.ajax({
            url: "/Home/GetSharedContent",
            type: "POST",
            data: {
                
            },
            success: function (data) {
                
            },
            error: function () {
                showMessage("Something went wrong!");
            }

        });

});
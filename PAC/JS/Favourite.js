$(document).ready(function () {
    $("#LoveRadBtn").click(function () {
        console.log(MemberListID);
        $.ajax({
            url: "FavouriteService.asmx/AddFavourite",
            type: "POST",
            data:
            {
                MemberListID: $("#MemberListID").val(),
                AniamlListID: $("#AnimalListID").val()
            }
        });
    });
});
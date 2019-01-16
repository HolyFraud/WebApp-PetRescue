//$(document).ready(function () {
//    //var grid = $find("<%=ResultsRadgrid.ClientID%>");
//    //var MasterTable = grid.get_masterTableView();
//    //var likebtn = MasterTable.$find("<%=testbtn.ClientID%>");
//    $('#testbtn').click(function () {
//        var favourite = {};
//        var lbmember = $('<%memberid.ClientID%>').text();
//        var lbanimal = $(".well").text();
//        favourite.MemberListID = lbmember;
//        favourite.AnimalListID = lbanimal;
//        $.ajax({
//            url: 'FavouriteService.asmx/AddFavourite',
//            method: 'post',
//            contentType: 'application/json;charset=utf-8',
//            data: '{favourite:' + JSON.stringify(favourite) + '}'
//        })

//    });
//});
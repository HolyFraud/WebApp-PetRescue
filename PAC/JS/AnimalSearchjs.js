function uncheckOthers(objchkbox) {

    var objchkList = objchkbox.parentNode.parentNode.parentNode;

    var chkboxControls = objchkList.getElementsByTagName("input");
    

    for (var i = 0; i < chkboxControls.length; i++) {

        if (chkboxControls[i] != objchkbox && objchkbox.checked) {

            chkboxControls[i].checked = false;
        }
    }

}


function ShowHide(chk, chkbreed) {

    chkbreed = document.getElementById(chkbreed);

    if (chk.checked == true) {

        chkbreed.style.display = 'block';
    }
    if (chk.checked == false) {

        chkbreed.style.display = 'none';
    }

}
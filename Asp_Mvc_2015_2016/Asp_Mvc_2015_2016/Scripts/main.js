var fs = {};

$(function () {
    fs.InitCultureForm("cultureForm");
});

fs.InitCultureForm = function (formname) {
    //get Culture form
    var form = $("#" + formname);
    //set a click handler on every node in the Culture list. (scoped on form)
    $("ul.nav > li.dropdown > ul.dropdown-menu > li", form).click(function () {
        //when user clicked a node, update the hidden form field and submit.
        $("#culturevalue").val(this.id); //the id attribute in the list holds the Culture value.
        form.submit();
    });
}
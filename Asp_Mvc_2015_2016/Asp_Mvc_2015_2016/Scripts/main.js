var fs = {};

//$.validator.addMethod('date', function (value, element) {
//    alert("");
//    var d = new Date();
//    return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
//});

$.validator.addMethod('date', function (value, element) {
    //var d = new Date();
   // alert("test")
    //alert(d.toLocaleString());
    //alert(new Date(d.toLocaleTimeString(value)));
    
    return this.optional(element) || (value|""=="");
}, "Geef datum en tijd op.");


$(function () {
    fs.initCultureForm("cultureForm");

  //  $('input[type=datetime]').datepicker();
});

fs.initCultureForm = function (formname) {
    //get Culture form
    var form = $("#" + formname);
    //set a click handler on every node in the Culture list. (scoped on form)
    $("ul.nav > li.dropdown > ul.dropdown-menu > li", form).click(function () {
        //when user clicked a node, update the hidden form field and submit.
        $("#culturevalue").val(this.id); //the id attribute in the list holds the Culture value.
        form.submit();
    });
}

/* add a failed message. Errors response returned from the server are always in Json! the errormessage is stored under 'error' */
fs.modalSaveFailed = function (xhr) {    
   // alert(JSON.stringify(xhr));
    $("#ResultMessage").fadeOut()
                            .removeClass("alert-success")
                            .addClass("alert-danger")
                            .fadeIn();
    $("span#message", "#ResultMessage").html(fs.createGlyph("remove") + xhr.responseJSON.error);
}
/* Add a success message */
fs.modalSaveSuccess = function (data) {
    $("span#message", "#ResultMessage").html(fs.createGlyph("ok") + " Opslaan Voltooid!");
    $("#ResultMessage")
        .fadeOut()
        .removeClass("alert-danger")
        .addClass("alert-success")        
        .fadeIn('slow', function () {
            //hide the modal on success only! (after showing 'saved' msg.)
            $("#ModalDialog").modal('hide').one('hidden.bs.modal', function () {
                if (data && data.redirect) {
                    window.location.href = data.redirect;
                }
            });
        });    
}

/* Load Edit or Add form in a bootstrap modal window */
fs.modalSuccess = function (data) {
    //var title = $(this).data("title"); //get title from data-title attribute.
    fs.setModalData(data);
}
/* Create Bootstrap Glyph icons in Js. */
fs.createGlyph = function (name, elType) {
    elType = elType || "span";
    if (name)
        return '<' + elType + ' class="glyphicon glyphicon-' + name + '" aria-hidden="true"></' + elType + '>';
    return "";
}
//Open an empty modal with wait text...
fs.modalBegin = function () {
    var $m = $("#ModalDialog"); //Our (hidden) Modal.
    $(".modal-body", $m).html("");//clear the body in the modal window
    $(".modal-title", $m).html($(this).data("title") || ""); //Set title. (or default = "")
    $("#ResultMessage", $m).hide(); //clear messages.
    $m.modal("show"); //show modal (as loading screen)
}

//set the (retrieved ajax) data in the modal.
fs.setModalData = function(html) {
    var $m = $("#ModalDialog"); //Our (hidden) Modal.
    var $b = $(".modal-body", $m); //the body in the modal
    //var f = $(".modal-footer", m) //the footer in the modal
    //add html and transform the screen.
    //chained: add html -> hide it(!) -> Now slide in from top to bottom.
   // var $html = $(html)    
    $b.html(html).hide().slideDown();

   // $('input[type=datetime]', $b).datepicker();

    //if (title) {
    //    $(".modal-title", $m).html(title).hide().slideDown(); //set the title of the modal
    //}
    //hook focus events so we can hide messages on (re-)focus on an input
    $("input", $b).on("focus", function () {
        $("#ResultMessage").fadeOut(); //fade out alert messages.
    })
    //register all forms in modal for unobtrusive validation. (parse new html)
    //This is not done automatically when forms are called through Ajax..
    $.validator.unobtrusive.parse("#ModalDialog");
}
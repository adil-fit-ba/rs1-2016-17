
function OnPitanjeSuccess() {
    $("#response").find('.alert').html("<a href='#' class='close' data-dismiss='alert' aria-label='close' title='close'>×</a>" 
        + "<p class='text-success'>Uspješno ste postavili pitanje. Instruktor će odgovoriti uskoro.</p>");
    $("#response").show();

}

function OnOcjenaSuccess() {
    $("#response-rating").find('.alert').html("<a href='#' class='close' data-dismiss='alert' aria-label='close' title='close'>×</a>"  +
        "<p class='text-success'>Uspješno ste ocjenili kurs.</p>");
    $("#response-rating").show();

}


function OnOcjenaComplete() {
    $("#star-rating").hide()
}

function OnOcjenaFailure(response) {
    $("#response-rating").find('.alert').html("<p class='text-danger'>" + response.statusText + "</p>");
    $("#response-rating").show();
}



function OnFavoriteItemAddBegin() {

    $(this).find("#btn-fav").button("loading");
    
}

function OnFavoriteAddedSuccess() {
    $("#favorit-button-add").remove();
    $("#response-favorit").find('.alert').html("<a href='#' class='close' data-dismiss='alert' aria-label='close' title='close'>×</a>" +
        "<p class='text-success'>Uspješno ste dodali kurs u favorite.</p>");
    $("#response-favorit").show();

}



function GetURLParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');

    for (var i = 0; i < sURLVariables.length; i++)
    {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam)
        {
            return sParameterName[1];
        }
    }

}








function OnFavoriteRemoveSuccess() {

    $("#favorit-response").find('.alert').html("<a href='#' class='close' data-dismiss='alert' aria-label='close' title='close'>×</a>" 
        + "<p class='text-success text-center'>Uspješno ste uklonili kurs iz favorita.</p>");
    $("#favorit-response").show();

}


function OnMojKursRemoveSuccess(id) {

    $("#kurs-response-" + id).find('.alert').html("<a href='#' class='close' data-dismiss='alert' aria-label='close' title='close'>×</a>"
        + "<p class='text-success text-center'>Uspješno ste se odjavili sa kursa.</p>");
    $("#kurs-response-" +id).show();

}



$(function () {

    $("#kurseviSearch").autocomplete({

        source: "/FrontKurs/KurseviSearch", minLength: 2,
        select: function (event, ui) {
            //assign value back to the form element
            if (ui.item) {
                $(event.target).val(ui.item.value);
            }
            //submit the form
            $(event.target.form).submit();
        }
    });

});

$(function () {

    $('#star-rating a').mouseenter(function () {
        var $lis = $('#star-rating a').children();
        var id = $(this).index();

        $lis.removeClass("checked");
        $lis.slice(0, id).addClass("checked");
    });

});



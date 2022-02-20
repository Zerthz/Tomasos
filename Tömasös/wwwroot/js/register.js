$(document).ready(() => {
    $('#passwordConfirm').blur(() => {
        if ($('#passwordConfirm').val() == $('#password').val() && $('#password').val().length > 0) {
            $('.passwordField').css({ 'border': 'none' });
        }
        else if ($('#passwordConfirm').val() != $('#password').val()) {
            $('.passwordField').css({ 'border': '1px solid #EE5622' });
        }
    });

    

});
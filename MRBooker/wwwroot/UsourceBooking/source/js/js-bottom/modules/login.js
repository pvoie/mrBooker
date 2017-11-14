// /**
//  * Created by Paul on 11/7/2017.
//  */
$('.login .form').find('input, textarea').on('keyup blur focus', function (e) {

    var $this = $(this),
        label = $this.prev('label');

    if (e.type === 'keyup') {
        if ($this.val() === '') {
            label.removeClass('active highlight');
        } else {
            label.addClass('active highlight');
        }
    } else if (e.type === 'blur') {
        if( $this.val() === '' ) {
            label.removeClass('active highlight');
        } else {
            label.removeClass('highlight');
        }
    } else if (e.type === 'focus') {

        if( $this.val() === '' ) {
            label.removeClass('highlight');
        }
        else if( $this.val() !== '' ) {
            label.addClass('highlight');
        }
    }

});

$('.login .tab a').on('click', function (e) {

    e.preventDefault();

    $(this).parent().addClass('active');
    $(this).parent().siblings().removeClass('active');

    var target = $(this).attr('href');

    $('.login .tab-content > div').not(target).hide();

    $(target).fadeIn(600);

});

$('#password, #confirm_password').on('keyup blur focus', function () {
    var pass = $('#password').val(),
        conf = $('#confirm_password').val();
    if (pass === conf && (pass !== "") && (conf !== "")) {
        $('#signup .message').html('Matching').css('color', 'green');
        $(':input[type="submit"]').prop('disabled', false);
    } else {
        $('#signup .message').html('Not Matching').css('color', 'red');
        $(':input[type="submit"]').prop('disabled', true);
    }
});

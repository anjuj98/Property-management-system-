

//Message alert will display for 2000 milli second and after that will fade and slide up
window.setTimeout(function () {
    $(".alert").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 2000);
















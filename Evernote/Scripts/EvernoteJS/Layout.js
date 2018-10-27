//$(document).ready(function () {
//    $('[data-toggle="tooltip"]').tooltip();
//});

function Url(url) {
    debugger;
    if (window.location.pathname.substr(1).split('/').length == 3)
        url = '..' + url;
    else if (window.location.pathname.substr(1).split('/').length == 2)
        url = '..' + url;

    return url;
}

$('[data-toggle="tooltip"]').tooltip(); 
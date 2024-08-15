$(document).ready(function () {
    if (!getCookie("cookiesAccepted") && sessionStorage.getItem('cookie')!='no') {
        $("#cookieConsent").show();
    }

    $("#acceptCookies").click(function () {
        setCookie("cookiesAccepted", "true", 365);
        $("#cookieConsent").hide();
    });

    $("#declineCookies").click(function () {
        sessionStorage.setItem('cookie', 'no');
        $("#cookieConsent").hide();
    });

    function getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function setCookie(name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }
});


// For Likes!
function FilipsFunktion(id) {
    $.ajax({
        url: '/News/Laikalaininen',
        type: 'GET',
        data: { articleId: id },
        success: function (result) {
            $('#VC1337').html(result);
        },
        error: function (error) {
            alert('Houston we got a probn lem!');
        }
    });
}


// Search New Geo Location "City"
function FindNewCity(cityName) {
    $.ajax({
        url: '/Home/SearchCity',
        type: 'GET',
        data: { city: cityName },
        success: function (result) {
            $('#VCWeather').html(result);
        },
        error: function (error) {
            alert('Houston we got a HUGE probn lem!');
        }
    });
}



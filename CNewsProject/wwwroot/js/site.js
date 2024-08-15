$(document).ready(function () {
    if (!getCookie("cookiesAccepted")) {
        $("#cookieConsent").show();
    }

    $("#acceptCookies").click(function () {
        setCookie("cookiesAccepted", "true", 365);
        $("#cookieConsent").hide();
    });

    function setCookie(name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

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


function AjaxRedirect(controller, action, elem) {
    $.ajax({
        url: `/${controller}/${action}`,
        type: 'GET',
        success: function (result) {
            $('#' + elem).html(result);
            console.log(`Redirect to /${controller}/${action} success`)
        },
        error: function (error) {
            console.log('Huge error');
        }
    });
}

function TryDeleteType(id) {

    console.log("TryDeleteType has entered the chat")

    $.ajax({
        url: '/Admin/TypeHasUsers',
        type: 'GET',
        data: { id: id },
        success: function (result) {
            console.log(result);
            console.log(result.hasUsers);
            if (result.hasUsers == true) {
                console.log("result.hasUsers is realised as true");
                alert('Type could not be deleted. Active Subscriptions exists with this Type, Batman!');
            }
            else {
                console.log(`result.hasUsers was realised as ${result.hasUsers}`);
                DeleteType(id);
            }
        },
        error: function (error) {
             console.log('Brrrrrrrrrrrrrrr');
            console.log(error);
        }
    });

    function DeleteType(id) {
        $.ajax({
            url: '/Admin/DeleteType',
            type: 'GET',
            data: { id: id },
            success: function (result) {
                if (result.succeeded == true) {
                    console.log("Success");
                    AjaxRedirect("Admin", "RevokeSubTypes", "VC420");
                }
                else {
                    console.log(`result.succeeded == ${result.succeeded}`);
                }
            },
            error: function (error) {
                console.log('Brrrrrrrrrrrrrrr');
            }
        })
    }
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

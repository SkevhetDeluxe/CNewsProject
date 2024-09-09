$(document).ready(function () {
    
    // Cookies Stuff
    LoadMums();

    if (!getCookie("cookiesAccepted") && sessionStorage.getItem('cookie')!='no') {
        $("#cookieConsent").show();
        //eatCookie("Oreo", 0);
    }

    $("#acceptCookies").click(function () {
        setCookie("cookiesAccepted", "true", 365);
        $("#cookieConsent").hide();
        PlayMums();
    });

    $("#declineCookies").click(function () {
        sessionStorage.setItem('cookie', 'no');
        $("#cookieConsent").hide();
        PlayMums();
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
    function PlayMums() {
        document.getElementById("mumsaudio").play();
    }

    function LoadMums() {
        document.getElementById("mumsaudio").load();
    }

    function eatCookie(name, amount) {
        if (amount > 0) {
            for (let i = amount; i > 0; i--) {
                alert(`User used mums on ${name}. It was very effective!`);
                PlayMums();
            }
        }
        else {
            alert('No cookies left. You feel the sadness creeping up on you.');
        }
    }
    
    // Edit Account Stuff
    $(".edit-acc-btn").click(function (){
        $("#edit-acc-form").toggle();
    });
    
    $("#save-edit-acc").click(function (){
        $("#edit-acc-form").toggle();
        $("#edit-acc-confirmation").toggle();
    });
    
    
    // Deselect Accordion
    $(".accordion-button").click(function () {
        $(this).blur();
    });
    
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

// For Better Deleting SubTypes
function AjaxRedirect(controller, action, elem) {
    $.ajax({
        url: `/${controller}/${action}`,
        type: 'GET',
        success: function (result) {
            $('#' + elem).html(result);
            console.log(`Redirect to /${controller}/${action} success`);
            console.log("Reloading Listeners");
            GenerateListeners();
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




// Loading Weather ASYNC

function LoadWeather(name)
{
    console.log("Loading Weather");
    $.ajax({
        url: `/LoadViewComponent/LoadViewComponent`,
        type: 'GET',
        data: { name: name },
        success: function (result) {
            $('#VCWeather').html(result);
        },
        error(error){
            console.log(error);
        }
    })
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
            console.log('Houston we got a HUGE probn lem!');
        }
    });
}



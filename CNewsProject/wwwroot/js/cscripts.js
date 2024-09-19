function CreateAuthorDivs(containerId, authorNames) {
    let oddEven = 1;
    let jsValChangeEvent = new Event("JSValueChange");
    let container = document.getElementById(containerId);
    authorNames.forEach(function (name) {

        // COUNTING ELEMS
        let oddEven = DecideColour();

        // CREATING THE ELEMENT
        let authorDiv = document.createElement("div");
        authorDiv.classList.add("row");
        authorDiv.classList.add("authorDiv");
        authorDiv.innerHTML = `<a class='col-11' href='#'>${name}</a>`;
        authorDiv.innerHTML += "<div class='col-1 p-0 clickToDelete d-inline-flex justify-content-end c-hover-pointer'>&#x274c;</div>";
        authorDiv.innerHTML += `<input type="hidden" value="${name}">`;
        container.appendChild(authorDiv);

        // SETTING COLOURS
        if (oddEven === 1) {
            authorDiv.classList.add("newsLetterSettingsBackground");
        } else {
            authorDiv.classList.add("newsLetterSettingsBackground");
        }

        // ADDING EVENT LISTENER FOR REMOVING AND RECOLOURING
        authorDiv.getElementsByClassName("clickToDelete")[0].addEventListener("click", function () {
            let returnElem = document.getElementById("returnToAuthorArray");
            returnElem.value = authorDiv.getElementsByTagName("input")[0].value;
            returnElem.dispatchEvent(jsValChangeEvent);
            authorDiv.remove();
            let remainingElems = document.querySelectorAll(".authorDiv");

            // SETTING COLOURS FOR REMOVAL
            oddEven = DecideColourForDelete();
            remainingElems.forEach(function (elem) {
                elem.classList.remove("newsLetterSettingsBackground");
                elem.classList.remove("newsLetterSettingsBackground");
                if (oddEven === 1) {
                    oddEven = 2;
                    elem.classList.add("newsLetterSettingsBackground");
                } else {
                    oddEven = 1;
                    elem.classList.add("newsLetterSettingsBackground");
                }
            });
        });
    });

    // RETURNING ODD OR EVEN
    return oddEven;
}

function CreateSingleAuthorDiv(containerId, name) {

    let jsValChangeEvent = new Event("JSValueChange");
    let container = document.getElementById(containerId);

    // COUNTING ELEMS
    let oddEven = DecideColour();

    // CREATING THE ELEMENT
    let authorDiv = document.createElement("div");
    authorDiv.classList.add("row");
    authorDiv.classList.add("authorDiv");
    authorDiv.innerHTML = `<a class='col-11' href='#'>${name}</a>`;
    authorDiv.innerHTML += "<div class='col-1 p-0 clickToDelete d-inline-flex justify-content-end c-hover-pointer'>&#x274c;</div>";
    authorDiv.innerHTML += `<input type="hidden" value="${name}">`;
    container.appendChild(authorDiv);
    if (oddEven === 1) {
        authorDiv.classList.add("newsLetterSettingsBackground");
    } else {
        authorDiv.classList.add("newsLetterSettingsBackground");
    }

    // ADDING EVENT LISTENER FOR REMOVING AND RECOLOURING
    authorDiv.getElementsByClassName("clickToDelete")[0].addEventListener("click", function () {
        let returnElem = document.getElementById("returnToAuthorArray");
        returnElem.value = authorDiv.getElementsByTagName("input")[0].value;
        returnElem.dispatchEvent(jsValChangeEvent);
        authorDiv.remove();
        let remainingElems = document.querySelectorAll(".authorDiv");

        // SETTING COLOURS FOR REMOVAL
        oddEven = DecideColourForDelete();
        remainingElems.forEach(function (elem) {
            elem.classList.remove("newsLetterSettingsBackground");
            elem.classList.remove("newsLetterSettingsBackground");
            if (oddEven === 1) {
                oddEven = 2;
                elem.classList.add("newsLetterSettingsBackground");
            } else {
                oddEven = 1;
                elem.classList.add("newsLetterSettingsBackground");
            }
        });
    });

    // RETURNING ODD OR EVEN
    return oddEven;
}

function DecideColour() {
    let oddOrEven = 0;
    let elemsInAuthorContainer = document.querySelectorAll(".authorDiv");
    console.log(elemsInAuthorContainer)

    elemsInAuthorContainer.forEach(function (elem) {
        oddOrEven++;
    })
    oddOrEven = (oddOrEven % 2) + 1;
    console.log(oddOrEven);

    return oddOrEven;
}

function DecideColourForDelete() {
    let oddOrEven = 0;
    let elemsInAuthorContainer = document.querySelectorAll(".authorDiv");
    console.log(elemsInAuthorContainer)

    elemsInAuthorContainer.forEach(function (elem) {
        oddOrEven++;
    })
    oddOrEven = (oddOrEven % 1) + 1;
    console.log(oddOrEven);

    return oddOrEven;
}

function GetAuthorDivValues(){
    let returnString = "NO AUTHORS";
    let changed = false;

    document.querySelectorAll(".authorDiv").forEach(function (elem) {
        if(changed === false){
            returnString = elem.getElementsByTagName("input")[0].value;
            changed = true;
        }
        else{
            returnString += ',' + elem.getElementsByTagName("input")[0].value;
        }
    })
    return returnString;
}

function StringifyArrayValues(array) {
    console.log(array);
    let stringifiedResult = "";
    let firstIteration = true;
    array.forEach(function (item) {
        console.log("IN ARRAY: " + item);
        if (firstIteration === true) {
            stringifiedResult = String(item);
            firstIteration = false;
        } else {
            stringifiedResult += ("," +String(item));
        }
    });
    console.log("STRINGIFIED ARRAY"); console.log(stringifiedResult);
    return stringifiedResult;
}

// REQUIREMENTS
//
// THE ELEMENT TO BE REMOVED
function RemoveDiv(div) {
    div.remove();
}

// RETURNS if CONTAINS and Index of Where
//
// REQUIRES TWO STRINGS
function StringHas(baseString, subString) {
    let startChar = Array.from(subString)[0];
    let startCursor = 0;
    let changed = false;

    for (let i = 0; i < baseString.length; i++) {
        if (baseString[i] === startChar) {
            if (changed === false) {
                startCursor = i;
                changed = true;
            }
        }
    }

    if (subString.length !== 1) {
        if (changed === true) {
            let j = 1;
            for (let i = startCursor + 1; i < startCursor + subString.length; i++) {

                if (Array.from(baseString)[i] !== Array.from(subString)[j]) {
                    changed = false;
                }
                j++;
            }
        }
    }

    return [changed, startCursor];
}

// REQUIREMENTS
//
// Add class .c-clickable-toggle to ACTIVATION ELEMENT
// 
// Add class .c-clickable-toggle-target to TARGET ELEMENT
function AddToggleableElementClass() {
    document.addEventListener("DOMContentLoaded", function () {
        const hitBox = document.querySelector(".c-clickable-toggle");
        const target = document.querySelector(".c-clickable-toggle-target");

        hitBox.addEventListener("click", function () {
            console.log("Clicked", this);
            if (target.classList.contains("c-hidden")) {
                target.classList.remove("c-hidden");
            } else {
                target.classList.add("c-hidden");
            }
        });
    });
}

// REQUIREMENTS
//
// ID from Clickable Element
//
// ID from Target Element
function AddToggleableElementId(hitBoxId, targetId) {
    document.addEventListener("DOMContentLoaded", function () {
        const hitBox = document.getElementById(hitBoxId);
        const target = document.getElementById(targetId);

        hitBox.addEventListener("click", function () {
            console.log("Clicked", this);
            if (target.classList.contains("c-hidden")) {
                target.classList.remove("c-hidden");
            } else {
                target.classList.add("c-hidden");
            }
        });
    });
}


// OBSERVE
//
// This checks for type of as well. 
// Comparing string array to number item will give false
function arrayContains(array, item) {

    for (let i = 0; i < array.length; i++) {
        console.log(`Comparing ${typeof (array[i])} : ${array[i]} with ${typeof (item)} : ${item}`);
        if (array[i] === item) {
            return true
        }
    }
    return false;
}

function CreateLoadingScreen(){
    let body = document.getElementById("cBodyId");
    let loadingScreen = document.createElement("div");
    loadingScreen.classList.add("c-nonselectable")
    loadingScreen.classList.add("c-loading-screen");
    loadingScreen.innerHTML = "<img class=\"c-loading-scream c-nonselectable\" src=\"/Content/svg/loadingscream/loadingScream.svg\" alt=\"Monster Loader\" width=\"31%\"/>";
    body.insertBefore(loadingScreen, body.firstChild);
}

function RemoveLoadingScreen(){
    let loadingScreen = document.getElementsByClassName("c-loading-screen");
    console.log(loadingScreen);
    for (let i = 0; i < loadingScreen.length; i++) {
        loadingScreen[i].remove();
    }
}
function UpdateNewsLetterSetting() {
    let body = document.getElementById("cBodyId");
    let loadingScreen = document.createElement("div");
    loadingScreen.classList.add("c-loading-screen");
    loadingScreen.innerHTML = "<img class=\"c-loading-scream\" src=\"/Content/svg/loadingscream/loadingScream.svg\" alt=\"Monster Loader\" width=\"31%\"/>";
    body.appendChild(loadingScreen);

    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            // Typical action to be performed when the document is ready:
            document.getElementById("demo").innerHTML = xhttp.responseText;
        }
    }
    xhttp.open("POST", "/Content/" + xhttp.responseURL, true);
    loadingScreen.remove();
}

function AjaxUpdateNewsLetterSetting(userSetting, authorNames){console.log(userSetting);
    console.log(authorNames);
    $.ajax({
        url:"/Account/UpdateNewsLetterSetting/",
        type:"POST",
        data: { jsonSetting: userSetting, authorNames: authorNames },
        beforeSend: function () {
            CreateLoadingScreen();
        },
        complete: function () {
            RemoveLoadingScreen();
        },
        success: function(result) {
            if(result.success == true){
                let successElem = document.getElementById("SuccessMessageSpot");
                successElem.innerHTML = "<div class=\"border-success border border-5 border-opacity-75\">\n" +
                    "<p style=\"color: springgreen\">Successfully updated profile</p>\n" +
                    "</div>";
            }
            else{
                let successElem = document.getElementById("SuccessMessageSpot");
                successElem.innerHTML = "<div class=\"border-danger border border-5 border-opacity-75\">\n" +
                    "<p style=\"color: darkred\">Successfully NOT updated profile</p>\n" +
                    "</div>";
            }
        },
        error: function(result) {
            let successElem = document.getElementById("SuccessMessageSpot");
            successElem.innerHTML = "<div class=\"border-danger\">\n" +
                "<p style=\"color: darkred\">ERROR ERROR ERROR ERROR ERROR ERROR ERROR</p>\n" +
                "</div>";
            RemoveLoadingScreen();
        }
    })
}



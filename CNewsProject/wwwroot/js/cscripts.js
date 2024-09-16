function CreateAuthorDivs(containerId, authorNames, oddEven) {
    let container = document.getElementById(containerId);
    authorNames.forEach(function (name) {

        // CREATING THE ELEMENT
        let authorDiv = document.createElement("div");
        authorDiv.classList.add("row");
        authorDiv.classList.add("authorDiv");
        authorDiv.innerHTML = `<a class='col-11' href='#'>${name}</a>`;
        authorDiv.innerHTML += "<div class='col-1 p-0 clickToDelete d-inline-flex justify-content-end c-hover-pointer'>&#x274c;</div>";
        authorDiv.innerHTML += `<input type="hidden" value="${name}">`;
        container.appendChild(authorDiv);
        if (oddEven === 1) {
            oddEven = 2;
            authorDiv.classList.add("bg-primary-subtle");
        } else {
            oddEven = 1;
            authorDiv.classList.add("bg-secondary-subtle");
        }

        // ADDING EVENT LISTENER FOR REMOVING AND RECOLOURING
        authorDiv.getElementsByClassName("clickToDelete")[0].addEventListener("click", function () {
            authorDiv.remove();
            let remainingElems = document.querySelectorAll(".authorDiv");
            oddEven = 1;
            remainingElems.forEach(function (elem) {
                elem.classList.remove("bg-primary-subtle");
                elem.classList.remove("bg-secondary-subtle");
                if (oddEven === 1) {
                    oddEven = 2;
                    elem.classList.add("bg-primary-subtle");
                } else {
                    oddEven = 1;
                    elem.classList.add("bg-secondary-subtle");
                }
            });
        });
    });

    // RETURNING ODD OR EVEN
    return oddEven;
}

function CreateSingleAuthorDiv(containerId, name, oddEven) {
    let container = document.getElementById(containerId);

    // CREATING THE ELEMENT
    let authorDiv = document.createElement("div");
    authorDiv.classList.add("row");
    authorDiv.classList.add("authorDiv");
    authorDiv.innerHTML = `<a class='col-11' href='#'>${name}</a>`;
    authorDiv.innerHTML += "<div class='col-1 p-0 clickToDelete d-inline-flex justify-content-end c-hover-pointer'>&#x274c;</div>";
    authorDiv.innerHTML += `<input type="hidden" value="${name}">`;
    container.appendChild(authorDiv);
    if (oddEven === 1) {
        oddEven = 2;
        authorDiv.classList.add("bg-primary-subtle");
    } else {
        oddEven = 1;
        authorDiv.classList.add("bg-secondary-subtle");
    }

    // ADDING EVENT LISTENER FOR REMOVING AND RECOLOURING
    authorDiv.getElementsByClassName("clickToDelete")[0].addEventListener("click", function () {
        authorDiv.remove();
        let remainingElems = document.querySelectorAll(".authorDiv");
        oddEven = 1;
        remainingElems.forEach(function (elem) {
            elem.classList.remove("bg-primary-subtle");
            elem.classList.remove("bg-secondary-subtle");
            if (oddEven === 1) {
                oddEven = 2;
                elem.classList.add("bg-primary-subtle");
            } else {
                oddEven = 1;
                elem.classList.add("bg-secondary-subtle");
            }
        });
    });
    
    // RETURNING ODD OR EVEN
    return oddEven;
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
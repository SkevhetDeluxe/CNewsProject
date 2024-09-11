

// REQUIREMENTS
//
// Add class=".c-clickable-toggle" to ACTIVATION ELEMENT
// 
// Add class=".c-clickable-toggle-target" to TARGET ELEMENT
function AddToggleableElementClass(){
    document.addEventListener("DOMContentLoaded", function () {
        const hitBox = document.querySelector(".c-clickable-toggle");
        const target = document.querySelector(".c-clickable-toggle-target");

        hitBox.addEventListener("click", function (){
            console.log("Clicked", this);
            if (target.classList.contains("c-hidden")){
                target.classList.remove("c-hidden");
            }
            else{
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
function AddToggleableElementId(hitBoxId, targetId){
    document.addEventListener("DOMContentLoaded", function () {
        const hitBox = document.getElementById(hitBoxId);
        const target = document.getElementById(targetId);

        hitBox.addEventListener("click", function (){
            console.log("Clicked", this);
            if (target.classList.contains("c-hidden")){
                target.classList.remove("c-hidden");
            }
            else{
                target.classList.add("c-hidden");
            }
        });
    });
}


// OBSERVE
//
// This checks for type of as well. 
// Comparing string array to number item will give false
function arrayContains(array, item){

    for (let i = 0; i < array.length; i++){
        console.log(`Comparing ${typeof(array[i])} : ${array[i]} with ${typeof(item)} : ${item}`);
        if (array[i] === item){
            return true
        }
    }
    return false;
}
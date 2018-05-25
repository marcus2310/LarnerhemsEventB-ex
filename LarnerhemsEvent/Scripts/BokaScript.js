var items = [];



function moveForw1(id) {

    items.push(id);
    console.log(items);

    var elem = document.getElementById("bar-boka");
    var width1 = "33.2%";
    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[0].style.backgroundColor = "#02b875";
        tablinks[1].style.backgroundColor = "white";
        tablinks[0].disabled = true;
    }

    openPage('steg2', this, 'white');


}
function moveForw2(id) {

    items.push(id);
    console.log(items);

    var elem = document.getElementById("bar-boka");
    var width1 = "50%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[1].style.backgroundColor = "#02b875";
        tablinks[2].style.backgroundColor = "white";
        tablinks[1].disabled = true;
    }

    openPage('steg3', this, 'white');

}
function moveForw3() {
    var elem = document.getElementById("bar-boka");
    var width1 = "66.6%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[2].style.backgroundColor = "#02b875";
        tablinks[3].style.backgroundColor = "white";
        tablinks[2].disabled = true;
    }

    openPage('steg4', this, 'white');
}
function moveForw4() {
    var elem = document.getElementById("bar-boka");
    var width1 = "83.2%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[3].style.backgroundColor = "#02b875";
        tablinks[4].style.backgroundColor = "white";
        tablinks[3].disabled = true;
    }

    openPage('steg5', this, 'white');

}
function moveForw5() {
    var elem = document.getElementById("bar-boka");
    var width1 = "100%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[4].style.backgroundColor = "#02b875";
        tablinks[5].style.backgroundColor = "white";
        tablinks[4].disabled = true;
    }

    openPage('steg6', this, 'white');

}
function moveBack1() {
    var elem = document.getElementById("bar-boka");
    var width1 = "16.6%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[1].style.backgroundColor = "#ebebeb";
        tablinks[0].style.backgroundColor = "white";
        tablinks[1].disabled = true;
    }

    openPage('steg1', this, 'white');


}
function moveBack2() {
    var elem = document.getElementById("bar-boka");
    var width1 = "33.2%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[2].style.backgroundColor = "#ebebeb";
        tablinks[1].style.backgroundColor = "white";
        tablinks[2].disabled = true;
    }

    openPage('steg2', this, 'white');

}
function moveBack3() {
    var elem = document.getElementById("bar-boka");
    var width1 = "50%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[3].style.backgroundColor = "#ebebeb";
        tablinks[2].style.backgroundColor = "white";
        tablinks[3].disabled = true;
    }

    openPage('steg3', this, 'white');

}
function moveBack4() {
    var elem = document.getElementById("bar-boka");
    var width1 = "66.6%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[4].style.backgroundColor = "#ebebeb";
        tablinks[3].style.backgroundColor = "white";
        tablinks[4].disabled = true;
    }

    openPage('steg4', this, 'white');

}
function moveBack5() {
    var elem = document.getElementById("bar-boka");
    var width1 = "83.2%";

    elem.style.width = width1;
    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[5].style.backgroundColor = "#ebebeb";
        tablinks[4].style.backgroundColor = "white";
        tablinks[5].disabled = true;
    }

    openPage('steg5', this, 'white');

}
 
function openPage(pageName, elmnt, color) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontentBoka");

    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "";
    }

    tablinks = document.getElementsByClassName("tablinkBoka");
    for (i = 0; i < tablinks.length; i++) {
        //tablinks[i].style.backgroundColor = "";
    }
    document.getElementById(pageName).style.display = "block";
    elmnt.style.backgroundColor = color;

}



// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();
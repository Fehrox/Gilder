function scrollToElement(elementId){
    var element = document.getElementById(elementId);
    element.scrollIntoView({behavior: "smooth", block: "center", inline: "nearest"});   
}
var addVideoBtn = document.getElementById("AddVideoBtn");
addVideoBtn.addEventListener("click", function () {
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status === 200) {
            window.location.href = "/AddVideo"; // Replace "/AddVideo" with the actual URL or route to your AddVideo page
        }
    };
    http.open("GET", "/Privacy?handler=AddVideo");
    http.send();
});

var videoContainer = document.getElementById("videoContainer");
console.log(videoContainer);
document.addEventListener("DOMContentLoaded", function () {
    let xhttp = new XMLHttpRequest();
    xhttp.onload = function () {
        if (this.status === 200) {
            let result = this.response;
        }
    }
    xhttp.open("GET", "/VideoCollection?handler=SelectVideos");
    xhttp.send();
});
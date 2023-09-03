var videoEl = document.getElementById("videoEl");
videoEl.style.width = "364px";
videoEl.style.borderRadius = "5px";

var titleInput = document.getElementById("titleInput");
var textAreaInput = document.getElementById("textAreaInput");

var globalQueryIdValue;

window.addEventListener("load", function () {
    var queryValues = splitQueryParams();
    var stringQueryValue = JSON.stringify(queryValues);
    var encodedQueryValue = encodeURIComponent(stringQueryValue);
    var idInput = document.getElementById("idInput");
    console.log(idInput);
    globalQueryIdValue = encodedQueryValue;

    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            var reponse = JSON.parse(this.response);
            idInput.value = reponse.id;
            videoEl.src = reponse.filePath;
            titleInput.value = reponse.title;
            textAreaInput.value = reponse.description;
            console.log(reponse);
        }
    }
    http.open("GET", `/EditVideo?handler=DoSomething&id=${encodedQueryValue}`);
    http.send();
});
function splitQueryParams() {
    // Get the query string from the current URL
    var queryString = window.location.search;

    // Remove the leading "?" character
    queryString = queryString.substring(1);

    // Split the query string into an array of key-value pairs
    var paramsArray = queryString.split("&");

    // Create an empty object to store the parameters
    var params = {};

    // Iterate over the key-value pairs and store them in the object

    for (param of paramsArray) {
        var paramParts = param.split("=");
        var key = decodeURIComponent(paramParts[0]);
        var value = decodeURIComponent(paramParts[1]);
        params[key] = value;
    }

    // Retrieve the value of a specific parameter
    const Id = {};
    Id.id = params.id;
    return Id;
}

var saveChangesBtn = document.getElementById("saveChangesBtn");

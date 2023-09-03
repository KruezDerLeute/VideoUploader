function isYoutubeLink(url) {
    var youtubeRegex = /^(https?:\/\/)?(www\.)?youtube\.com\/watch\?v=([a-zA-Z0-9_-]{11})/;
    return youtubeRegex.test(url);
}

const errorMessage = document.getElementById("errorMessage");
const saveBtn = document.getElementById("saveBtn");
const linkInput = document.getElementById("linkInput");
linkInput.addEventListener("change", function () {
    var value = linkInput.value;
    if (!isYoutubeLink(value)) {
        errorMessage.style.display = "block";
        errorMessage.innerHTML = "Та Youtube-ийн холбоос оруулсан эсэхээ шалгана уу";
        errorMessage.style.color = "red";

        saveBtn.disabled = true;
    } else {
        errorMessage.style.display = "none";
        errorMessage.innerHTML = "";
        saveBtn.disabled = false;
    }
});
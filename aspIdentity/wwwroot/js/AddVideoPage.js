var VideoFileInput = document.getElementById("VideoFileInput");
var fileSizeShower = document.getElementById("fileSizeShower");
var saveBtn = document.getElementById("saveBtn");
var fileAdditionalInfo = document.getElementById("fileAdditionalInfo");
var isVideoFile = document.getElementById("isVideoFile");

VideoFileInput.type = "file";

VideoFileInput.addEventListener("change", function () {
    var file = VideoFileInput.files[0];
    if (file) {
        console.log("File name: " + file.name);
        if (checkFileType(file)) {
            isVideoFile.innerHTML = "";
            isVideoFile.style.display = "none";
            saveBtn.disabled = false;
            if (file.size >= 104857600) {
                var message = "Таны оруулсан файл 100мб-аас их хэмжээтэй байна.";
                fileSizeShower.innerHTML = `Файлны хэмжээ: ${FormattedFileSize(file.size)}`;
                fileSizeShower.style.display = "block";
                fileSizeShower.style.color = "red";

                fileAdditionalInfo.innerHTML = message;
                fileAdditionalInfo.style.color = "red";
                fileAdditionalInfo.style.display = "block";

                saveBtn.disabled = true;

            } else if (file.size <= 104857600) {
                isVideoFile.innerHTML = "";
                fileSizeShower.innerHTML = `Файлны хэмжээ: ${FormattedFileSize(file.size)}`;
                fileSizeShower.style.display = "block";
                fileSizeShower.style.color = "green";

                fileAdditionalInfo.innerHTML = "";
                fileAdditionalInfo.style.display = "none";
            }
        }
        else {
            isVideoFile.innerHTML = "Та mp4, mov, mkv, webm өргөтгөлтэй файл сонгоно уу";
            isVideoFile.style.display = "block";
            isVideoFile.style.color = "red";
            saveBtn.disabled = true;
        }
    }
});

function FormattedFileSize(bytes) {
    var suffixes = ["b", "kb", "mb", "gb", "tb"];
    var suffixIndex = 0;
    var fileSize = bytes;

    while (fileSize >= 1024 && suffixIndex < suffixes.length - 1) {
        fileSize /= 1024;
        suffixIndex++;
    }
    return fileSize.toFixed(2) + " " + suffixes[suffixIndex];
}

function checkFileType(file) {
    var isVideoFile = true;
    var fileName = file.name;
    var fileExtension = fileName.split(".").pop().toLowerCase();

    const allowedExtionsions = ["mp4", "mov", "mkv", "webm"];

    if (!allowedExtionsions.includes(fileExtension)) {
        isVideoFile = false;
    } else {
        isVideoFile = true;
    }
    return isVideoFile;
}
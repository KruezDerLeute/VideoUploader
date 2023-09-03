const videoContainer = document.getElementById("videoContainer");
const inputAndBtn = document.getElementById("inputAndBtn");
const videoInfoSection = document.getElementById("videoInfoSection");
const idContainer = document.getElementById("idContainer");
const youtubeLinkChecker = document.getElementById("youtubeLinkChecker");
const commentSection = document.getElementById("commentSection");
const commentBtn = document.getElementById("commentBtn");
const commentInput = document.getElementById("commentInput");
const EditingSection = document.getElementById("EditingSection");

var videoPlayer;

var inComingJson = {};

const TitleEl = document.getElementById("TitleEl");
const DescriptionEl = document.getElementById("DescriptionEl");
var globalEncodedJsonVideoOrLink = "";
window.addEventListener("load", function () {
    var videoOrLinkObj = splitQueryParams();
    var jsonVideoOrLink = JSON.stringify(videoOrLinkObj);
    var encodedJsonVideoOrLink = encodeURIComponent(jsonVideoOrLink);
    globalEncodedJsonVideoOrLink = encodedJsonVideoOrLink;

    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            var jsonResponse = JSON.parse(this.response);

            var videoObj = jsonResponse.video;
            var commentObjs = jsonResponse.comments;

            console.log(youtubeLinkChecker.value);

            loadComments(commentObjs);

            console.log(videoObj);
            console.log(commentObjs);

            idContainer.value = videoObj.id;

            inComingJson.id = videoObj.id;
            inComingJson.title = videoObj.title;
            inComingJson.description = videoObj.description;
            inComingJson.publishedDate = videoObj.createdDate;
            
            if (jsonResponse.video.linkId != undefined) {
                inComingJson.youtubeLink = `https://www.youtube.com/embed/${videoObj.linkId}`;
                inComingJson.isYoutubeLink = true;
                youtubeLinkChecker.value = inComingJson.isYoutubeLink;
            } else if (jsonResponse.youtubeVidId == undefined) {
                inComingJson.filePath = videoObj.filePath;
                inComingJson.isYoutubeLink = false;
                youtubeLinkChecker.value = inComingJson.isYoutubeLink;
            }

            if (videoOrLinkObj.youtubeLink != null) {
                videoPlayer = document.createElement("iframe");
                videoPlayer.id = "videoPlayer";
                videoPlayer.src = inComingJson.youtubeLink;
                videoPlayer.width = "900px;"
                videoPlayer.height = "500px";
                videoPlayer.style.borderRadius = "5px";
                TitleEl.innerHTML = inComingJson.title;
                DescriptionEl.innerHTML = inComingJson.description;
            } else {
                videoPlayer = document.createElement("video");
                videoPlayer.id = "videoPlayer";
                videoPlayer.style.width = "900px";
                videoPlayer.style.justifySelf = "center";
                videoPlayer.src = videoObj.filePath;
                videoPlayer.style.borderRadius = "5px"; 
                videoPlayer.controls = true;
                TitleEl.innerHTML = videoObj.title;
                DescriptionEl.innerHTML = videoObj.description;
            }

            videoContainer.insertBefore(videoPlayer, videoInfoSection);

            console.log(`inComingJson:------------------`);
            console.log(inComingJson);
        }
    }
    http.open("GET", `/VideoPlayer?handler=LoadVideo&data=${encodedJsonVideoOrLink}`);
    http.send();
});
commentBtn.addEventListener("click", function () {
    var requestString = "";
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            console.log(this.response);
        }
    }
    http.open("GET", `/VideoPlayer?handler=AddComment&data=${globalEncodedJsonVideoOrLink}&idContainer=${idContainer.value}&commentInput=${commentInput.value}&isYoutubeLink=${youtubeLinkChecker.value}`);
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
    var videoOrLink = {};
    videoOrLink.id = params.id;
    videoOrLink.youtubeLink = params.youtubeLink;

    return videoOrLink;
}

var deleteBtn = document.getElementById("deleteBtn");
deleteBtn.addEventListener("click", function () {
    var data = {};
    data.id = inComingJson.id;
    if (inComingJson.youtubeLink == undefined || inComingJson.youtubeLink == null) {
        data.isYoutubeLink = false;
    } else if (inComingJson.youtubeLink != undefined || inComingJson.youtubeLink != null) {
        data.isYoutubeLink = true;
        data.youtubeLink = inComingJson.youtubeLink;
    }
    var stringData = JSON.stringify(data);
    var encodedData = encodeURIComponent(stringData);

    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            window.location.href = `/VideoCollection`;
        }
    }
    http.open("GET", `/VideoPlayer?handler=Delete&data=${encodedData}`);
    http.send();
});

const editBtn = document.getElementById("editBtn");
editBtn.addEventListener("click", function () {
    var requestString;
    var locationString;
    if (inComingJson.youtubeLink == null || inComingJson.youtubeLink == undefined || inComingJson.youtubeLink == "") {
        requestString = "/VideoPlayer?handler=RedirectToEditVideo";
        locationString = `/EditVideo?id=${inComingJson.id}`;
    } else {
        requestString = "/EditLink?handler=RedirectToEditLink";
        locationString = `/EditLink?id=${inComingJson.id}&youtubeLink${inComingJson.youtubeLink}`;
    }
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            window.location.href = locationString;
        }
    }
    http.open("GET", requestString);
    http.send();
});

function loadComments(commentObjArray = []) {
    for (var commentObj of commentObjArray) {
        const br = document.createElement("br");
        const commentContainer = document.createElement("div");
        commentContainer.style.border = "solid 1px grey";
        commentContainer.style.borderRadius = "10px";
        commentContainer.id = "commentContainer";
        commentContainer.style.marginTop = "20px";
        commentContainer.style.marginBottom = "20px";

        const userLabel = document.createElement("label");
        userLabel.id = "UserLabel";
        userLabel.innerHTML = commentObj.authorName;
        userLabel.style.fontSize = "19px";
        userLabel.style.fontWeight = "600";
        userLabel.style.marginLeft = "10px";

        const comment = document.createElement("label");
        comment.id = "comment"; 
        comment.innerHTML = commentObj.data;
        comment.style.marginLeft = "10px";
        comment.style.marginBottom = "3px";
        comment.style.fontSize = "17px";

        commentContainer.append(userLabel);
        commentContainer.append(br);
        commentContainer.append(comment);
        commentSection.append(commentContainer);
    }
}

const videosContainer = document.getElementById("videosContainer");

const handleResponsive = () => {
    videoPlayer = document.getElementById("videoPlayer");

    if (!videoPlayer) {
        return;
    }

    if (window.innerWidth <= 450) {
        // Window width <= 450px
        videoContainer.style.width = "430px";
        videoPlayer.style.width = "430px";
        videoInfoSection.style.width = "430px";
        inputAndBtn.style.width = "430px";
        EditingSection.style.width = "430px";
        commentInput.style.width = "430px";
        commentSection.style.width = "430px";
    } else if (window.innerWidth <= 630) {
        videoContainer.style.width = "490px";
        videoPlayer.style.width = "490px";
        videoInfoSection.style.width = "490px";
        inputAndBtn.style.width = "490px";
        EditingSection.style.width = "490px";
        commentInput.style.width = "490px";
        commentSection.style.width = "490px";
    } else if (window.innerWidth <= 917) {
        // 450px < Window width <= 992px
        videoPlayer.style.width = "600px";
        videoContainer.style.width = "600px";
        videoPlayer.style.width = "600px";
        videoInfoSection.style.width = "600px";
        inputAndBtn.style.width = "600px";
        EditingSection.style.width = "600px";
        commentInput.style.width = "600px";
        commentSection.style.width = "600px";
    } else {
        // Window width > 992px
        videoContainer.style.width = "900px";
        videoPlayer.style.width = "900px";
        videoInfoSection.style.width = "900px";
        inputAndBtn.style.width = "900px";
        EditingSection.style.width = "900px";
        commentInput.style.width = "900px";
        commentSection.style.width = "900px";
    }
};
const addEventListenerAfterPageLoad = () => {
    handleResponsive();
    window.addEventListener("resize", handleResponsive);
};

if (
    document.readyState === "complete" ||
    (document.readyState !== "loading" && !document.documentElement.doScroll)
) {
    addEventListenerAfterPageLoad();
} else {
    window.onload = addEventListenerAfterPageLoad;
}
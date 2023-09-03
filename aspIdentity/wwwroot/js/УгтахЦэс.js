const videosCollection = document.getElementById("videosCollection");
videosCollection.style.marginTop = "50px";
videosCollection.style.marginBottom = "50px";
videosCollection.style.display = "grid";
videosCollection.style.gridTemplateColumns = "repeat(3, 307px)";
videosCollection.style.gridRowGap = "20px";
videosCollection.style.justifyContent = "space-between";
const initialColumns = 3;


//const videosContainer = document.getElementById("videosContainer");
//videosContainer.style.marginTop = "50px";
//videosContainer.style.marginBottom = "50px";
//videosContainer.style.display = "grid";
//videosContainer.style.gridTemplateColumns = "repeat(3, 307px)";
//videosContainer.style.gridRowGap = "20px";
//videosContainer.style.justifyContent = "space-between";
//const initialColumns = 3;

const handleResponsive = () => {
    if (window.innerWidth <= 450) {
        // Window width <= 450px
        videosCollection.style.gridTemplateColumns = "repeat(1, 307px)";
        videosCollection.style.justifyContent = "center";
        videosCollection.style.alignContent = "space-around";
    } else if (window.innerWidth <= 769) {
        videosCollection.style.gridTemplateColumns = "repeat(1, 307px)"
        videosCollection.style.justifyContent = "center";
        videosCollection.style.alignContent = "space-around";
    } else if (window.innerWidth <= 1009) {
        // 450px < Window width <= 992px
        videosCollection.style.gridTemplateColumns = "repeat(2, 307px)";
        videosCollection.style.justifyContent = "space-between";
        videosCollection.style.alignContent = "space-around";
    } else if (window.innerWidth <= 1170) {
        videosCollection.style.gridTemplateColumns = `repeat(${initialColumns}, 307px)`;
        videosCollection.style.gridColumnGap = "20px";
        videosCollection.style.alignContent = "space-around";
    }

    else {
        // Window width > 992px
        videosCollection.style.gridTemplateColumns = `repeat(${initialColumns}, 307px)`;
        videosCollection.style.justifyContent = "space-between";
        videosCollection.style.alignContent = "space-around";
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

function addVideoInVideosContainer(video = {
    createdDate: "",
    description: "",
    filePath: "",
    id: "",
    title: "",
    author: ""
}) { 
    const authorNameP = document.createElement("p");
    authorNameP.innerHTML = `бичлэг нийтэлсэн: ${video.author}`;
    authorNameP.style.marginTop = "1px";

    const videoContainer = document.createElement("div");
    videoContainer.style.width = "307px";
    videoContainer.className = "videoContainer";
    videoContainer.style.className = "videoContainer";

    const blackBackGround = document.createElement("div");
    blackBackGround.className = "blackBackGround";
    blackBackGround.style.height = "201px";
    blackBackGround.style.width = "306px";
    blackBackGround.style.backgroundColor = "black";
    blackBackGround.style.borderTopLeftRadius = "10px";
    blackBackGround.style.borderTopRightRadius = "10px";

    const videoBackground = document.createElement("div");
    videoBackground.className = "videoBackground";
    videoBackground.style.backgroundColor = "white";
    videoBackground.style.borderRadius = "10px";
    videoBackground.style.backgroundColor = "white";
    videoBackground.style.border = "solid 1px grey";
    videoBackground.style.height = "240px";

    videoContainer.append(videoBackground);
    videoBackground.append(blackBackGround);

    const p = document.createElement("p");
    p.innerHTML = video.title;
    p.style.textAlign = "center";
    p.style.padding = "5px";

    videoBackground.append(p);
    videoContainer.appendChild(authorNameP);

    const Video = document.createElement("video");
    Video.style.borderTopLeftRadius = "10px";
    Video.style.borderTopRightRadius = "10px";
    Video.style.height = "200px";
    Video.style.width = "306px";
    Video.src = video.filePath;
    Video.controls = false;
    Video.addEventListener("click", function () {
        var xhttp = new XMLHttpRequest();
        xhttp.open("GET", "/VideoCollection?handler=VideoPlayer");
        xhttp.send();
        xhttp.onload = function () {
            if (this.status == 200) {
                window.location.href = "/VideoPlayer?id=" + video.id;
            }
        }
    });

    blackBackGround.append(Video);
    videoContainer.appendChild(videoBackground);
    videosCollection.append(videoContainer);
}

function addLinkInVideosContainer(link = {
    id: "",
    youtubeVidId: "",
    youtubeLink: "",
    title: "",
    createdDate: "",
    modifiedDate: "",
    author: ""
}) {
    const authorNameP = document.createElement("p");
    authorNameP.innerHTML = `холбоос нийтэлсэн: ${link.author}`;
    authorNameP.style.marginTop = "1px";

    const videoContainer = document.createElement("div");
    videoContainer.style.width = "307px";
    videoContainer.className = "videoContainer";
    videoContainer.style.className = "videoContainer";

    const blackBackGround = document.createElement("div");
    blackBackGround.className = "blackBackGround";
    blackBackGround.style.height = "201px";
    blackBackGround.style.width = "306px";
    blackBackGround.style.backgroundColor = "black";
    blackBackGround.style.borderTopLeftRadius = "10px";
    blackBackGround.style.borderTopRightRadius = "10px";
    blackBackGround.style.display = "flex";
    blackBackGround.style.justifyContent = "center";
    blackBackGround.style.alignContent = "center";

    const videoBackground = document.createElement("div");
    videoBackground.className = "videoBackground";
    videoBackground.style.backgroundColor = "white";
    videoBackground.style.borderRadius = "10px";
    videoBackground.style.backgroundColor = "white";
    videoBackground.style.border = "solid 1px grey";
    videoBackground.style.height = "240px";

    videoContainer.append(videoBackground);
    videoBackground.append(blackBackGround);

    const p = document.createElement("p");
    p.innerHTML = link.title;
    p.style.textAlign = "center";
    p.style.padding = "5px";

    videoBackground.append(p);
    videoContainer.append(authorNameP);

    const playerContainer = document.createElement("iframe");
    playerContainer.id = "player";
    playerContainer.src = link.youtubeLink;
    playerContainer.width = "305px";
    playerContainer.height = "200px";
    playerContainer.style.borderTopRightRadius = "10px";
    playerContainer.style.borderTopLeftRadius = "10px";

    blackBackGround.append(playerContainer);
    videoContainer.appendChild(videoBackground);
    videosCollection.append(videoContainer);

    videoContainer.addEventListener("click", function () {
        var xhttp = new XMLHttpRequest();
        xhttp.open("GET", "/VideoCollection?handler=VideoPlayer");
        xhttp.send();
        xhttp.onload = function () {
            if (this.status == 200) {
                window.location.href = `/VideoPlayer?id=${link.id}&youtubeLink=${link.youtubeLink}`;
            }
        }
    });
}

window.addEventListener("load", function () {
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            var Response = this.response;

            stringResponse = JSON.stringify(JSON.parse(Response), undefined, 2);
            var parsedResponse = JSON.parse(stringResponse);
            console.log(parsedResponse);

            var Links = {};
            Links = parsedResponse._link;
            console.log(Links);

            var Videos = {};
            Videos = parsedResponse._video;

            for (var row of Videos) {
                const singleVideo = {};
                singleVideo.createdDate = row.createdDate;
                singleVideo.description = row.description;
                singleVideo.filePath = row.filePath;
                singleVideo.id = row.id;
                singleVideo.title = row.title;
                singleVideo.userId = row.userId;
                singleVideo.author = row.userName;
                addVideoInVideosContainer(singleVideo);
            }

            for (var row of Links) {
                const singleLink = {};
                singleLink.createdDate = row.createdDate;
                singleLink.description = row.description;
                singleLink.id = row.id;
                singleLink.modifiedDate = row.modifiedDate;
                singleLink.title = row.title;
                singleLink.userId = row.userId;
                singleLink.youtubeVidId = row.youtubeVidId;
                singleLink.youtubeLink = "https://www.youtube.com/embed/" + row.youtubeVidId;
                singleLink.author = row.userName;
                addLinkInVideosContainer(singleLink);
            }
        }
    }
    http.open("GET", "/Index?handler=SelectVideos");
    http.send();
});


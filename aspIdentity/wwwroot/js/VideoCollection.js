var addVideoBtn = document.getElementById("AddVideoBtn");
addVideoBtn.addEventListener("click", function () {
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status === 200) {
            window.location.href = "/AddVideo"; // Replace "/AddVideo" with the actual URL or route to your AddVideo page
        }
    };
    http.open("GET", "/VideoCollection?handler=AddVideo");
    http.send();
});

const addLinkBtn = document.getElementById("AddLinkBtn");
addLinkBtn.addEventListener("click", function () {
    var http = new XMLHttpRequest();
    http.onload = function () {
        if (this.status == 200) {
            window.location.href = "/AddLink";
        }
    }
    http.open("GET", "/VideoCollection?handler=AddLink");
    http.send();
});

const videosContainer = document.getElementById("videosContainer");
videosContainer.style.marginTop = "50px";
videosContainer.style.marginBottom = "50px";
videosContainer.style.display = "grid";
videosContainer.style.gridTemplateColumns = "repeat(3, 307px)";
videosContainer.style.gridRowGap = "20px";
videosContainer.style.justifyContent = "space-between";
const initialColumns = 3;

const handleResponsive = () => {
    if (window.innerWidth <= 450) {
        // Window width <= 450px
        videosContainer.style.gridTemplateColumns = "repeat(1, 307px)";
        videosContainer.style.justifyContent = "center";
        videosContainer.style.alignContent = "space-around";
    } else if (window.innerWidth <= 769) {
        videosContainer.style.gridTemplateColumns = "repeat(1, 307px)"
        videosContainer.style.justifyContent = "center";
        videosContainer.style.alignContent = "space-around";
    } else if (window.innerWidth <= 992) {
        // 450px < Window width <= 992px
        videosContainer.style.gridTemplateColumns = "repeat(2, 307px)";
        videosContainer.style.justifyContent = "space-between";
        videosContainer.style.alignContent = "space-around";
    } else {
        // Window width > 992px
        videosContainer.style.gridTemplateColumns = `repeat(${initialColumns}, 307px)`;
        videosContainer.style.justifyContent = "space-between";
        videosContainer.style.alignContent = "space-around";
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
    id: "",
    title: "",
    description: "",
    date: "",
    filePath: "",
    author: ""
}) {
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


    const Video = document.createElement("video");
    Video.style.borderTopLeftRadius = "10px";
    Video.style.borderTopRightRadius = "10px";
    Video.style.alignSelf = "top";
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
    videosContainer.append(videoContainer);
}

function addLinkInVideosContainer(link = {
    id: "",
    youtubeVidId: "",
    youtubeLink: "",
    title: "",
    createdDate: "",
    modifiedDate: "",
    author: "",
}) {
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

    const playerContainer = document.createElement("iframe");
    playerContainer.id = "player";
    playerContainer.src = link.youtubeLink;
    playerContainer.width = "305px";
    playerContainer.height = "200px";
    playerContainer.style.borderTopRightRadius = "10px";
    playerContainer.style.borderTopLeftRadius = "10px";

    blackBackGround.append(playerContainer);
    videoContainer.appendChild(videoBackground);
    videosContainer.append(videoContainer);

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
    let xhttp = new XMLHttpRequest();
    xhttp.responseType = "json";
    xhttp.onload = function () {
        if (this.status === 200) {
            let result = this.response;
            console.log(result);
            var videos = result["videos"];
            var links = result["links"];

            for (var row of videos) {
                const video = {
                    id: row.id,
                    title: row.title,
                    description: row.description,
                    date: row.date,
                    filePath: row.filePath,
                    author: row.userName
                }
                addVideoInVideosContainer(video);
                console.log(video);
            }
            console.log("links-----------------");
            for (var row of links) {
                const link = {
                    id: row.id,
                    youtubeVidId: row.youtubeVidId,
                    youtubeLink: "https://www.youtube.com/embed/" + row.youtubeVidId,
                    title: row.title,
                    createdDate: row.createdDate,
                    modifiedDate: row.modifiedDate,
                    author: row.userName,
                }
                addLinkInVideosContainer(link);
                console.log(link);
            }
        }
    }
    xhttp.open("GET", "/VideoCollection?handler=SelectVideos");
    xhttp.send();
});


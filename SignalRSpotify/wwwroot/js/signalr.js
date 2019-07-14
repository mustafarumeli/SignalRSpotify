var userId = btoa($.cookie("userId"));
let connection = null;
if (userId !== '') {
    connection = new signalR.HubConnectionBuilder().withUrl("/testHub?userId=" + userId).build();
}
connection.start().then(function () {
    $("#createRoom__container").show();
}).catch(function (err) {
    return console.error(err.toString());
});
function SendRoomMesage() {
    var roomName = $("#roomName").val();
    var msg = $("#roomMsg").val();
    connection.invoke("SendRoomMessage", roomName, msg).then(function (data) {
        alert(data);
    });
}
function CreateorJoinSignalRRoom(isCreate) {
    var roomName = $("#roomName").val();
    connection.invoke("CreateOrJoinRoom", roomName, isCreate).then(function (data) {
        alert(data);
    }).catch(function (err) {
        return console.error(err.toString());
    });
}
function WriteText() {
    var text = $("#summernote").val();
    var roomName = $("#roomName").val();
    connection.invoke("WriteText", text, roomName).catch(function (err) {
        return console.error(err.toString());
    });
}
function TogglePlayingStatus(isPlaying) {
    var roomName = $("#roomName").val();
    var currentTime = document.getElementById("audio").currentTime;
    connection.invoke("ToggleAudioPlayingStatus", isPlaying, roomName, currentTime).catch(function (err) {
        return console.error(err.toString());
    });
}

if (connection !== null) {
    connection.on("ReciveGroupMessage", function (userName, msg) {
        alert(userName + " said  " + msg);
    });
    connection.on("AppendText", function (text) {
        $("#summernote").summernote('code', text);
    });
    connection.on("ToggleAudioPlayingStatusJs", function (isPlaying,currentTime) {
        var audio = document.getElementById("audio");
        audio.currentTime = currentTime;
        if (isPlaying === true) {
            audio.play();
        } else {
            audio.pause();
        }
    });
}


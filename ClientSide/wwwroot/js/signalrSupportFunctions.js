var supportConnection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/support", signalR.HttpTransportType.WebSockets).build();


supportConnection.on("GetRooms", loadRooms);


var roomListEL = document.getElementById("#roomList")
function removeAllChildren(node) {
    if (!node) return;
    while (node.lastChild) {
        node.removeChild(node.lastChild)
    }
}


function loadRooms(rooms) {
    if (!rooms) return;
    var roomIds = Object.keys(rooms);
    //console.log(roomIds)
    if (!roomIds) return;

    removeAllChildren(roomListEL)
    roomIds.forEach(function (id) {
        var roomInfo = rooms[id];
        if (!roomInfo) return;
        return $("#roomList").append('<div class="list-group list-group-flush"> <a href="javascript:;" class="list-group-item" data-id="' + id + '"> <div class="d-flex"> <div class="chat-user-online"> <img src="/appdata/user/avatar/Default.png" width="42" height="42" class="rounded-circle" alt="" /> </div> <div class="flex-grow-1 ms-2"> <h6 class="mb-0 chat-title">' + roomInfo + '</h6> </div> </div> </a> </div>');
    })
}

function Init() {
 
}

$(document).ready(function () {
    Init();
});




//start connection
function fulfiled() {
    console.log("connection to user Hub Successful");
    //newWindowLoadedOnClient();
}
function rejected() {

}

supportConnection.start().then(fulfilled, rejected);
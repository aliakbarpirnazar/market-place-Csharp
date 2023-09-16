var connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/chat",signalR.HttpTransportType.WebSockets).build();


connection.on("updateOnlineUserCount", (value) => {
    var newCountSpan = document.getElementById("viewsCounter");
    newCountSpan.innerText = value.toString();
});

function Init() {
    var messageForm = $("#MessageForm");
    messageForm.on("submit", function (e) {
        e.preventDefault();

        var message = e.target[0].value;
        e.target[0].value = "";
        sendMessage(message);
    });
}

$(document).ready(function () {
    Init();
});

//اسال پیام به سرور
async function sendMessage(message) {
    try {
        await connection.invoke("SendNewMessage", "بازدید کننده سایت :", message);
    }
    catch (error){
        console.error(error);
    }
   

}

//دریافت پیام از سرور
connection.on("getNewMessage", getMessage);
function getMessage(sender, message) {
    if (sender == "پشتیبانی :") {
        $("#MessagesList").append("<li><div class='chat-msg self'><div class='cm-msg-text'><p>" + message + "</p></div></div></li>")
    } else {
        $("#MessagesList").append("<li><div class='chat-msg user'><div class='cm-msg-text'><p>" + message + "</p></div></div></li>")
    }
   
}

async function createChatRoom() {
    try {
        await connection.invoke("SendNewMessage","بازدید کننده :","/createNewChat")



    } catch (error) {
        console.error(error);
    }
}

//start connection
function fulfiled() {
    console.log("connection to user Hub Successful");
    //newWindowLoadedOnClient();
}
function rejected() {

}

connection.start().then(fulfilled, rejected);
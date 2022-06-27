var tokenKey = "AccessToken";
var currentUserId = "userId";
function init() {
    let authorized = sessionStorage.getItem(currentUserId);
    if (!authorized) {
        document.getElementById("logout-button").style = "visibility:hidden";
        if (location.pathname != "/LoginPage" && location.pathname != "/RegisterPage")
            location.assign("/LoginPage");
    }
}

function logout() {
    sessionStorage.removeItem(tokenKey);
    sessionStorage.removeItem(currentUserId);
}

function checkEmpty() {
    for (let i = 0; i < arguments.length; i++) {
        if (arguments[i] === "") return true;
    }
    return false;
}

function setMessage(message, messageElement = null) {
    if (messageElement === "" || !messageElement)
        messageElement = document.getElementById("message");
    messageElement.innerText = message;
}
function setMessageHtml(messageHtml, messageElement = null) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    messageElement.innerHTML = messageHtml;
}
function addMessageHtml(messageHtml, messageElement = null) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    messageElement.innerHTML += messageHtml + "<br/>";
}
function addMultipleMessageHtml(msgArray) {
    for (let i = 0; i < msgArray.length; i++) {
        addMessageHtml(msgArray[i])
    }
}
var tokenKey = "AccessToken";
var currentUserId = "userId";
var apihost = "https://localhost:5001/api";

function init() {
    let authorized = sessionStorage.getItem(currentUserId);
    if (!authorized) {
        document.getElementById("logout-button").style = "visibility:hidden";
        if (location.pathname != "/Login" && location.pathname != "/Register")
            location.assign("/Login");
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

function setMessage(message, messageElement) {
    if (messageElement === "" || !messageElement)
        messageElement = document.getElementById("message");
    if (messageElement)
        messageElement.innerText = message;
}
function setMessageHtml(messageHtml, messageElement) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    if (messageElement)
        messageElement.innerHTML = messageHtml;
}
function addMessageHtml(messageHtml, messageElement) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    if (messageElement)
        messageElement.innerHTML += messageHtml + "<br/>";
}
function addMultipleMessageHtml(msgArray, messageElement) {
    for (let i = 0; i < msgArray.length; i++) {
        if (messageElement)
            addMessageHtml(msgArray[i], messageElement)
    }
}
function addRequestErrorsToMessage(requestData, messageElement) {
    let errorArr = Object.values(requestData.errors);
    if (messageElement === "" || !messageElement)
        messageElement = document.getElementById("message");
    if (messageElement)
        addMultipleMessageHtml(errorArr, messageElement);
}

function handleRequestErrors(response) {
    response.json().then((data) => {
        if (data.errors) {
            //обработка ошибок модели
            setMessage("");
            addRequestErrorsToMessage(data);
        }
        else
            if (data.errorMessage)
                //обработка ошибок сервера
                setMessage(data.errorMessage);
            else setMessage("Произошла ошибка. Код ошибки: " + response.status);
    });
}
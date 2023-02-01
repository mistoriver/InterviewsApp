var tokenKey = "AccessToken";
var currentUserId = "userId";
var apihost = "";

function init(host) {
    apihost = host ?? "http://unconfigured-api-host-will-not-work";
    let authorized = Cookies.get(currentUserId);
    if (!authorized) {
        if (location.pathname != "/Login" && location.pathname != "/Register")
            location.assign("/Login");
    }
}

function logout() {
    Cookies.remove(tokenKey);
    Cookies.remove(currentUserId);
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
    if (response.status === 401 && location.pathname != "/Login") {
        logout();
        location.reload();
    }
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

function getLocals() {
    let langBtn = document.getElementById("localization-button");
    langBtn.innerText = (langBtn.innerText === "RU" ? "EN" : "RU");

    let lang = langBtn.innerText;
    Cookies.get(currentUserId);
    fetch(apihost + "/Localization/" + (lang ? "GetByLang?langCode=" + lang : "GetByUserId?userId=" + Cookies.get(currentUserId)), {
        method: "GET", headers: {
            "Accept": "application/json"
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    localStorage.setItem("localizations", data);
                });
        else
            handleRequestErrors(response);
    });
}
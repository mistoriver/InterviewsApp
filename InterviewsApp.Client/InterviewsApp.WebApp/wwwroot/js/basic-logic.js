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

function getLocals(isLangSwitched = false) {
    let userId = Cookies.get(currentUserId);

    let langBtn = document.getElementById("localization-button");
    let lang = langBtn.innerText;
    if (isLangSwitched) {
        langBtn.innerText = (langBtn.innerText === "RU" ? "EN" : "RU");
        lang = langBtn.innerText;
        if (userId)
            fetch(apihost + "/Localization/SetForUser?userId=" + userId + "&langCode=" + lang, {
                method: "PUT", headers: {
                    "Accept": "application/json"
                }
            }).then((response) => {
                if (response.ok) { }
                else
                    handleRequestErrors(response);
            });
    }
    fetch(apihost + "/Localization/" + (isLangSwitched || !userId ? "GetByLang?langCode=" + lang : "GetByUserId?userId=" + userId), {
        method: "GET", headers: {
            "Accept": "application/json"
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    localStorage.setItem("localizations", JSON.stringify(data.responseData));
                    document.dispatchEvent(localsReadyEvent);
                });
        else
            handleRequestErrors(response);
    });
}

function setLocals() {
    let locals = JSON.parse(localStorage.getItem("localizations"));
    let localizableElements = document.getElementsByClassName("loc");
    for (let elem of localizableElements) {
        let local = locals.find(loc => elem.innerText === loc.localizationCode);
        if (local) {
            elem.innerText = local.value;
        }
    };
}
var tokenKey = "AccessToken";
var currentUserId = "userId";
var apihost = "";
var localsUpdating = false;

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
function checkMessageElement(messageElement) {
    if (messageElement === "" || !messageElement)
        messageElement = document.getElementById("message");
    return messageElement;
}

function setMessage(message, messageElement) {
    messageElement = checkMessageElement(messageElement);
    if (messageElement) {
        messageElement.innerText = message;
        setLocals();
    }
}
function setMessageHtml(messageHtml, messageElement) {
    messageElement = checkMessageElement(messageElement);
    if (messageElement) {
        messageElement.innerHTML = messageHtml;
        setLocals();
    }
}
function addMessageHtml(messageHtml, messageElement) {
    messageElement = checkMessageElement(messageElement);
    if (messageElement) {
        messageElement.innerHTML += messageHtml + "<br/>";
        setLocals();
    }
}
function addMultipleMessageHtml(msgArray, messageElement) {
    for (let i = 0; i < msgArray.length; i++) {
        if (messageElement)
            addMessageHtml(msgArray[i], messageElement);
    }
    setLocals();
}
function addRequestErrorsToMessage(requestData, messageElement) {
    let errorArr = Object.values(requestData.errors);
    messageElement = checkMessageElement(messageElement);
    if (messageElement) {
        addMultipleMessageHtml(errorArr, messageElement);
        setLocals();
    }
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
            else setMessage((localStorage.getItem("currentLocal") === "RU" ?
                "Произошла ошибка. Код ошибки: " :
                "An error occured. Error code: ")
                + response.status);
    });
}

function changeLanguage() {
    let userId = Cookies.get(currentUserId);
    let langBtn = document.getElementById("localization-button");
    let lang = (langBtn.innerText === "RU" ? "EN" : "RU")
    if (userId) {
        fetch(apihost + "/Localization/SetForUser?userId=" + userId + "&langCode=" + lang, {
            method: "PUT", headers: {
                "Accept": "application/json"
            }
        }).then((response) => {
            if (response.ok) {
                getLocals(lang);
            }
            else
                handleRequestErrors(response);
        });
    }
    else {
        getLocals(lang);
    }
}

function getLocals(lang = "EN") {
    localsUpdating = true;
    let userId = Cookies.get(currentUserId);
    localStorage.removeItem("localizations");
    localStorage.removeItem("currentLocal");
    fetch(apihost + "/Localization/" + (!userId ? "GetByLang?langCode=" + lang : "GetByUserId?userId=" + userId), {
        method: "GET", headers: {
            "Accept": "application/json"
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    localStorage.setItem("localizations", JSON.stringify(data.responseData));
                    localStorage.setItem("currentLocal", data.responseData[0].language);
                    location.reload();
                });
        else
            handleRequestErrors(response);
    }).catch((error) => {
        setMessage(localStorage.getItem("currentLocal") === "RU" ? "Сервер недоступен." : "Server is inaccessible.");
    });
}

function setLocals() {
    if (localsUpdating) return;
    let table = document.getElementById('interviews-table') ?? document.getElementById('positions-table');
    if (table)
        table.setAttribute("data-locale", localStorage.getItem("currentLocal") === "RU" ? "ru-RU" : "en-US");
    document.getElementById("localization-button").innerText = localStorage.getItem("currentLocal");
    let locals = JSON.parse(localStorage.getItem("localizations"));
    let localizableElements = document.getElementsByClassName("localizable");
    for (let elem of localizableElements) {
        if (elem.innerHTML.includes("Loc.")) {
            let local = locals.find(loc => elem.innerHTML.includes(loc.localizationCode));
            if (local) {
                elem.innerHTML = elem.innerHTML.replace(local.localizationCode, local.value);
            }
        }
    };
}
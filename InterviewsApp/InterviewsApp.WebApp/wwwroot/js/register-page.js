function checkPass(firstpass, secondpass) {
    return firstpass === secondpass;
}

let redirectTimeoutToken = "";
let messageElement = ""

function register() {
    let name = document.getElementById("name").value;
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
    let secpassword = document.getElementById("repeat-password").value;
    if (checkEmpty(name, login, password, secpassword))
        setMessage("Для регистрации необходимо заполнить все поля!");
    else
        if (checkPass(password, secpassword)) {
            document.getElementById("register-button").disabled = true;
            fetch('https://localhost:7262/api/User/Register',
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        name: name,
                        login: login,
                        password: password
                    })
                }).then((response) => {
                    if (response.ok === true) {
                        document.getElementById("manual-redirect").style = "";
                        setMessage("Пользователь успешно зарегистрирован. Переадресация на страницу входа через 5 секунд...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/LoginPage");
                        }, 5000);
                    }
                    else {
                        try {
                            response.json().then((data) => {
                                setMessage("");
                                if (data.errors.Name)
                                    addMultipleMessageHtml(data.errors.Name);
                                if (data.errors.Login)
                                    addMultipleMessageHtml(data.errors.Login);
                                if (data.errors.Password)
                                    addMultipleMessageHtml(data.errors.Password);
                                document.getElementById("register-button").disabled = false;
                            })
                        }
                        catch (e) {
                            setMessage("Регистрация неуспешна. Код ошибки: " + response.status);
                        }
                    }
                })
        }
        else {
            setMessage("Пароль и подтверждение пароля должны совпадать!");
        }
}

function setMessage(message) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    messageElement.innerText = message;
}
function setMessageHtml(messageHtml) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    messageElement.innerHTML = messageHtml;
}
function addMessageHtml(messageHtml) {
    if (messageElement === "")
        messageElement = document.getElementById("message");
    messageElement.innerHTML += messageHtml + "<br/>";
}
function addMultipleMessageHtml(msgArray) {
    for (let i = 0; i < msgArray.length; i++) {
        addMessageHtml(msgArray[i])
    }
}
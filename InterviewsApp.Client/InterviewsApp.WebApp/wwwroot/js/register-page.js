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
            fetch(apihost + '/User/Register',
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
                            location.assign("/Login");
                        }, 5000);
                    }
                    else {
                        try {
                            response.json().then((data) => {
                                setMessage("");
                                addRequestErrorsToMessage(data)
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
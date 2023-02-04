function checkPass(firstpass, secondpass) {
    return firstpass === secondpass;
}

let redirectTimeoutToken = "";
let messageElement = ""

function register() {
    let currentLocal = localStorage.getItem("currentLocal")
    let name = document.getElementById("name").value;
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
    let secpassword = document.getElementById("repeat-password").value;
    if (checkEmpty(name, login, password, secpassword))
        setMessage(currentLocal === "RU" ?
            "Для регистрации необходимо заполнить все поля!"
            :"You need to fill all fields to continue.");
    else {
        setMessage("");
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
                    if (response.ok) {
                        document.getElementById("manual-redirect").style = "";
                        setMessage(currentLocal === "RU" ?
                            "Пользователь успешно зарегистрирован. Переадресация на страницу входа через 5 секунд..."
                            :"User created successfully. Redirecting to the login page in 5 seconds...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/Login");
                        }, 5000);
                    }
                    else {
                        handleRequestErrors(response);
                        document.getElementById("register-button").disabled = false;
                    }
                }).catch((error) => {
                    setMessage(currentLocal === "RU" ?
                        "Регистрация невозможна. Сервер недоступен."
                        :"Registration failed. Server is inaccessible");
                    document.getElementById("register-button").disabled = false;
                })
        }
        else {
            setMessage(currentLocal === "RU" ?
                "Пароль и подтверждение пароля должны совпадать!"
                :"Password and password confirmation must be identical!");
        }
    }
}
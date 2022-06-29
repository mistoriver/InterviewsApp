function login() {
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
    document.getElementById("login-button").disabled = true;
    document.getElementById("register-button").disabled = true;
    fetch(apihost + '/User/Login',
        {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                login: login,
                password: password
            })
        }).then((response) => {
            if (response.ok === true) {
                response.json().then((data) => {
                    sessionStorage.setItem(tokenKey, data.token);
                    sessionStorage.setItem(currentUserId, data.userId);
                    location.replace("/");
                });
            }
            else {
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                    else
                        if (response.status === 401)
                            setMessage(data.error);
                        else setMessage("Аутентификация неуспешна. Код ошибки: " + response.status);
                });
            }
        }).catch((error) => {
            setMessage("Аутентификация неуспешна. Сервер недоступен.");
        })
        .then(() => {
            document.getElementById("login-button").disabled = false;
            document.getElementById("register-button").disabled = false;
        });
}
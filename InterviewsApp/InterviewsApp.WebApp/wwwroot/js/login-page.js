function login() {
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
    try {
        document.getElementById("login-button").disabled = true;
        document.getElementById("register-button").disabled = true;
        fetch('https://localhost:7262/api/User/Login',
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
                    try {
                        response.json().then((data) => {
                            if (data.errors) {
                                setMessage("");
                                addRequestErrorsToMessage(data);
                            }
                            else
                                setMessage("Аутентификация неуспешна. Код ошибки: " + response.status);
                        });
                    }
                    catch (e) {
                    }
                }
            }).then(() => {
                document.getElementById("login-button").disabled = false;
                document.getElementById("register-button").disabled = false;
            });
    }
    catch (e) {

    }
}
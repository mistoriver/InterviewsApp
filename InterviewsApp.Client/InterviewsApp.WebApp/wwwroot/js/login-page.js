function login() {
    setMessage("");
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
            if (response.ok) {
                response.json().then((data) => {
                    Cookies.set(tokenKey, data.responseData.token, {expires: 1});
                    Cookies.set(currentUserId, data.responseData.userId, { expires: 1 });
                    getLocals();
                    location.replace("/");
                });
            }
            else {
                handleRequestErrors(response);
            }
        }).catch((error) => {
            setMessage("Аутентификация неуспешна. Сервер недоступен.");
            document.getElementById("login-button").disabled = false;
            document.getElementById("register-button").disabled = false;
        })
        .then(() => {
            document.getElementById("login-button").disabled = false;
            document.getElementById("register-button").disabled = false;
        });
}
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
            if (response.ok) {
                response.json().then((data) => {
                    sessionStorage.setItem(tokenKey, data.responseData.token);
                    sessionStorage.setItem(currentUserId, data.responseData.userId);
                    location.replace("/");
                });
            }
            else {
                handleRequestErrors(response);
            }
        }).catch((error) => {
            setMessage("Аутентификация неуспешна. Сервер недоступен.");
        })
        .then(() => {
            document.getElementById("login-button").disabled = false;
            document.getElementById("register-button").disabled = false;
        });
}
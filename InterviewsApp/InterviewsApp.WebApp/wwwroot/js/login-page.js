function login() {
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
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
                location.assign("/");
            });
        }
    })
    
}
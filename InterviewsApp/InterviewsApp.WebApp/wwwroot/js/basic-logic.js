var tokenKey = "AccessToken";
var currentUserId = "userId";
function init() {
    let authorized = sessionStorage.getItem(currentUserId);
    if (!authorized) {
        document.getElementById("logout-button").style = "visibility:hidden";
        if (location.pathname != "/LoginPage" && location.pathname != "/RegisterPage")
            location.assign("/LoginPage");
    }
}

function logout() {
    sessionStorage.removeItem(tokenKey);
    sessionStorage.removeItem(currentUserId);
}
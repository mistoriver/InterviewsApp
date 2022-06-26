var tokenKey = "AccessToken";
var currentUserId = "userId";
function init() {
    if (!sessionStorage.getItem(currentUserId) && location.pathname != "/LoginPage")
        location.assign("/LoginPage");
}

function logout() {
    sessionStorage.removeItem(tokenKey);
    sessionStorage.removeItem(currentUserId);
}
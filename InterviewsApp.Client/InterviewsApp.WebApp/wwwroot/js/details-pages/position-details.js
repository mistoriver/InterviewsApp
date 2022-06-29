function getPositionInfo(id) {
    fetch(apihost + "/Position/GetByUser?id=" + id + "&userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("position-name").innerText += ' "' + data.name + '"';
                    document.getElementById("city-name").innerText = data.city;
                    document.getElementById("city-edit-input").value = data.city;
                    document.getElementById("money").innerText = data.moneyLower == data.moneyUpper ? numberWithSpaces(data.moneyLower) : numberWithSpaces(data.moneyLower) + " - " + numberWithSpaces(data.moneyUpper);
                    document.getElementById("money-edit-from").value = data.moneyLower;
                    document.getElementById("money-edit-to").value = data.moneyUpper;
                    let commentElem = document.getElementById("comment");
                    commentElem.innerText = data.comment;
                    if (!data.comment || data.comment === "") {
                        commentElem.style = "display:none";
                    }
                    document.getElementById("comment-edit-input").value = data.comment;
                    let comp = document.getElementById("company-name");
                    let aComp = document.createElement('a');
                    aComp.href = '/Details/Company?CompanyId=' + data.companyId;
                    aComp.innerText = data.companyName;
                    comp.appendChild(aComp);
                }
            })
    });
}

function numberWithSpaces(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
}

function editComment() {
    let com = document.getElementById("comment");
    let commInput = document.getElementById("comment-edit-input");
    commInput.value = com.innerText;
    com.style = "display:none;";
    commInput.style = "";
    document.getElementById("edit-comment").style = "display:none;";
    document.getElementById("confirm-comment").style = "";
    document.getElementById("discard-comment").style = "";

}
function discardComment() {
    let comm = document.getElementById("comment");
    let commInput = document.getElementById("comment-edit-input");
    commInput.value = comm.innerText;
    comm.style = "";
    commInput.style = "display:none;";
    document.getElementById("edit-comment").style = "";
    document.getElementById("confirm-comment").style = "display:none;";
    document.getElementById("discard-comment").style = "display:none;";

}
function confirmComment(id) {
    document.getElementById("confirm-comment").disabled = true;
    document.getElementById("comment-edit-input").disabled = true;
    fetch(apihost + "/Interview/UpdateComment",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: sessionStorage.getItem(currentUserId),
                comment: document.getElementById("comment-edit-input").value
            })
        }).then((response) => {
            if (response.ok === true) {
                location.reload();
            }
            else {
                document.getElementById("confirm-comment").disabled = false;
                document.getElementById("comment-edit-input").disabled = false;
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                });
            };
        });
}

function editMoney() {
    document.getElementById("money").style = "display:none;";
    document.getElementById("edit-money").style = "display:none;";
    document.getElementById("money-edit-from").style = "";
    document.getElementById("money-edit-to").style = "";
    document.getElementById("confirm-money").style = "";
    document.getElementById("discard-money").style = "";
}
function discard() {
    location.reload();

}
function confirmMoney(id) {
    document.getElementById("confirm-money").disabled = true;
    document.getElementById("money-edit-from").disabled = true;
    document.getElementById("money-edit-to").disabled = true;
    fetch(apihost + "/Position/UpdateMoney",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: sessionStorage.getItem(currentUserId),
                moneyLower: document.getElementById("money-edit-from").value,
                moneyUpper: document.getElementById("money-edit-to").value
            })
        }).then((response) => {
            if (response.ok === true) {
                location.reload();
            }
            else {
                document.getElementById("confirm-money").disabled = false;
                document.getElementById("money-edit-from").disabled = false;
                document.getElementById("money-edit-to").disabled = false;
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                });
            };
        });
}
function editCity() {
    document.getElementById("city-name").style = "display:none;";
    document.getElementById("edit-city").style = "display:none;";
    document.getElementById("city-edit-input").style = "";
    document.getElementById("confirm-city").style = "";
    document.getElementById("discard-city").style = "";
}
function confirmCity(id) {
    document.getElementById("confirm-city").disabled = true;
    document.getElementById("city-edit-input").disabled = true;
    fetch(apihost + "/Position/UpdateCity",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: sessionStorage.getItem(currentUserId),
                city: document.getElementById("city-edit-input").value
            })
        }).then((response) => {
            if (response.ok === true) {
                location.reload();
            }
            else {
                document.getElementById("confirm-city").disabled = false;
                document.getElementById("city-edit-input").disabled = false;
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                });
            };
        });
}

function offer(id) {
    fetch(apihost + "/Position/SetOffered?id=" + id + "&userId=" + sessionStorage.getItem(currentUserId),
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
            }
        }).then((response) => {
            if (response.ok === true) {
                location.assign("/");
            }
            else {
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                });
            };
        });
}
function denial(id) {
    fetch(apihost + "/Position/SetDenied?id=" + id + "&userId=" + sessionStorage.getItem(currentUserId),
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
            }
        }).then((response) => {
            if (response.ok === true) {
                location.assign("/");
            }
            else {
                response.json().then((data) => {
                    if (data.errors) {
                        setMessage("");
                        addRequestErrorsToMessage(data);
                    }
                });
            };
        });
}
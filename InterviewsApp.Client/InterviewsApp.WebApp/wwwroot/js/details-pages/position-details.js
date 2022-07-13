let positionId = "";
function getPositionInfo(id) {
    setMessage("");
    positionId = id;
    fetch(apihost + "/Position/GetByUser?id=" + id + "&userId=" + Cookies.get(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then((data) => {
                    if (data) {
                        document.getElementById("position-name").innerText += ' "' + data.responseData.name + '"';
                        document.getElementById("city-name").innerText = data.responseData.city;
                        document.getElementById("city-edit-input").value = data.responseData.city;
                        document.getElementById("money").innerText = data.responseData.moneyLower == data.responseData.moneyUpper ?
                            numberWithSpaces(data.responseData.moneyLower) :
                            numberWithSpaces(data.responseData.moneyLower) + " - " + numberWithSpaces(data.responseData.moneyUpper);
                        document.getElementById("money-edit-from").value = data.responseData.moneyLower;
                        document.getElementById("money-edit-to").value = data.responseData.moneyUpper;
                        let commentElem = document.getElementById("comment");
                        commentElem.innerText = data.responseData.comment;
                        if (!data.responseData.comment || data.responseData.comment === "") {
                            commentElem.style = "display:none";
                        }
                        document.getElementById("comment-edit-input").value = data.responseData.comment;
                        let comp = document.getElementById("company-name");
                        let aComp = document.createElement('a');
                        aComp.href = '/Details/Company?CompanyId=' + data.responseData.companyId;
                        aComp.innerText = data.responseData.companyName;
                        comp.appendChild(aComp);
                    }
                });
        else
            handleRequestErrors(response);
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
    setMessage("");
    document.getElementById("confirm-comment").disabled = true;
    document.getElementById("comment-edit-input").disabled = true;
    fetch(apihost + "/Position/UpdateComment",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: Cookies.get(currentUserId),
                comment: document.getElementById("comment-edit-input").value
            })
        }).then((response) => {
            if (response.ok) {
                location.reload();
            }
            else {
                document.getElementById("confirm-comment").disabled = false;
                document.getElementById("comment-edit-input").disabled = false;
                handleRequestErrors(response);
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
    setMessage("");
    document.getElementById("confirm-money").disabled = true;
    document.getElementById("money-edit-from").disabled = true;
    document.getElementById("money-edit-to").disabled = true;
    fetch(apihost + "/Position/UpdateMoney",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: Cookies.get(currentUserId),
                moneyLower: document.getElementById("money-edit-from").value,
                moneyUpper: document.getElementById("money-edit-to").value
            })
        }).then((response) => {
            if (response.ok) {
                location.reload();
            }
            else {
                document.getElementById("confirm-money").disabled = false;
                document.getElementById("money-edit-from").disabled = false;
                document.getElementById("money-edit-to").disabled = false;
                handleRequestErrors(response);
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
    setMessage("");
    document.getElementById("confirm-city").disabled = true;
    document.getElementById("city-edit-input").disabled = true;
    fetch(apihost + "/Position/UpdateCity",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: Cookies.get(currentUserId),
                city: document.getElementById("city-edit-input").value
            })
        }).then((response) => {
            if (response.ok) {
                location.reload();
            }
            else {
                document.getElementById("confirm-city").disabled = false;
                document.getElementById("city-edit-input").disabled = false;
                handleRequestErrors(response);
            };
        });
}

function offer(id) {
    setMessage("");
    fetch(apihost + "/Position/SetOffered?id=" + id + "&userId=" + Cookies.get(currentUserId),
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            }
        }).then((response) => {
            if (response.ok) {
                location.assign("/");
            }
            else {
                handleRequestErrors(response);
            };
        });
}
function denial(id) {
    setMessage("");
    fetch(apihost + "/Position/SetDenied?id=" + id + "&userId=" + Cookies.get(currentUserId),
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            }
        }).then((response) => {
            if (response.ok) {
                location.assign("/");
            }
            else {
                handleRequestErrors(response);
            };
        });
}
function getInterviewsForPosition(params) {
    let token = Cookies.get(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    let dt = new Date()
    let corrDt = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes() - dt.getTimezoneOffset());
    currDTString = corrDt.toISOString();
    fetch(apihost + "/Interview/GetByPosition?positionId="+ positionId +"&userId=" + Cookies.get(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    params.success(data.responseData);
                });
        $('#interviews-table').bootstrapTable("hideLoading");
    });
}
function dateFormatter(value) {
    value = value.split('.')[0].replace('Z', '');
    return '<input type="datetime-local" value="' + value + '" disabled/>'
}
function interviewFormatter(value, row) {
    return '<a  href="/Details/Interview?InterviewId=' + row.id + '">' + value + ' <a/>'
}
function rowStyle(row, index) {
    if (row.offerReceived)
        return {
            css: { background: '#cee8be' }
        }
    if (row.denialReceived)
        return {
            css: { background: '#bf8c8c' }
        }
    if (row.date < currDTString) {
        return {
            css: { background: '#e3e3e3' }
        }
    }
    return {}
}
function goToInterviewCreation() {
    location.assign('/Create/Interview?CreatedPosition=' + positionId);
}

function deletePosition() {
    document.getElementById("delete-position-button").disabled = true;
    fetch(apihost + "/Position/Delete?id=" + positionId + "&userId=" + Cookies.get(currentUserId), {
        method: "DELETE", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        if (response.ok)
            location.assign('/Positions');
        else {
            document.getElementById("delete-position-button").disabled = false;
            handleRequestErrors(response);
        }
    });
}
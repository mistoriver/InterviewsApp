function getInterviewInfo(id) {
    setMessage("");
    fetch(apihost + "/Interview/Get?id="+ id +"&userId=" + Cookies.get(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then((data) => {
                    if (data) {
                        document.getElementById("interview-name").innerText += ' "' + data.responseData.name + '"';
                        document.getElementById("interview-time").value = data.responseData.date.replace('Z', '');
                        let commentElem = document.getElementById("comment");
                        commentElem.innerText = data.responseData.comment;
                        if (!data.responseData.comment || data.responseData.comment === "") {
                            commentElem.style = "display:none";
                        }
                        let pos = document.getElementById("position-name");
                        let aPos = document.createElement('a');
                        aPos.href = '/Details/Position?PositionId=' + data.responseData.positionId;
                        aPos.innerText = data.responseData.positionName;
                        pos.appendChild(aPos);
                        let comp = document.getElementById("company-name");
                        let aComp = document.createElement('a');
                        aComp.href = '/Details/Company?CompanyId=' + data.responseData.companyId;
                        aComp.innerText = data.responseData.companyName;
                        comp.appendChild(aComp);
                    }
                });
        else handleRequestErrors(response);
    });
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
    fetch(apihost + "/Interview/UpdateComment",
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

function editTime() {
    document.getElementById("interview-time").disabled = false;
    document.getElementById("edit-time").style = "display:none;";
    document.getElementById("confirm-time").style = "";
    document.getElementById("discard-time").style = "";

}
function discardTime() {
    location.reload();
}
function confirmTime(id) {
    setMessage("");
    document.getElementById("confirm-time").disabled = true;
    document.getElementById("interview-time").disabled = true;
    fetch(apihost + "/Interview/UpdateDatetime",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + Cookies.get(tokenKey)
            },
            body: JSON.stringify({
                id: id,
                userId: Cookies.get(currentUserId),
                date: document.getElementById("interview-time").value + "Z"
            })
        }).then((response) => {
            if (response.ok) {
                location.reload();
            }
            else {
                document.getElementById("confirm-time").disabled = false;
                document.getElementById("interview-time").disabled = false;
                handleRequestErrors(response);
            };
        });
}
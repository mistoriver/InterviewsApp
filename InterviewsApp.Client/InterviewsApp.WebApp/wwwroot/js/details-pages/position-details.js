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

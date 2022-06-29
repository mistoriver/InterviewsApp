function getInterviewInfo(id) {
    fetch(apihost + "/Interview/Get?id="+ id +"&userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("interview-name").innerText += ' "' + data.name + '"';
                    document.getElementById("interview-time").value = data.date.replace('Z', '');
                    document.getElementById("comment").innerText = data.comment;
                    let pos = document.getElementById("position-name");
                    let aPos = document.createElement('a');
                    aPos.href = '/Details/Position?PositionId=' + data.positionId;
                    aPos.innerText = data.positionName;
                    pos.appendChild(aPos);
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
    commInput.parentElement.style = "";
    document.getElementById("edit-comment").parentElement.style = "display:none;";
    document.getElementById("confirm-comment").parentElement.style = "";
    document.getElementById("discard-comment").parentElement.style = "";

}
function discardComment() {
    let comm = document.getElementById("comment");
    let commInput = document.getElementById("comment-edit-input");
    commInput.value = comm.innerText;
    comm.style = "";
    commInput.parentElement.style = "display:none;";
    document.getElementById("edit-comment").parentElement.style = "";
    document.getElementById("confirm-comment").parentElement.style = "display:none;";
    document.getElementById("discard-comment").parentElement.style = "display:none;";

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
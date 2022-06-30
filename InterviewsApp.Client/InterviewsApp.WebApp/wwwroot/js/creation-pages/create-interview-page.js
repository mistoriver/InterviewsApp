function getPositions(createdPosition) {
    fetch(apihost + "/Position/GetMultiplePositionsByUser?userId=" + sessionStorage.getItem("userId"), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem("AccessToken")
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    let sel = document.getElementById("position-select");
                    if (data.responseData.length > 0) {
                        for (let i = 0; i < data.responseData.length; i++) {
                            let opt = document.createElement("option");
                            opt.value = data.responseData[i].id;
                            opt.text = data.responseData[i].name;
                            if (data.responseData[i].id === createdPosition)
                                opt.selected = true;
                            sel.appendChild(opt);
                        }
                    }
                    else {
                        let opt = document.createElement("option");
                        opt.text = "У вас нет вакансий, для которых можно добавить собеседование.";
                        sel.appendChild(opt);
                        sel.disabled = true;
                        document.getElementById("create-button").disabled = true;
                    }
                    document.getElementById("hidden-button").style = "";
                });
        else
            handleRequestErrors(response);
    });
}
function createInterview() {
    let name = document.getElementById("interviewName").value;
    let datetime = document.getElementById("interviewTime").value+"Z";
    let position = document.getElementById("position-select").value;
    if (!checkEmpty(name, datetime, position)) {
        document.getElementById("create-button").disabled = true;
        fetch(apihost + '/Interview/AddInterview',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + sessionStorage.getItem("AccessToken")
                },
                body: JSON.stringify({
                    name: name,
                    date: datetime,
                    positionId: position
                })
            }).then((response) => {
                if (response.ok) {
                    document.getElementById("manual-redirect").style = "";
                    setMessage("Собеседование добавлено. Переадресация на страницу входа через 5 секунд...");
                    redirectTimeoutToken = setTimeout(() => {
                        location.assign("/");
                    }, 5000);
                }
                else
                    handleRequestErrors(response);
            }).then(() => {
                document.getElementById("create-button").disabled = false;
            });
    }
}
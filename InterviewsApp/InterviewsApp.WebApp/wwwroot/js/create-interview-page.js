function getPositions(createdPosition) {
    fetch("https://localhost:7262/api/Position/GetPositionsByUser?userId=" + sessionStorage.getItem("userId"), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem("AccessToken")
        }
    }).then((response) => {
        response.json()
            .then(function (data) {
                let sel = document.getElementById("position-select");
                if (data.length > 0) {
                    for (let i = 0; i < data.length; i++) {
                        let opt = document.createElement("option");
                        opt.value = data[i].id;
                        opt.text = data[i].name;
                        if (data[i].id === createdPosition)
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
                document.getElementById("hidden-button-cell").style = "";
            });
    });
}
function createInterview() {
    let name = document.getElementById("interviewName").value;
    let datetime = document.getElementById("interviewTime").value+"Z";
    let position = document.getElementById("position-select").value;
    if (!checkEmpty(name, datetime, position)) {
        document.getElementById("create-button").disabled = true;
        fetch('https://localhost:7262/api/Interview/AddInterview',
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
                if (response.ok === true) {
                    document.getElementById("manual-redirect").style = "";
                    setMessage("Собеседование добавлено. Переадресация на страницу входа через 5 секунд...");
                    redirectTimeoutToken = setTimeout(() => {
                        location.assign("/");
                    }, 5000);
                }
                else {
                    try {
                        response.json().then((data) => {
                            setMessage("");
                            if (data.errors.Name)
                                addMultipleMessageHtml(data.errors.Name);
                            if (data.errors.Login)
                                addMultipleMessageHtml(data.errors.Login);
                            if (data.errors.Password)
                                addMultipleMessageHtml(data.errors.Password);
                            document.getElementById("register-button").disabled = false;
                        })
                    }
                    catch (e) {
                        setMessage("Добавление неуспешно. Код ошибки: " + response.status);
                    }
                }
            })
    }
}
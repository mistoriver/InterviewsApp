function getPositions() {
    fetch("https://localhost:7262/api/Position/GetPositionsByUser?userId=1" + sessionStorage.getItem("userId"), {
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
                        sel.appendChild(opt);
                    }
                }
                else {
                    let opt = document.createElement("option");
                    opt.text = "У вас нет вакансий, для которых можно добавить собеседование.";
                    sel.appendChild(opt);
                    sel.disabled = true;
                    document.getElementById("create-button").disabled = true;
                    document.getElementById("hidden-button-cell").style = "";
                }
            })
    });
}
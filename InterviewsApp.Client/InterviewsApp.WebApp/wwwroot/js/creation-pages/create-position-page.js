﻿function getCompanies(createdCompany) {
    fetch(apihost + "/Company/GetCompanies", {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem("AccessToken")
        }
    }).then((response) => {
        response.json()
            .then(function (data) {
                let sel = document.getElementById("company-select");
                if (data.length > 0) {
                    for (let i = 0; i < data.length; i++) {
                        let opt = document.createElement("option");
                        opt.value = data[i].id;
                        opt.text = data[i].name;
                        if (data[i].id === createdCompany)
                            opt.selected = true;
                        sel.appendChild(opt);
                    }
                }
                else {
                    let opt = document.createElement("option");
                    opt.text = "Компании не найдены.";
                    sel.appendChild(opt);
                    sel.disabled = true;
                    document.getElementById("create-button").disabled = true;
                }
            })
    });
}

function createPosition() {
    let name = document.getElementById("position-name").value;
    let cityName = document.getElementById("city-name").value;
    let company = document.getElementById("company-select").value;
    if (!checkEmpty(name, cityName, company)) {
        document.getElementById("create-button").disabled = true;
        fetch(apihost + '/Position/CreatePosition',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
                },
                body: JSON.stringify({
                    name: name,
                    city: cityName,
                    companyId: company,
                    userId: sessionStorage.getItem(currentUserId)
                })
            }).then((response) => {
                if (response.ok === true) {
                    response.json().then((data) => {
                        document.getElementById("manual-redirect").style = "";
                        setMessage("Позиция успешно создана. Переадресация на страницу создания собеседования...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/Create/Interview?CreatedPosition=" + data);
                        }, 1000);
                    })
                }
                else {
                    try {
                        response.json().then((data) => {
                            setMessage("");
                            addRequestErrorsToMessage(data);
                        })
                    }
                    catch (e) {
                        setMessage("Создание неуспешно. Код ошибки: " + response.status);
                    }
                }
            }).then(() => {
                document.getElementById("create-button").disabled = false;
            });
    }
}
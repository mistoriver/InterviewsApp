function getCompanies(createdCompany) {
    fetch(apihost + "/Company/GetCompanies", {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem("AccessToken")
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    let sel = document.getElementById("company-select");
                    if (data.responseData.length > 0) {
                        for (let i = 0; i < data.responseData.length; i++) {
                            let opt = document.createElement("option");
                            opt.value = data.responseData[i].id;
                            opt.text = data.responseData[i].name;
                            if (data.responseData[i].id === createdCompany)
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
                });
        else
            handleRequestErrors(response);
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
                if (response.ok) {
                    response.json().then((data) => {
                        document.getElementById("manual-redirect").style = "";
                        setMessage("Позиция успешно создана. Переадресация на страницу создания собеседования...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/Create/Interview?CreatedPosition=" + data.responseData);
                        }, 1000);
                    })
                }
                else
                    handleRequestErrors(response);
            }).then(() => {
                document.getElementById("create-button").disabled = false;
            });
    }
}
function getCompanies(createdCompany) {
    setMessage("");
    fetch(apihost + "/Company/GetCompanies", {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
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
                        opt.text = localStorage.getItem("currentLocal") === "RU" ?
                            "Компании не найдены."
                            : "Companies not found";
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
    setMessage("");
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
                    "Authorization": "Bearer " + Cookies.get(tokenKey)
                },
                body: JSON.stringify({
                    name: name,
                    city: cityName,
                    companyId: company,
                    userId: Cookies.get(currentUserId)
                })
            }).then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        document.getElementById("manual-redirect").style = "";
                        setMessage(localStorage.getItem("currentLocal") === "RU" ?
                            "Вакансия успешно создана. Переадресация на страницу создания собеседования..."
                            : "Position created successfully. Redirecting to the interview creation page...");
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
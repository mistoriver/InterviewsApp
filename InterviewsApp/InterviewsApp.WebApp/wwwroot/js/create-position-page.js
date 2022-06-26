function getCompanies() {
    fetch("https://localhost:7262/api/Company/GetCompanies", {
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
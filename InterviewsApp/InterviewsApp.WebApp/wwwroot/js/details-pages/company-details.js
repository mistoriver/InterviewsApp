function getCompanyInfo(id) {
    fetch("https://localhost:7262/api/Company/Get?id=" + id, {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("company-name").innerText = data.name;
                }
            })
    });
}
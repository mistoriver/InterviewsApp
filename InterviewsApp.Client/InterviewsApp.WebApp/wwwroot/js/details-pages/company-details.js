function getCompanyInfo(id) {
    fetch(apihost + "/Company/Get?id=" + id, {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("company-name").innerText += ' "' + data.name + '"';
                    let rating = document.getElementById("company-rating");
                    rating.innerText = data.rating / 10;
                    rating.style = "color: " + (data.rating > 7? "green" : (data.rating < 5? "red" : "orange")) + "; font-weight: 900;"
                }
            })
    });
}
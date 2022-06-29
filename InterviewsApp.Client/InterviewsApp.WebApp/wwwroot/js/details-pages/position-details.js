function getPositionInfo(id) {
    fetch(apihost + "/Position/GetByUser?id=" + id + "&userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("position-name").innerText += ' "' + data.name + '"';
                    document.getElementById("city-name").innerText = data.city;
                    document.getElementById("comment").innerText = data.comment;
                    let comp = document.getElementById("company-name");
                    let aComp = document.createElement('a');
                    aComp.href = '/Details/Company?CompanyId=' + data.companyId;
                    aComp.innerText = data.companyName;
                    comp.appendChild(aComp);
                }
            })
    });
}
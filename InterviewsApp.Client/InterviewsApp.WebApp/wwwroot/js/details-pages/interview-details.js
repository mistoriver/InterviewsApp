function getInterviewInfo(id) {
    fetch(apihost + "/Interview/Get?id="+ id +"&userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
        }
    }).then((response) => {
        response.json()
            .then((data) => {
                if (data) {
                    document.getElementById("interview-name").innerText = data.name;
                    document.getElementById("interview-time").value = data.date.replace('Z', '');
                    let pos = document.getElementById("position-name");
                    let aPos = document.createElement('a');
                    aPos.href = '/Details/Position?PositionId=' + data.positionId;
                    aPos.innerText = data.positionName;
                    pos.appendChild(aPos);
                    let comp = document.getElementById("company-name");
                    let aComp = document.createElement('a');
                    aComp.href = '/Details/Company?CompanyId=' + data.companyId;
                    aComp.innerText = data.companyName;
                    comp.appendChild(aComp);
                }
            })
    });
}
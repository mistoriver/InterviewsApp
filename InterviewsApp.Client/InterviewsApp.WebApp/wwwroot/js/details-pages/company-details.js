let currentRate = 0;

function getCompanyInfo(id) {
    fetch(apihost + "/Company/Get?id=" + id, {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then((data) => {
                    if (data) {
                        document.getElementById("company-name").innerText += ' "' + data.responseData.name + '"';
                        let rating = document.getElementById("company-rating");
                        let dbRating = data.responseData.rating / 10
                        rating.innerText = dbRating / 2;
                        rating.style = "color: " + (dbRating > 8 ? "green" : (dbRating < 5 ? "red" : "orange")) + "; font-weight: 900;"
                        getRateFromDb(id);
                    }
                });
        else
            handleRequestErrors(response);
    });
}
function handleHover(event) {
    let num = event.target.id.substring(5);
    setRate(num);    
}
function clearStar() {
    for (let i = 0; i <= 50; i += 5) {
        if (i % 10 > 0) {
            let elem = document.getElementById("star-" + i);
            elem.classList.value = "";
            elem.classList.add("fa-regular");
            elem.classList.add("fa-star");
        }
    }
    setRate(currentRate);
}
function setRate(rate) {
    for (let i = 0; i <= 50; i += 5) {
        let elem = document.getElementById("star-" + i);
        let prevelem = document.getElementById("star-" + (i - 5));
        elem.classList.value = "";
        if (i % 10 > 0) {
            if (i < rate) {
                elem.classList.add("fa-solid");
                elem.classList.add("fa-star");
            }
            else
                if (rate - i < 5 && rate - i >= 0) {
                    elem.classList.add("fa-solid");
                    elem.classList.add("fa-star-half-stroke");
                }
                else {
                    elem.classList.add("fa-regular");
                    elem.classList.add("fa-star");
                }
        }
        else if (i == rate) {
            if (prevelem) {
                elem.classList.value = "";
                prevelem.classList.add("fa-solid");
                prevelem.classList.add("fa-star");
            }
        }
    }
}
function getRateFromDb(id) {
    fetch(apihost + "/Company/GetCompanyRate?id=" + id + "&userId=" + Cookies.get(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then((data) => {
                    if (data) {
                        currentRate = data.responseData / 2;
                        setRate(currentRate);
                    }
                });
        else
            handleRequestErrors(response);
    });
}
function rateCompany(id, event) {
    newRate = event.target.id.substring(5) * 2;
    fetch(apihost + "/Company/RateCompany?id=" + id + "&userId=" + Cookies.get(currentUserId) + "&rate=" + newRate, {
        method: "PUT", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + Cookies.get(tokenKey)
        }
    }).then((response) => {
        location.reload();
    });
}
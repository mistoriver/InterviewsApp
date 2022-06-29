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
function handleHover(event) {
    let num = event.target.id.substring(5);
    for (let i = 0; i <= 50; i += 5) {
        let elem = document.getElementById("star-" + i);
        elem.classList.value = "";
        if (i % 10 > 0) {
            if (i < num) {
                elem.classList.add("fa-solid");
                elem.classList.add("fa-star");
            }
            else
                if (i == num) {
                    elem.classList.add("fa-solid");
                    elem.classList.add("fa-star-half-stroke");
                }
                else {
                    elem.classList.add("fa-regular");
                    elem.classList.add("fa-star");
                }
        }
        else if (i == num){
            let prevelem = document.getElementById("star-" + (i - 5));
            if (prevelem) {
                elem.classList.value = "";
                prevelem.classList.add("fa-solid");
                prevelem.classList.add("fa-star");
            }
        }
    }
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
}
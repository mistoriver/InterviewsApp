function createCompany() {
    let name = document.getElementById("company-name").value;
    if (!checkEmpty(name)) {
        document.getElementById("create-button").disabled = true;
        fetch('https://localhost:7262/api/Company/CreateCompany',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + sessionStorage.getItem(tokenKey)
                },
                body: JSON.stringify({
                    name: name
                })
            }).then((response) => {
                if (response.ok === true) {
                    response.json().then((data) => {
                        document.getElementById("manual-redirect").style = "";
                        setMessage("Компания успешно добавлена. Переадресация на страницу создания вакансии...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/Create/Position?CreatedCompany=" + data);
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
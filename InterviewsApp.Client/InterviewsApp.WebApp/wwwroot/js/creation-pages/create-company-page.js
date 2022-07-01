function createCompany() {
    setMessage("");
    let name = document.getElementById("company-name").value;
    if (!checkEmpty(name)) {
        document.getElementById("create-button").disabled = true;
        fetch(apihost + '/Company/CreateCompany',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + Cookies.get(tokenKey)
                },
                body: JSON.stringify({
                    name: name
                })
            }).then((response) => {
                if (response.ok) {
                    response.json().then((data) => {
                        document.getElementById("manual-redirect").style = "";
                        setMessage("Компания успешно добавлена. Переадресация на страницу создания вакансии...");
                        redirectTimeoutToken = setTimeout(() => {
                            location.assign("/Create/Position?CreatedCompany=" + data.responseData);
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
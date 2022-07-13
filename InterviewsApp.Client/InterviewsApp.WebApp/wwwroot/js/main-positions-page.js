function getPositions(params) {
    document.getElementById("my-positions-tab").style = "font-weight: bold;text-decoration: underline;";
    document.getElementById("my-interviews-tab").style = "";
    let token = Cookies.get(tokenKey);
    $('#positions-table').bootstrapTable("showLoading");
    let dt = new Date()
    let corrDt = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes() - dt.getTimezoneOffset());
    currDTString = corrDt.toISOString();
    fetch(apihost + "/Position/GetMultiplePositionsByUser?userId=" + Cookies.get(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    }).then((response) => {
        if (response.ok)
            response.json()
                .then(function (data) {
                    params.success(data.responseData);
                });
        else
            handleRequestErrors(response);
        $('#positions-table').bootstrapTable("hideLoading");
    });
}
function positionFormatter(value, row) {
    return '<a  href="/Details/Position?PositionId=' + row.id + '">' + value + ' <a/>'
}
function companyFormatter(value, row) {
    return '<a  href="/Details/Company?CompanyId=' + row.companyId + '">' + value + ' <a/>'
}
function rowStyle(row, index) {
    if (row.offerReceived)
        return {
            css: { background: '#cee8be' }
        }
    if (row.denialReceived)
        return {
            css: { background: '#bf8c8c' }
        }
    return {}
}
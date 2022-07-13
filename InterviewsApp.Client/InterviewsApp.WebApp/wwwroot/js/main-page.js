var currDTString;
let onlyfuture;
function start() {
    fetch(apihost +'/User/GetUsers', { method: 'GET' });
}

function setCheckbox(checked) {
    onlyfuture = checked === "True";
    document.getElementById("showOnlyFuture").checked = onlyfuture;
}
function updateFuture() {
    onlyfuture = document.getElementById("showOnlyFuture").checked
    location.assign(onlyfuture? "/?OnlyFuture=true" : "/");
}

function getInterviews(params) {
    document.getElementById("my-positions-tab").style = "";
    document.getElementById("my-interviews-tab").style = "font-weight: bold;text-decoration: underline;";
    let token = Cookies.get(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    let dt = new Date()
    let corrDt = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes() - dt.getTimezoneOffset());
    currDTString = corrDt.toISOString();
    fetch(apihost + "/Interview/GetMultipleInterviewsByUser?userId=" + Cookies.get(currentUserId) + (onlyfuture ? "&showOnlyFuture=true" : ""), {
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
        $('#interviews-table').bootstrapTable("hideLoading");
    });
}
function dateFormatter(value) {
    value = value.split('.')[0].replace('Z', '');
    return '<input type="datetime-local" value="' + value + '" disabled/>'
}
function interviewFormatter(value, row) {
    return '<a  href="/Details/Interview?InterviewId=' + row.id + '">' + value + ' <a/>'
}
function positionFormatter(value, row) {
    return '<a  href="/Details/Position?PositionId=' + row.positionId + '">' + value + ' <a/>'
}
function companyFormatter(value, row) {
    return '<a  href="/Details/Company?CompanyId=' + row.companyId + '">' + value + ' <a/>'
}
function rowStyle(row, index) {
    if (row.offerReceived)
        return {
            css: { background: '#cee8be'}
        }
    if (row.denialReceived)
        return {
            css: { background: '#bf8c8c' }
        }
    if (row.date < currDTString) {
        return {
            css: { background: '#e3e3e3' }
        }
    }
    return {}
}
var currDTString;
function start() {
    fetch(apihost +'/User/GetUsers', { method: 'GET' });
}

function getInterviews(params) {
    let token = Cookies.get(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    currDTString = new Date().toISOString();
    fetch(apihost + "/Interview/GetMultipleInterviewsByUser?userId=" + Cookies.get(currentUserId), {
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
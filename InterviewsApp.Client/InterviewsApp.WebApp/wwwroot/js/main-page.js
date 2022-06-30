function start() {
    fetch(apihost +'/User/GetUsers', { method: 'GET' });
}

function getInterviews(params) {
    let token = sessionStorage.getItem(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    fetch(apihost + "/Interview/GetMultipleInterviewsByUser?userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    }).then((response) => {
        response.json()
            .then(function (data) {
                params.success(data.responseData);
                $('#interviews-table').bootstrapTable("hideLoading");
            })
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
    return {}
}
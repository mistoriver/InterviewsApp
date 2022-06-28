function start() {
    fetch(apihost +'/User/GetUsers', { method: 'GET' });
}

function getInterviews(params) {
    let token = sessionStorage.getItem(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    fetch(apihost + "/Interview/GetInterviewsByUserForUi?userId=" + sessionStorage.getItem(currentUserId), {
        method: "GET", headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    }).then((response) => {
        response.json()
            .then(function (data) {
                params.success(data);
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

function getInterviewData(id) {
    fetch(apihost + '/Interview/Get?id=' + id)
        .then((response) => response.json())
        .then((data) => {
            document.getElementById('interview-datetime').value = data.date.replace('Z', '');
            document.getElementById('interview-title').innerText = data.name;
            document.getElementById('interview-position').innerText = data.positionId;
        });
}
function start() {
    fetch('https://localhost:7262/api/User/GetUsers', { method: 'GET' });
}

function getInterviews(params) {
    let token = sessionStorage.getItem(tokenKey);
    $('#interviews-table').bootstrapTable("showLoading");
    fetch("https://localhost:7262/api/Interview/GetInterviewsByUser?userId=" + sessionStorage.getItem(currentUserId), {
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
    return '<input type="datetime-local" value="' + value.replace('Z', '') + '" disabled/>'
}

function getInterviewData(id) {
    fetch('https://localhost:7262/api/Interview/Get?id=' + id)
        .then((response) => response.json())
        .then((data) => {
            document.getElementById('interview-datetime').value = data.date.replace('Z', '');
            document.getElementById('interview-title').innerText = data.name;
            document.getElementById('interview-position').innerText = data.positionId;
        });
}
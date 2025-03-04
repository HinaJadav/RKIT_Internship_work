$(function () {
    let users = [
        { "Name": "Rohan Sharma", "Email": "rohan.sharma@example.com", "Role": "Admin" },
        { "Name": "Priya Iyer", "Email": "priya.iyer@example.com", "Role": "Developer" },
        { "Name": "Amit Patel", "Email": "amit.patel@example.com", "Role": "Tester" },
        { "Name": "Neha Verma", "Email": "neha.verma@example.com", "Role": "Developer" },
        { "Name": "Arjun Reddy", "Email": "arjun.reddy@example.com", "Role": "Tester" },
        { "Name": "Rahul Das", "Email": "rahul.das@example.com", "Role": "Admin" }
    ];

    // Dragging logic
    $(".role").on("dragstart", function (event) {
        event.originalEvent.dataTransfer.setData("text", $(this).data("role"));
    });

    // Drop Zone logic
    $("#dropZone").on("dragover", function (event) {
        event.preventDefault();
    });

    $("#dropZone").on("drop", function (event) {
        event.preventDefault();
        let role = event.originalEvent.dataTransfer.getData("text");

        // Filter users based on role
        let filteredUsers = users.filter(user => user.Role === role);

        // Show users in drop zone
        let dropZone = $("#dropZone");
        dropZone.html(`<h3>${role} Users:</h3>`);
        filteredUsers.forEach(user => {
            dropZone.append(`<div class="user">${user.Name} - ${user.Email}</div>`);
        });
    });
});

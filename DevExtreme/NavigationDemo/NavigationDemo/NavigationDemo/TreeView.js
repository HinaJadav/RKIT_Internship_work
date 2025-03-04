$(function () {
    let users = [
        { "Id": 1, "Name": "Rohan Sharma", "Email": "rohan.sharma@example.com", "Role": "Admin" },
        { "Id": 2, "Name": "Priya Iyer", "Email": "priya.iyer@example.com", "Role": "Developer" },
        { "Id": 3, "Name": "Amit Patel", "Email": "amit.patel@example.com", "Role": "Tester" },
        { "Id": 4, "Name": "Neha Verma", "Email": "neha.verma@example.com", "Role": "Developer" },
        { "Id": 5, "Name": "Arjun Reddy", "Email": "arjun.reddy@example.com", "Role": "Tester" },
        { "Id": 6, "Name": "Kavita Singh", "Email": "kavita.singh@example.com", "Role": "Developer" },
        { "Id": 7, "Name": "Rahul Das", "Email": "rahul.das@example.com", "Role": "Admin" },
        { "Id": 8, "Name": "Meera Joshi", "Email": "meera.joshi@example.com", "Role": "Tester" },
        { "Id": 9, "Name": "Sanjay Kulkarni", "Email": "sanjay.kulkarni@example.com", "Role": "Developer" },
        { "Id": 10, "Name": "Anjali Nair", "Email": "anjali.nair@example.com", "Role": "Tester" }
    ];

    // Transform data: Group by Role
    let transformedData = [];
    let roleMap = new Map();
    let idCounter = 1;

    users.forEach(user => {
        let role = user.Role;

        // Add role as parent if not already added
        if (!roleMap.has(role)) {
            roleMap.set(role, idCounter);
            transformedData.push({
                id: idCounter,
                name: role,
                parentId: 0, // Root-level node (parentId = 0)
                isRole: true // Custom property to differentiate roles
            });
            idCounter++;
        }

        // Add user as child node under their role
        transformedData.push({
            id: idCounter,
            name: user.Name,
            parentId: roleMap.get(role),
            email: user.Email,
            role: user.Role,
            isRole: false
        });

        idCounter++;
    });

    // Initialize dxTreeView with drag & drop support
    $("#treeView").dxTreeView({
        dataSource: transformedData,
        dataStructure: "plain",
        parentIdExpr: "parentId",
        keyExpr: "id",
        displayExpr: "name",
        searchEnabled: true,
        selectionMode: "single",
        selectByClick: true,

        onItemSelectionChanged: function (e) {
            const selectedUser = e.itemData;

            if (!selectedUser.isRole) {
                $("#userDetails").removeClass("hidden");
                $("#name").text("Name: " + selectedUser.name);
                $("#email").text("Email: " + selectedUser.email);
                $("#role").text("Role: " + selectedUser.role);
            } else {
                $("#userDetails").addClass("hidden");
            }
        },

        onItemClick: function (e) {
            console.log("Clicked Node:", e.itemData);
        },

        onContentReady: function () {
            // Make role elements draggable
            $(".dx-treeview-node").each(function () {
                let text = $(this).text().trim();
                if (roleMap.has(text)) {
                    $(this).attr("draggable", "true"); // Enable dragging
                    $(this).on("dragstart", function (event) {
                        event.originalEvent.dataTransfer.setData("text/plain", text);
                    });
                }
            });
        }
    });

    // Drop Zone Events
    $("#dropZone")
        .on("dragover", function (event) {
            event.preventDefault(); // Allow drop
        })
        .on("drop", function (event) {
            event.preventDefault();
            let role = event.originalEvent.dataTransfer.getData("text/plain");

            // Get users under this role
            let filteredUsers = users.filter(user => user.Role === role);

            // Append users to the drop zone
            let dropZone = $("#dropZone");
            dropZone.html(`<h3>${role}</h3>`);
            filteredUsers.forEach(user => {
                dropZone.append(`<div class="user">${user.Name} - ${user.Email}</div>`);
            });
        });
});


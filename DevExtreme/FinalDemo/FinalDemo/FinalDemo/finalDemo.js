var menuItems = [
    {
        name: 'DashBoard',
        icon: 'home',
    },
    {
        name: 'Bugs',
        icon: 'bug',
        items: [
            {
                name: 'Add Bug',
                icon: 'add',
            },
            {
                name: 'Bug List',
                icon: 'list',
            },
            {
                name: 'Bug Reports',
                icon: 'report',
            },
            {
                name: 'Bug History',
                icon: 'history',
            }
        ]
    },
    {
        name: 'Settings',
        icon: 'settings',
        items: [
            {
                name: 'Preferences',
                icon: 'preferences',
            },
            {
                name: 'System Logs',
                icon: 'logs',
            }
        ]
    },
    {
        name: 'Help',
        icon: 'help',
        items: [
            {
                name: 'User Guide',
                icon: 'book',
            },
            {
                name: 'Support',
                icon: 'support',
            }
        ]
    }
];


$(function () {
    let menuInstance = $('#menu').dxMenu({
        items: menuItems,
        displayExpr: 'name',
        orientation: 'vertical',
        hideSubmenuOnMouseLeave: true,
        onItemClick: function (e) {
            let itemName = e.itemData.name;

            if (itemName === 'Add Bug') {
                showPopup('Add Bug');
            }
        },
    });

    let popupInstance = $('#popup').dxPopup({
        title: 'Add Bug',
        width: 400,
        height: 'auto',
        showCloseButton: true,
        dragEnabled: true,
        closeOnOutsideClick: true,
        contentTemplate: function () {
            let $form = $('<div id="bugFormContainer"></div>');

            // Append Form Fields
            $form.append(
                $("<label for='title' class='form-label'>Title:</label>"),
                $("<div class='form-control' id='title'></div>"),

                $("<label for='description' class='form-label'>Description:</label>"),
                $("<div class='form-control' id='description'></div>"),

                $("<label for='status' class='form-label'>Status:</label>"),
                $("<div class='form-control' id='status'></div>"),

                $("<label for='priority' class='form-label'>Priority:</label>"),
                $("<div class='form-control' id='priority'></div>"),

                $("<label for='severity' class='form-label'>Severity:</label>"),
                $("<div class='form-control' id='severity'></div>"),

                $("<label for='bugCategory' class='form-label'>Bug Category:</label>"),
                $("<div class='form-control' id='bugCategory'></div>"),

                $("<label for='assignedUser' class='form-label'>Assigned User:</label>"),
                $("<div class='form-control' id='assignedUser'></div>"),

                $('<div class="form-control" id="submitButton"></div>')
            );

            return $form;
        },
        onShown: function () {
            // Initialize Form Fields when Popup is shown
            $("#title").dxTextBox({
                placeholder: "Enter bug title"
            });
            $("#description").dxTextArea({
                placeholder: "Enter bug description"
            });
            $("#status").dxSelectBox({
                dataSource: ['Open', 'In Progress', 'Resolved', 'Closed'],
                placeholder: "Select status"
            });
            $("#priority").dxSelectBox({
                dataSource: ['Low', 'Medium', 'High'],
                placeholder: "Select priority"
            });
            $("#severity").dxSelectBox({
                dataSource: ['Minor', 'Major', 'Critical'],
                placeholder: "Select severity"
            });
            $("#bugCategory").dxTextBox({
                placeholder: "Enter bug category"
            });
            $("#assignedUser").dxTextBox({
                placeholder: "Enter assigned user"
            });

            // Submit Button
            $("#submitButton").dxButton({
                text: "Submit",
                type: "success",
                onClick: function () {
                    var title = $("#title").dxTextBox("instance").option("value");
                    var description = $("#description").dxTextArea("instance").option("value");
                    var status = $("#status").dxSelectBox("instance").option("value");
                    var priority = $("#priority").dxSelectBox("instance").option("value");
                    var severity = $("#severity").dxSelectBox("instance").option("value");
                    var bugCategory = $('#bugCategory').dxTextBox('instance').option('value');
                    var assignedUser = $('#assignedUser').dxTextBox('instance').option('value');

                    if (!title || !description || !status || !priority || !severity || !bugCategory || !assignedUser) {
                        alert("Please fill all fields!");
                        return;
                    }

                    $.ajax({
                        url: "https://67c68a2f351c081993fdadd8.mockapi.io/Bug", // Sample API
                        method: "POST",
                        data: {
                            title,
                            description,
                            status,
                            priority,
                            severity,
                            bugCategory,
                            assignedUser
                        },
                        dataType: "json",
                        success: function (data) {
                            showToast("Bug added successfully!", "success");
                        },
                        error: function () {
                            showToast("Failed to add bug!", "error");
                        }
                    });

                    // Close Popup after Submission
                    popupInstance.hide();
                }
            });
        }
    }).dxPopup("instance");

    // Function to Show Popup Dynamically
    function showPopup(title) {
        popupInstance.show();
    }

    var toast = $("#toast").dxToast({
        message: null,
        type: null,
        displayTime: 3000,
        position: { my: "center bottom", at: "center bottom", of: window },
        width: "auto",
        height: "auto",
        animation: {
            show: { type: "fade", duration: 600, from: 0, to: 1 },
            hide: { type: "fade", duration: 600, from: 1, to: 0 },
        },
        contentTemplate: function (data) {
            return $('<div class="toast-content"><i class="dx-icon-' + (data.type === 'success' ? 'check' : 'close') + '"></i> ' + data.message + '</div>');
        }
    }).dxToast("instance");

    function showToast(message, type) {
        toast.option("message", message);
        toast.option("type", type);
        toast.show();
    }
});

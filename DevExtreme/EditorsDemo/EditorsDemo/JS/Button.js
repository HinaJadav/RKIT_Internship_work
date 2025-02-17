$(function () {
   
    $("#personalInfoButton").dxButton({
        accessKey: 'p', // Shortcut key to activate the button
        hint: "Redirect to personal information section", // Tooltip hint
        hoverStateEnabled: true, // Enable hover state for the button
        stylingMode: "outlined", // Button style mode (outlined, contained)
        tabIndex: 1, // Set the tab order
        text: "Personal Info", // Button text
        type: "normal", // Button type (normal, default, success, danger, etc.)
        icon: "user", // Icon to display
        onClick: function () {
            // Redirect to TextBox.html page on button click
            window.location.href = "TextBox.html";
        }
    });

    
    $("#basicInfoButton").dxButton({
        accessKey: 'b', 
        hint: "Redirect to basic information section",
        hoverStateEnabled: true,
        stylingMode: "outlined", 
        tabIndex: 2, 
        text: "Basic Info", 
        type: "default",
        icon: "event", 
        onClick: function () {
            
            window.location.href = "DateBox.html";
        }
    });

    $("#addressInfoButton").dxButton({
        accessKey: 'a', 
        hint: "Redirect to address information section", 
        hoverStateEnabled: true, 
        stylingMode: "outlined", 
        tabIndex: 3, 
        text: "Address Info", 
        type: "default", 
        icon: "map", 
        onClick: function () {
            
            window.location.href = "DropDownBox.html";
        }
    });

    $("#resultInfoButton").dxButton({
        accessKey: 'r', 
        hint: "Redirect to results information section", 
        hoverStateEnabled: true, 
        stylingMode: "outlined", 
        tabIndex: 4, 
        text: "Result Info", 
        type: "info",
        icon: "chart",
        onClick: function () {
            window.location.href = "NumberBox.html";
        }
    });

    $("#skillsInfoButton").dxButton({
        accessKey: 's', 
        hint: "Redirect to skills information section", 
        hoverStateEnabled: true,
        stylingMode: "outlined", 
        tabIndex: 5,
        text: "Skills", 
        type: "info", 
        icon: "favorites",
        onClick: function () {
            window.location.href = "SelectBox.html";
        }
    });

    $("#feedBackButton").dxButton({
        accessKey: 'f', 
        hint: "Redirect to feedback section", 
        hoverStateEnabled: true, 
        stylingMode: "outlined", 
        tabIndex: 6, 
        text: "Feedback",
        type: "normal", 
        icon: "comment", 
        onClick: function () {
            window.location.href = "TextArea.html";
        }
    });

    $("#profileImageButton").dxButton({
        accessKey: 'i', 
        hint: "Update profile image",
        hoverStateEnabled: true, 
        stylingMode: "outlined",
        tabIndex: 8, 
        icon: "image",
        onClick: function () {
            alert("Update profile image!");
        },
        optionChanged: function (e) {
            console.log("Button option changed:", e);
        }
    });

    $("#closeButton").dxButton({
        accessKey: 'c', 
        hint: "Close or cancel the action", 
        hoverStateEnabled: true, 
        stylingMode: "outlined", 
        tabIndex: 9, 
        disabled: true, 
        type: "danger", 
        icon: "close"
    });

   

    $("#saveButton").dxButton({
        accessKey: 's',
        hint: "Save and create a new user button",
        hoverStateEnabled: true,
        stylingMode: "contained",
        type: "success",
        icon: "save",
        onClick: function () {
            // Dynamically create a new "User" button
            var newButton = $("<div>").appendTo("#userButton"); // Create and append to DOM first

            newButton.dxButton({
                text: "User",  // Button text
                icon: "user",  // Button icon
                stylingMode: "outlined", // Styling mode
                type: "default",  // Button type
                hint: "Newly created user button",  // Tooltip hint
                onClick: function () {
                    // Show notification when the "User" button is clicked
                    DevExpress.ui.notify("User button clicked!", "info", 2000);
                }
            });

            // Notify that the button was created
            DevExpress.ui.notify("User button created successfully!", "success", 2000);
        }
    });


    $("#deleteProfileButton").dxButton({
        accessKey: 'd', 
        hint: "Permanently delete profile-related buttons", 
        icon: "trash", 
        stylingMode: "outlined",
        type: "danger", 
        onClick: function () {
            // Confirmation dialog before deleting profile-related buttons
            DevExpress.ui.dialog.confirm(
                "Are you sure you want to delete profile?", // Confirm deletion message
                "Delete Confirmation" // Title of the confirmation dialog
            ).done(function (result) {
                if (result) {
                    // If the user confirms deletion
                    let button = $("#userButton").dxButton("instance"); // Get the instance of the user button
                    if (button) {
                        button.dispose(); // Dispose of the button instance
                        $("#userButton").remove(); // Remove the button element from DOM
                    }
                    // Notify user that profile-related buttons are deleted successfully
                    DevExpress.ui.notify("Profile-related buttons deleted successfully!", "error", 2000);
                }
            });
        }
    });

    // Event fired when the delete button is disposed
    $("#deleteProfileButton").on("disposing", function () {
        console.log("Delete Profile button has been removed from the UI."); // Log when the delete button is removed from UI
    });
});

                    //ERROR : delete user profile component 
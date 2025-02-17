$(function () {
    // Prevent UI refreshes while initializing
    DevExpress.ui.dxTextBox.beginUpdate();

    // First Name Field (Only Alphabets Allowed)
    let firstName = $("#firstName").dxTextBox({
        accessKey: 'f',
        hint: "Enter your first name",
        placeholder: "John",
        showClearButton: true,
        focusStateEnabled: true,
        stylingMode: "outlined",
        spellcheck: false,
        inputAttr: { 'aria-label': 'First Name' },
        mask: "AAAAAAAAAAAAAAAAAAAA",
        maskChar: "_",
        maskInvalidMessage: "Only alphabets are allowed!",
        validationRules: [{ type: "required", message: "First Name is required!" }],
    }).dxTextBox("instance");

    // Last Name Field
    let lastName = $("#lastName").dxTextBox({
        accessKey: 'l',
        hint: "Enter your last name",
        placeholder: "Doe",
        showClearButton: true,
        stylingMode: "outlined",
        inputAttr: { 'aria-label': 'Last Name' },
        mask: "AAAAAAAAAAAAAAAAAAAA",
        validationRules: [{ type: "required", message: "Last Name is required!" }],
    }).dxTextBox("instance");

    // Email Field with Dynamic Validation
    let email = $("#email").dxTextBox({
        accessKey: 'e',
        hint: "Enter your email",
        placeholder: "example@mail.com",
        showClearButton: true,
        maxLength: 50,
        stylingMode: "outlined",
        inputAttr: { 'aria-label': 'Email' },
        validationRules: [
            {
                type: "pattern",
                pattern: "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
                message: "Enter a valid email address!"
            }
        ],
        onValueChanged: function (e) {
            let maxLength = e.component.option("maxLength");
            if (e.value.length >= maxLength) {
                DevExpress.ui.notify(`Max ${maxLength} characters allowed!`, "warning", 2000);
                e.component.focus();
            }
        }
    }).dxTextBox("instance");

    // Contact Number Field (Only 10 Digits)
    let contactNumber = $("#contactNumber").dxTextBox({
        accessKey: 'c',
        hint: "Enter your contact number",
        placeholder: "1234567890",
        showClearButton: true,
        focusStateEnabled: true,
        stylingMode: "outlined",
        inputAttr: { 'aria-label': 'Contact Number' },
        mask: "0000000000", // Allows exactly 10 digits
        maskInvalidMessage: "Enter a valid 10-digit number!",
        validationRules: [{ type: "required", message: "Contact Number is required!" }],
    }).dxTextBox("instance");

    // End update after initialization to refresh UI
    DevExpress.ui.dxTextBox.endUpdate();

    // Reset Button (Uses built-in `reset()` method)
    $("#reset").dxButton({
        text: "Reset",
        type: "danger",
        stylingMode: "contained",
        onClick: function () {
            DevExpress.ui.dialog.confirm("Are you sure you want to reset all fields?", "Reset Confirmation").done(function (result) {
                if (result) {
                    firstName.reset();
                    lastName.reset();
                    email.reset();
                    contactNumber.reset();
                    firstName.focus(); // Focus on First Name after reset
                }
            });
        }
    });

    // Submit Button (Validation & Summary)
    $("#submit").dxButton({
        text: "Submit",
        type: "success",
        stylingMode: "contained",
        onClick: function () {
            let isValid = true;
            let summaryMessage = "Submitted Data:\n";

            [firstName, lastName, email, contactNumber].forEach(field => {
                let value = field.option("value");
                let hint = field.option("hint");

                if (!value) {
                    DevExpress.ui.notify(hint + " is required!", "error", 2000);
                    isValid = false;
                    return false;
                }
                summaryMessage += `${hint}: ${value}\n`;
            });

            if (isValid) {
                DevExpress.ui.dialog.alert(summaryMessage, "Submission Successful").done(function () {
                    $("#reset").dxButton("instance").option("onClick")();
                });
            }
        }
    });

    // Event: Focus First Name when page loads
    firstName.focus();

    // Event: Key Handler for Enter Key Submission
    firstName.registerKeyHandler("enter", function () {
        lastName.focus();
    });

    lastName.registerKeyHandler("enter", function () {
        email.focus();
    });

    email.registerKeyHandler("enter", function () {
        contactNumber.focus();
    });

    contactNumber.registerKeyHandler("enter", function () {
        $("#submit").dxButton("instance").option("onClick")();
    });

    // Event: Force UI update on change
    email.on("optionChanged", function () {
        email.repaint();
    });

    // Event: Remove focus on blur
    email.on("focusOut", function () {
        email.blur();
    });
});

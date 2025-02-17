$(function () {
    // Options  
    let firstName = $("#firstName").dxTextBox({
        accessKey: 'f',                  // Shortcut key for the input field
        hint: "Enter your first name",    // Hint text when the input is empty
        placeholder: "Priyank",           // Placeholder text
        showClearButton: true,            // Display clear button for input
        focusStateEnabled: true,          // Enable focus state styling
        stylingMode: "outlined",         // Set styling mode to outlined
        spellcheck: false,                // Disable spell check
        inputAttr: { 'aria-label': 'FirstName' },  // Accessibility label
        //mask: "AAAAAAAAAAAAAAAAAAAA",    // Mask to allow only alphabets
        maxLength:20,
        maskInvalidMessage: "Only alphabets are allowed!", // Invalid mask message
        validationRules: [{ type: "required", message: "First Name is required!" }], // Validation rule for required field
        width: 300
    }).dxTextBox("instance");

    // Begin and end update for the firstName instance
    firstName.beginUpdate();

    let lastName = $("#lastName").dxTextBox({
        accessKey: 'l',
        hint: "Enter your last name.",
        placeholder: "Jadav",
        showClearButton: true,
        stylingMode: "outlined",
        inputAttr: { 'aria-label': 'LastName' },
        maxLength: 20,
        //mask: "AAAAAAAAAAAAAAAAAAAA",  // Mask for alphabets only
        validationRules: [{ type: "required", message: "Last Name is required!" }], // Required validation
        width: 300,
    }).dxTextBox("instance");

    // Begin and end update for the lastName instance
    lastName.beginUpdate();

    let email = $("#email").dxTextBox({
        accessKey: 'e',
        hint: "Enter your email",
        placeholder: "example@gmail.com",
        showClearButton: true,
        maxLength: 30,                 // Limit max length to 30 characters
        stylingMode: "outlined",
        inputAttr: { 'aria-label': "Email" },
        validationRules: [
            {
                type: "pattern",
                pattern: "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", // Email pattern validation
                message: "Enter a valid email address!"
            },
            { type: "required", message: "Email is required!" }  // Email is required
        ],
        width: 300,
    }).dxTextBox("instance");

    // Begin and end update for the email instance
    email.beginUpdate();

    let contactNumber = $("#contactNumber").dxTextBox({
        accessKey: 'c',
        hint: "Enter your contact number",
        placeholder: "1234567894",
        showClearButton: true,
        stylingMode: "outlined",
        inputAttr: { 'aria-label': 'ContactNumber' },
        mask: "0000000000",              // Mask to allow only 10-digit contact numbers
        maskInvalidMessage: "Enter a valid 10-digit number!", // Invalid input message
        validationRules: [{ type: "required", message: "Contact number is required!" }], // Required validation
        width: 300,  
    }).dxTextBox("instance");
-
    // Begin and end update for the contactNumber instance
        contactNumber.beginUpdate();

    // End updates after initialization
    firstName.endUpdate();
    lastName.endUpdate();
    email.endUpdate();
    contactNumber.endUpdate();

    // Methods 
    $("#reset").dxButton({
        text: "Reset",
        type: "danger",                 // Button style for danger action
        stylingMode: "contained",
        onClick: function () {
            // Confirmation dialog before reset
            DevExpress.ui.dialog.confirm("Are you sure you want to reset all fields?", "Reset Confirmation").done(function (result) {
                if (result) {
                    firstName.reset();    // Reset all fields to their initial state
                    lastName.reset();
                    email.reset();
                    contactNumber.reset();
                    firstName.focus();     // Focus back to the first name field after reset
                }
            });
        }
    });

    // Submit Button: Validates all fields and shows a confirmation dialog with submitted data
    $("#submit").dxButton({
        text: "Submit",
        type: "success",                // Button style for success action
        stylingMode: "contained",
        onClick: function () {
            let isValid = true;
            let summaryMessage = "Submitted Data:\n";

            // Validate each field and create a summary of valid values
            [firstName, lastName, email, contactNumber].forEach(field => {
                let value = field.option("value");    // Get the value of each field
                let hint = field.option("hint");      // Get the hint for each field

                if (!value) {
                    DevExpress.ui.notify(hint + " is required!", "error", 2000);  // Notify if any field is empty
                    isValid = false;   // Mark form as invalid if a field is missing
                    return false;      // Stop processing on invalid form
                }
                summaryMessage += `${hint}: ${value}\n`;  // Add valid field values to the summary message
            });

            // If all fields are valid, show a success message
            if (isValid) {
                DevExpress.ui.dialog.alert(summaryMessage, "Submission Successful").done(function () {
                    $("#reset").dxButton("instance").option("onClick")();  // Call reset logic after submission
                });
            }
        }
    });

    // Events
    // Key handler for Enter key navigation through fields
    firstName.registerKeyHandler("enter", function () {
        lastName.focus();    // Focus on the last name field when Enter is pressed on first name
    });

    lastName.registerKeyHandler("enter", function () {
        email.focus();    // Focus on the email field when Enter is pressed on last name
    });

    email.registerKeyHandler("enter", function () {
        contactNumber.focus();    // Focus on the contact number field when Enter is pressed on email
    });

    contactNumber.registerKeyHandler("enter", function () {
        $("#submit").dxButton("instance").option("onClick")();  // Submit the form when Enter is pressed on contact number
    });

});

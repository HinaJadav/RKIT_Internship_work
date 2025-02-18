$(() => {

    const westIndianStates = ['Maharashtra', 'Gujarat', 'Rajasthan', 'Goa', 'Madhya Pradesh'];

    const sendRequest = (value) => {
        const invalidEmail = 'test@dx-email.com';
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve(value !== invalidEmail);
            }, 1000);
        });
    };

    const changePasswordMode = (selector) => {
        const editor = $(selector).dxTextBox("instance");
        editor.option("mode", editor.option("mode") === "text" ? "password" : "text");
    };

    // Validation Summary (Add the same validation group here)
    $("#validationSummary").dxValidationSummary({
        group: "studentForm" // Ensure the validation group matches
    });

    // Applying a consistent width and user-friendly label designs
    const textBoxOptions = {
        width: 300,
        showClearButton: true,
        inputAttr: { "aria-label": "Field" },
        labelMode: 'floating',  // Floating label effect
        validationGroup: "studentForm" // Assign validation group to each input
    };

    $("#name").dxTextBox({

        placeholder: "Enter Name",
        validationGroup: "studentForm", // Ensure the group is assigned here
        ...textBoxOptions
    }).dxValidator({
        validationRules: [
            { type: "required", message: "Name is required" },
            { type: "pattern", pattern: /^[^0-9]+$/, message: "Do not use digits in the Name." },
            { type: "stringLength", min: 2, message: "Name must have at least 2 symbols" }
        ]
    });

    $("#email").dxTextBox({
        placeholder: "Enter Email",
        validationGroup: "studentForm", // Ensure the group is assigned here
        ...textBoxOptions
    }).dxValidator({
        validationRules: [
            { type: "required", message: "Email is required" },
            { type: "email", message: "Email is invalid" },
            { type: "async", message: "Email is already registered", validationCallback: (params) => sendRequest(params.value) }
        ]
    });

    $("#password").dxTextBox({
        mode: "password",
        placeholder: "Enter Password",
        validationGroup: "studentForm",
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "eyeopen",
                stylingMode: "text",
                onClick: () => changePasswordMode("#password")
            }
        }],
        ...textBoxOptions
    }).dxValidator({
        validationRules: [
            { type: "required", message: "Password is required" },
            { type: "stringLength", min: 6, message: "Password must be at least 6 characters long" },
            { type: "pattern", pattern: "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", message: "Password must contain letters and numbers" }
        ]
    }); // Z3xy56 -- check it 

    $("#confirmPassword").dxTextBox({
        mode: "password",
        placeholder: "Confirm Password",
        validationGroup: "studentForm",
        buttons: [{
            name: "password",
            location: "after",
            options: {
                icon: "eyeopen",
                stylingMode: "text",
                onClick: () => changePasswordMode("#confirmPassword")
            }
        }],
        ...textBoxOptions
    }).dxValidator({
        validationRules: [
            {
                type: "compare",
                comparisonTarget: () => $("#password").dxTextBox("instance").option("value"), // Correct reference to compare the password value
                message: "Passwords do not match."
            },
            { type: "required", message: "Confirm Password is required" }
        ]
    });


    $("#contactNo").dxTextBox({
        placeholder: "Enter Contact Number",
        validationGroup: "studentForm", // Ensure the group is assigned here
        ...textBoxOptions
    }).dxValidator({
        validationRules: [
            { type: "required", message: "Contact number is required" },
            { type: "numeric", message: "Only numbers allowed" }
        ]
    });

    $("#state").dxSelectBox({
        dataSource: westIndianStates,
        placeholder: "Select State",
        acceptCustomValue: true,  // Allow user to enter values manually
        onCustomItemCreating(args) {
            if (!westIndianStates.includes(args.text)) {
                args.customItem = null;
            }
        },
        inputAttr: { 'aria-label': 'State' },
        validationGroup: "studentForm", // Ensure the group is assigned here
        width: 300,
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            validationCallback: ({ value }) => westIndianStates.includes(value),
            message: 'Only West Indian states are allowed',
        }]
    });

    $("#age").dxNumberBox({
        placeholder: "Enter Age",
        validationGroup: "studentForm", // Ensure the group is assigned here
        width: 300,
    }).dxValidator({
        validationRules: [
            { type: "required", message: "Age is required" },
            { type: "range", min: 18, max: 60, message: "Age must be between 18 and 60" }
        ]
    });

    $("#validateMe").dxCheckBox({
        text: "Validate me",
        validationGroup: "studentForm", // Ensure the group is assigned here
        width: 300,
    }).dxValidator({ validationRules: [{ type: "required", message: "You must check this box" }] });

    $("#submit").dxButton({
        text: "Submit",
        type: "success",
        width: 150,
        onClick: () => {
            let result = DevExpress.validationEngine.validateGroup("studentForm");
            if (result.isValid) {
                alert("Form submitted successfully!");
            } else {
                alert("Please fix validation errors.");
            }
        }
    });

    $("#reset").dxButton({
        text: "Reset",
        type: "default",
        width: 150,
        onClick: () => DevExpress.validationEngine.resetGroup("studentForm")
    });
});

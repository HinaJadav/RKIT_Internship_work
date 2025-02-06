$(function () {
    $("#signUpForm").dxForm({
        formData: {
            UserName: "Priyank", // TextBox
            Email: "priyank@gmail.com",
            Education: "B.Tech - CE",
            College: "DDU",
            StudentId: 21, // NumberBox
        }
    });

    // Get form instance
    var formInstance = $("#signUpForm").dxForm("instance");
    console.log("Form instance: ", formInstance);

    if (formInstance) {
        console.log("Form initialized successfully!");
        console.log("User name:", formInstance.option("formData.UserName"));
    } else {
        console.error("Error: formInstance is undefined!");
    }

    // Get "formData" object
    var formDataObj = formInstance.option("formData");
    console.log("Form Object: ", formDataObj);

    // Get single property from "formData" object
    var userName = formInstance.option("formData.UserName");
    console.log("User name: ", userName);

    // Alternative method to get a single property
    var userEmail = $("#signUpForm").dxForm("option", "formData.Email");
    console.log("User email: ", userEmail);

    // Get all properties
    var formAllData = formInstance.option();
    console.log("All User data: ", formAllData);

    // Set single property
    formInstance.option("formData.College", "DDIT");

    // Call method to suspend update
    formInstance.beginUpdate();  // Suspend update

    // Set multiple properties
    formInstance.option("formData", {
        UserName: "Priyank",
        Email: "priyank@gmail.com",
        Education: "BE - CE",
        College: "DDIT",
        StudentId: 11,
    });

    formInstance.endUpdate(); // Apply updates

    // Update a single field
    formInstance.updateData("formData.UserName", "Suresh");
    console.log("User name:", formInstance.option("formData.UserName"));

    formInstance.repaint(); // Ensure that update is applied
    console.log("User name after repaint:", formInstance.option("formData.UserName"));

    
    // Example: Adding a validation rule
    // # Remove when want to check for validation Demo

    /*$("#signUpForm").dxForm({
        formData: {
            UserName: "Priyank",
            Email: "priyank@gmail.com",
            Education: "B.Tech - CE",
            College: "DDU",
            StudentId: 21,
        },
        items: [{
            dataField: "UserName",
            validationRules: [{
                type: "required",
                message: "User Name is required"
            }]
        }]
    });*/

    // Validation
    var validationResult = formInstance.validate();
    console.log(validationResult.isValid); // It returns "true" if no validation rules are defined


    // Handle Events

    // Login Button
    $("#LoginButton").dxButton({
        text: "Login",
        onClick: function () {
            // Show an alert on click of Login Button
            alert("You have successfully signed up!");
        }
    });

    // Reset Button
    

    $("#ResetButton").dxButton({
        text: "Reset",
        onClick: function () {
            // Show an alert on click of Reset Button
            alert("Your data has been successfully reset.");
        }
    });

    $("#ResetButton").on("mouseenter", function () {
        console.log("Warning: Clicking this will reset your data!");
    });

    // Destroying the widget (be careful with destroying widgets that are in use)
    // Ensure widgets are not in use before destroying them

    // Destroying the form widget instance (uncomment if you want to destroy it)
    $("#signUpForm").destroy();

    // Using dxButton("dispose")
    // Dispose the Login Button widget
    $("#LoginButton").dxButton("dispose");

    // Check if the widget is destroyed by trying to get the instance again
    var destroyedButtonInstance = $("#LoginButton").dxButton("instance");
    console.log("Button instance after destroy:", destroyedButtonInstance);

    // Dispose Reset Button widget
    $("#ResetButton").dxButton("dispose");

    // Removing Reset Button from DOM after disposal (if necessary)
    $("#ResetButton").remove();
});
$(function () {
    let name = $("#name").dxTextBox({
        accessKey: "n",
        hint: "Full Name",
        placeholder: "Enter Full Name",
        showClearButton: true,
        stylingMode: "outlined",
        spellcheck: false,
        inputAttr: { 'aria-label': "Full Name" },
        maxLength: 50,
        width: 600,
        validationRules: [{ type: "required", message: "Full Name is required!" }],
    }).dxTextBox("instance");

    let fatherName = $("#fatherName").dxTextBox({
        accessKey: "f",
        hint: "Father's Name",
        placeholder: "Enter Father's Name",
        showClearButton: true,
        stylingMode: "outlined",
        spellcheck: false,
        inputAttr: { 'aria-label': "Father's Name" },
        maxLength: 50,
        width: 300,
        validationRules: [{ type: "required", message: "Father's Name is required!" }],
    }).dxTextBox("instance");

    let motherName = $("#motherName").dxTextBox({
        accessKey: "m",
        hint: "Mother's Name",
        placeholder: "Enter Mother's Name",
        showClearButton: true,
        stylingMode: "outlined",
        spellcheck: false,
        inputAttr: { 'aria-label': "Mother's Name" },
        maxLength: 50,
        width: 300,
        validationRules: [{ type: "required", message: "Mother's Name is required!" }],
    }).dxTextBox("instance");

    let email = $("#email").dxTextBox({
        accessKey: "e",
        hint: "Email",
        placeholder: "example@gmail.com",
        showClearButton: true,
        stylingMode: "outlined",
        spellcheck: false,
        maxLength: 50,
        width: 300,
        validationRules: [
            { type: "required", message: "Email is required!" },
            { type: "pattern", pattern: "^[^\s@]+@[^\s@]+\.[^\s@]+$", message: "Enter a valid email address!" }
        ],
        valueChangeEvent: "blur",
        onValueChanged: function (e) {
            let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailPattern.test(e.value)) {
                DevExpress.ui.notify("Invalid Email Format", "error", 2000);
            }
        }
    }).dxTextBox("instance");

    let dateOfBirth = $("#dateOfBirth").dxDateBox({
        accessKey: "d",
        type: "date",
        placeholder: "Select your birth date",
        displayFormat: "dd/MM/yyyy",
        pickerType: "calendar",
        showClearButton: true,
        min: new Date(1900, 0, 1),
        max: new Date(),
        dateOutOfRangeMessage: "Future dates are not allowed",
        width: "500px",
        onValueChanged: function (e) {
            console.log("Date selected: " + e.value);
        }
    }).dxDateBox("instance");

    let genderData = ['Male', 'Female', 'Other'];

    // RadioGroup for Gender
    let gender = $('#gender').dxRadioGroup({
        accessKey: "g",
        value: null,
        items: genderData,
        layout: 'horizontal',

        onValueChanged: function (e) {
            console.log("Selected Gender:", e.value);
        }
    }).dxRadioGroup("instance");

    let maritalStatusData = ["Single", "Married", "Divorced"]

    let maritalStatus = $("#maritalStatus").dxSelectBox({
        accessKey: "M",
        items: maritalStatusData,
        placeholder: "Select Marital Status",
        showClearButton: true,
        searchEnabled: true,
        width: 300,
        labelMode: "floating",
        onValueChanged: function (e) {
            console.log("Marital Status Selected: " + e.value);
        }
    }).dxSelectBox("instance");

    let address = $("#address").dxTextArea({
        accessKey: 'A', // Alt + Shift + A
        placeholder: "Enter Present Address",
        activeStateEnabled: false,
        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 120,
        focusStateEnabled: true,
        hint: "Enter student's present address.",
        hoverStateEnabled: true,
        inputAttr: {
            maxLength: 100
        },
        name: "address",
        spellcheck: true,
        stylingMode: "Outlined",
        tabIndex: 2,
        width: 500,
        maxLength: 100,
        onValueChanged: function (e) {
            let remaining = 100 - e.value.length;
            $("#addressCharCount").text(`Remaining Characters: ${remaining}`);
        }
    }).dxTextArea("instance");

    let contactNo = $("#contactNo").dxTextBox(  {
        accessKey: "c",
        placeholder: "Enter Contact Number", // Visible placeholder
        stylingMode: "outlined",
        mode: "tel", // Opens numeric keypad on mobile
        showClearButton: true,
        maxLength: 10, // Restricts input to 10 characters
        inputAttr: { pattern: "[0-9]*" }, // Allows only numbers
        width: "300px",
        onInput: function (e) {
            let value = e.event.target.value.replace(/\D/g, ""); // Remove non-numeric characters
            if (value.length > 10) {
                value = value.substring(0, 10); // Enforce 10-digit limit
            }
            e.component.option("value", value);
        }
    }).dxTextBox("instance");



    let tenthResult = $("#tenthResult").dxNumberBox({

        accessKey: "t",
        value: null,
        placeholder: "Enter 10th Percentage",
        showSpinButtons: true,
        format: "#0.##",
        min: 0,
        max: 100,
        width: 250,
        onValueChanged: function (e) {
            if (e.value === null || e.value === "") {
                tenthResult.option("value", null); // Resets to show placeholder
            }
        }
    }).dxNumberBox("instance");

    let twelthResult = $("#twelthResult").dxNumberBox({
        accessKey: "T",
        placeholder: "Enter 12th Percentage",
        //showSpinButtons: true,
        min: 0,
        max: 100,
        width: 250
    }).dxNumberBox("instance");

    // Department options
    let departments = ["Engineering", "Medical", "Arts"];

    // Courses mapped by department
    let coursesByDepartment = {
        "Engineering": ["Computer Science", "Mechanical", "Civil"],
        "Medical": ["MBBS", "Nursing", "Pharmacy"],
        "Arts": ["History", "Literature", "Fine Arts"]
    };

    // Initialize Department SelectBox
    let department = $("#department").dxSelectBox({
        accessKey: "D",
        items: departments,
        placeholder: "Select Department",
        searchEnabled: true,
        onValueChanged: function (e) {
            let selectedDepartment = e.value;
            let courses = coursesByDepartment[selectedDepartment] || [];

            // Ensure Course SelectBox is updated properly
            let courseSelectBox = $("#course").dxSelectBox("instance");
            courseSelectBox.option("items", courses);
            courseSelectBox.option("value", null); // Reset selection
            courseSelectBox.repaint(); // Force UI update
        }
    }).dxSelectBox("instance");

    // Initialize Course SelectBox
    let course = $("#course").dxSelectBox({
        accessKey: "C",
        items: [],
        placeholder: "Select Course",
        searchEnabled: true,
        disabled: true // Initially disabled
    }).dxSelectBox("instance");

    // Enable Course SelectBox only when Department is selected
    $("#department").dxSelectBox("option", "onValueChanged", function (e) {
        let selectedDepartment = e.value;
        let courses = coursesByDepartment[selectedDepartment] || [];

        let courseSelectBox = $("#course").dxSelectBox("instance");
        courseSelectBox.option("items", courses);
        courseSelectBox.option("value", null);
        courseSelectBox.option("disabled", courses.length === 0 ? true : false);
        courseSelectBox.repaint();
    });

    let profilePhoto = $('#profilePhoto').dxFileUploader({
        accessKey: "p",
        selectButtonText: 'Upload profilePhoto Photo',
        labelText: '',
        accept: 'image/*', // Accepts only image files
        uploadMode: 'useForm', // Uploads when form is submitted
        multiple: false, // Allows only a single file
        showFileList: true, // Displays selected file list
        allowedFileExtensions: ['.png', '.jpg', '.jpeg'], // Restricts file types
        maxFileSize: 2 * 1024 * 1024, // Max size: 2MB
        minFileSize: 10 * 1024, // Min size: 10KB
        invalidFileExtensionMessage: "Only PNG, JPG, and JPEG formats are allowed.",
        invalidMaxFileSizeMessage: "File size should not exceed 2MB.",
        invalidMinFileSizeMessage: "File size is too small.",
        uploadUrl: "https://example.com/uploadProfile", // Replace with actual endpoint
        uploadMethod: "POST", // Uses POST request for upload
        uploadHeaders: {
            "Authorization": "Bearer your_token",
        },
        onValueChanged: function (e) {
            let file = e.value[0];
            if (file) {
                if (file.size > 2 * 1024 * 1024) {
                    alert("File size should not exceed 2MB.");
                    $('#profilePhoto').dxFileUploader("reset");
                } else {
                    $("#filePreview").text("Selected file: " + file.name);
                }
            } else {
                $("#filePreview").text("");
            }
        },
        onFileUploaded: function () {
            alert("profilePhoto photo uploaded successfully!");
        },
        onUploadError: function (e) {
            alert("Upload failed: " + e.error.message);
        },
    }).dxFileUploader("instance");

    let validate = $('#validateMe').dxCheckBox({
        
        text: 'I agree to the terms and conditions',
        value: false, // Default unchecked
        hint: 'You must agree before proceeding',
        accessKey: 'v', // Keyboard shortcut
        focusStateEnabled: true, // Enables focus state
        hoverStateEnabled: true, // Enables hover effect
        width: 'auto', // Adjusts to content
        onValueChanged: function (e) {
            if (!e.value) {
                alert('You must agree to continue.');
            }
        }
    }).dxCheckBox("instance");

    $("#reset").dxButton({
        text: "Reset",
        type: "normal",
        onClick: function () {
            DevExpress.ui.dialog.confirm("Are you sure you want to reset the form?", "Reset Confirmation").done(function (result) {
                if (result) {

                    name.reset();
                    fatherName.reset();
                    motherName.reset();
                    email.reset();
                    dateOfBirth.reset();
                    gender.reset();
                    maritalStatus.reset(); 
                    address.reset(); 
                    contactNo.reset(); 
                    tenthResult.reset();
                    twelthResult.reset(); 
                    department.reset(); 
                    course.option("items", []).reset();
                    profilePhoto.reset(); // check once for resetting it's value  
                    validate.reset(); 

                    sessionStorage.clear();
                    
                    

                    DevExpress.ui.notify("Form has been reset!", "info", 2000);
                }
            });
        }
    });



    $("#submit").dxButton({
        text: "Submit",
        type: "success",
        onClick: function () {
            let selectedFiles = profilePhoto.option("value"); // Get selected files

            if (selectedFiles && selectedFiles.length > 0) {
                let file = selectedFiles[0]; // Get first selected file
                let reader = new FileReader();

                reader.onload = function (e) {
                    let profilePhotoData = e.target.result; // Convert file to Base64

                    let formData = {
                        name: name.option("value"),
                        fatherName: fatherName.option("value"),
                        motherName: motherName.option("value"),
                        dateOfBirth: dateOfBirth.option("value"),

                        gender: gender.option("value"),

                        maritalStatus: maritalStatus.option("value"),
                        email: email.option("value"),
                        contactNo: contactNo.option("value"),
                        presentAddress: address.option("value"),
                        tenthResult: tenthResult.option("value"),
                        twelthResult: twelthResult.option("value"),
                        department: department.option("value"),
                        course: course.option("value"),
                        profilePhoto: profilePhotoData, // Store Base64 Image
                    };

                    // Save to session storage
                    sessionStorage.setItem("admissionForm", JSON.stringify(formData));

                    // Show success message
                    DevExpress.ui.notify("Form submitted successfully!", "success", 2000);

                    // Reset form after submission
                    setTimeout(() => {
                        $("#reset").dxButton("instance").option("onClick")();
                    }, 2000);
                };

                reader.readAsDataURL(file); // Convert file to Base64

            } else {
                DevExpress.ui.notify("Please upload a profilePhoto photo before submitting.", "warning", 2000);
            }
        }
    });


    name.registerKeyHandler("enter", function () { fatherName.focus(); });

    fatherName.registerKeyHandler("enter", function () { motherName.focus(); });

    motherName.registerKeyHandler("enter", function () { dateOfBirth.focus(); });

    dateOfBirth.registerKeyHandler("enter", function () { gender.focus(); });


    gender.registerKeyHandler("enter", function () { maritalStatus.focus(); });

    maritalStatus.registerKeyHandler("enter", function () { email.focus(); });


    email.registerKeyHandler("enter", function () { contactNo.focus(); });

    contactNo.registerKeyHandler("enter", function () { address.focus(); });

    address.registerKeyHandler("enter", function () { tenthResult.focus(); });

    tenthResult.registerKeyHandler("enter", function () { twelthResult.focus(); });

    twelthResult.registerKeyHandler("enter", function () { department.focus(); });

    department.registerKeyHandler("enter", function () { course.focus(); });


    course.registerKeyHandler("enter", function () {
        alert("Select profile photo!");
    });



    validate.registerKeyHandler("enter", function () { $("#submit").dxButton("instance").option("onClick")(); });
});

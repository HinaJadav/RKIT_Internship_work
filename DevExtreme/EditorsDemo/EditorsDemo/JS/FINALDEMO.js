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
    $('#gender').dxRadioGroup({
        items: genderData,
        layout: 'horizontal'
    });

    let maritalStatusData = ["Single", "Married", "Divorced"]

    let maritalStatus = $("#maritalStatus").dxSelectBox({
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

    name.registerKeyHandler("enter", function () { fatherName.focus(); });
    fatherName.registerKeyHandler("enter", function () { motherName.focus(); });
    motherName.registerKeyHandler("enter", function () { email.focus(); });
    email.registerKeyHandler("enter", function () { $("#submit").dxButton("instance").option("onClick")(); });


    
    
    //-----------------
    

    // TextArea for Addresses
    $('#presentAddress').dxTextArea({ placeholder: 'Enter Present Address' });
    $('#permanentAddress').dxTextArea({ placeholder: 'Enter Permanent Address' });

    // NumberBox for Contact Number
    $('#contactNo').dxNumberBox({
        placeholder: 'Enter Contact Number',
        showSpinButtons: true
    });

    // NumberBox for Academic Results
    $('#tenthResult').dxNumberBox({ placeholder: 'Enter 10th Percentage' });
    $('#twelthResult').dxNumberBox({ placeholder: 'Enter 12th Percentage' });

    

    // SelectBox for Department & Course
    $('#department').dxSelectBox({
        items: ['Science', 'Commerce', 'Arts'],
        placeholder: 'Select Department'
    });
    $('#course').dxSelectBox({
        items: ['B.Tech', 'BBA', 'BA', 'B.Com'],
        placeholder: 'Select Course'
    });

    // FileUploader for Profile Photo
    $('#profilePhoto').dxFileUploader({
        selectButtonText: 'Upload Profile Photo',
        labelText: '',
        accept: 'image/*',
        uploadMode: 'useForm'
    });

    // CheckBox for Validation Agreement
    $('#validateMe').dxCheckBox({
        text: 'I agree to the terms and conditions',
        value: false
    });

    $("#reset").dxButton({
        text: "Reset",
        type: "normal",
        onClick: function () {
            sessionStorage.clear();
            location.reload();
        }
    });

    $("#submit").dxButton({
        text: "Submit",
        type: "success",
        onClick: function () {
            let formData = {
                name: $("#name").dxTextBox("instance").option("value"),
                fatherName: $("#fatherName").dxTextBox("instance").option("value"),
                motherName: $("#motherName").dxTextBox("instance").option("value"),
                dateOfBirth: $("#dateOfBirth").dxDateBox("instance").option("value"),
                gender: $("#gender").dxRadioGroup("instance").option("value"),
                maritalStatus: $("#maritalStatus").dxSelectBox("instance").option("value"),
                email: $("#email").dxTextBox("instance").option("value"),
                contactNo: $("#contactNo").dxNumberBox("instance").option("value"),
                presentAddress: $("#presentAddress").dxTextArea("instance").option("value"),
                permanentAddress: $("#permanentAddress").dxTextArea("instance").option("value"),
                tenthResult: $("#tenthResult").dxNumberBox("instance").option("value"),
                twelthResult: $("#twelthResult").dxNumberBox("instance").option("value"),
                department: $("#department").dxSelectBox("instance").option("value"),
                course: $("#course").dxSelectBox("instance").option("value")
            };
            sessionStorage.setItem("admissionForm", JSON.stringify(formData));
            DevExpress.ui.notify("Form submitted and data saved to session storage!", "success", 2000);
        }
    });
});

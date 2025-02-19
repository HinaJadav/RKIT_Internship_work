$(function () {
    let nameInstance = $("#name").dxTextBox({
        accessKey: 'n',
        hint: "Enter your full name",
        placeholder: "Full name",
        showClearButton: true,
        inputAttr: { 'aria-label': 'FullName' },
        width: 500,
        maxLength: 20,
        
    }).dxTextBox("instance");
    // validation: on length, required, only aphabats
    // i want to apply this validation using validator
    // on instance like event or method

    let fatherNameInstance = $("#fatherName").dxTextBox({
        accessKey: 'f',
        hint: "Enter your full name",
        placeholder: "Full name",
        showClearButton: true,
        inputAttr: { 'aria-label': 'FullName' },
        width: 500,
        maxLength: 20,
    }).dxTextBox("instance");

    let motherNameInstance = $("#motherName").dxTextBox({
        accessKey: 'n',
        hint: "Enter your full name",
        placeholder: "Full name",
        showClearButton: true,
        inputAttr: { 'aria-label': 'FullName' },
        width: 500,
        maxLength: 20,
    }).dxTextBox("instance");

    let dob = $("#dateOfBirth").dxDateBox({
        type: "date",
        text: "Birth Date",
        placeholder: "Select your birth date",
        displayFormat: "dd/MM/yyyy",
        pickerType: "calendar",
        stylingMode: "underlined",
        showClearButton: true, 
        openOnFieldClick: true,
        min: new Date(1900, 0, 1),
        max: new Date(),
        hoverStateEnabled: true,
        hint: "Enter your DOB",
        dateOutOfRangeMessage: "Future dates are not allowed", // when this msg comes
        width: "500px",
    }).dxDateBox("instance");

    const genders = ['Male', 'Female', 'Other'];

    let genderInstance = $("#gender").dxRadioGroup({
        items: genders,
        value: genders[0], // Default value
        layout: 'horizontal',
        itemTemplate(itemData, _, itemElement) {
            itemElement.text(itemData).addClass('gender-radio-item');
        },
        focusStateEnabled: true, // Enable focus state
        showClearButton: true,
    }).dxRadioGroup("instance");

    const marriageStatuses = ['Single', 'Married', 'Divorced'];

    let maritalStatusInstance = $("maritalStatus").dxRadioGroup({
        items: marriageStatuses,
        value: marriageStatuses[0], // Default value
        layout: 'vertical',
        itemTemplate(itemData, _, itemElement) {
            itemElement.text(itemData).addClass('marriage-status-item');
        },
        focusStateEnabled: true, // Enable focus state
        showClearButton: true,
    }).dxRadioGroup("instance");

    let presentAddressInstance = $("presentAddress").dxTextArea({
        accessKey: '',
        placeholder: "Present Address",
        autoSizeEnabled: true,
        minHeight: 40, // default parameter = px
        maxHeight: 100,
        focusStateEnabled: true,
        hint: "Enter your present address.",
        inputAttr: {
            maxLength: 100,
        },
        spellcheck: true,
        hoverStateEnabled: true,
    }).dxTextArea("instance");

    let permanentAddressInstance = $("permanentAddress").dxTextArea({
        accessKey: '',
        placeholder: "Permanent Address",
        autoSizeEnabled: true,
        minHeight: 40, // default parameter = px
        maxHeight: 100,
        focusStateEnabled: true,
        hint: "Enter your permanent address.",
        inputAttr: {
            maxLength: 100,
        },
        spellcheck: true,
        hoverStateEnabled: true,
    }).dxTextArea("instance");

    let contactNoInstance = $("contactNo").dxNumberBox({

    }).dxNumberBox("instance");

    let tenthResultInstance = $("tenthResult").dxNumberBox({
        type: "number",
        min: 0.35,  // 35%
        max: 1.00,  // 100%

        placeholder: "Enter 10th result",
        hoverStateEnabled: true,
        mode: 'number',
        placeholder: "10th result in percentage!",
        stylingMode: "filled",
        tabIndex: 1,
        validationStatus: 'pending',
        validationMessageMode: 'auto',
        value: null,
        width: "500px",
    }).dxNumberBox("instance");

    let twelthResultInstance = $("twelthResult").dxNumberBox({
        // do as above tenthInstance
    }).dxNumberBox("instance");

    let emailInstance = $("email").dxTextBox({
        // use basic textBox instance
        // mainly do eamil validation using : pattern rule into validator etc other rules like required also if requierd use custom validation also 
    }).dxTextBox("instance");

    let departmentInstance = $("department").dxSelectBox({

    }).dxSelectBox("instance");

    let courseInstance = $("#course").dxDropDownBox({

    }).dxDropDownBox("instance");

    $("#profilePhoto").dxFileUploader({

    });

    $("#validateMe").dxCheckBox({

    });

    $("#reset").dxButton({

    });

    $("#submit").dxButton({

    });
});
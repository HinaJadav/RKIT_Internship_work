$(function () {
    $("#birthDate").dxDateBox({
        type: "date",
        text: "Birth Date",
        placeholder: "Select your birth date",
        displayFormat: "dd/MM/yyyy",
        dateSerializationFormat: "yyyy-MM-dd", // use?
        pickerType: "calendar",
        stylingMode: "underlined",
        // spellcheck: true, --> not useful 
        tabIndex: 1,
        showClearButton: true, // ui ?
        rtlEnabled: false,
        readonly: false,
        todayButtonText: "Today", // ?
        useMaskBehavior: true, // ?
        openOnFieldClick: true,
        open: false, // ?
        cancelButtonText: 'Cancel', // no effect
        onValueChanged: function (e) {
            alert("Date selected: " + e.value);
        },
        onOpen: function () {
            console.log("DateBox opened");
        },
        onKeyUp: function () {
            console.log("Key up event");
        },
        onKeyDown: function () {
            console.log("Key down event");
        },
        //onKeyPress: function () {
           // console.log("Key press event");
        //},// ?
        onInput: function () {
            console.log("Input event - move to next field");
        },
        onInitialized: function () {
            alert("Welcome! Please enter your birth date.");
        },
        onFocusOut: function () {
            console.log("Focus out event");
        },
        onFocusIn: function () {
            console.log("Focus in event");
        },
        onCut: function () {
            alert("Cut input");
        },
        onCopy: function () {
            alert("Copy input");
        },
        onContentReady: function () {
            alert("Now user can interact with page");
        },
        min: new Date(1900, 0, 1),
        max: new Date(),
        isValidDateMessage: "Invalid date",
        hoverStateEnabled: true,
        hint: "Enter your DOB",
        
        disabledDates: function (data) {
            return data.value > new Date();
        },
        dateOutOfRangeMessage: "Future dates are not allowed" // when this msg comes
    });

    $("#graduationDate").dxDateBox({
        type: "date",
        text: "Graduation Date",
        placeholder: "Graduation Date",
        pickerType: "rollers",
        displayFormat: "MM/yyyy",
        showAnalogClock: true, // ?
        showDropDown: true,
        interval: 30 // ?
        
    });

    $("#exam10thDate").dxDateBox({
        type: "date",
        pickerType: "calendar",
        placeholder: "10th Exam Date",
    });

    $("#exam12thDate").dxDateBox({
        type: "date",
        pickerType: "list",
        placeholder: "12th Exam Date",
        onEnterKey: function () {
            alert("12th exam date entered");
        } // why not working
    });

    $("#collegeEnrollmentDate").dxDateBox({
        type: "date",
        pickerType: "native",
        placeholder: ""
    });
});

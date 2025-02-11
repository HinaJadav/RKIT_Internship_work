$(function () {
    $("#birthDate").dxDateBox({
        type: "date",
        text: "Birth Date",
        placeholder: "Select your birth date",
        displayFormat: "dd/MM/yyyy",
        // dateSerializationFormat: "yyyy-MM-dd",
            // Used when the date value needs to be serialized for sending to a server or processing. If not needed for data binding or API calls,
        pickerType: "calendar",
        stylingMode: "underlined",
       
        tabIndex: 1,
        showClearButton: true, 
        rtlEnabled: false,
        readonly: false,
       
        openOnFieldClick: true,
       
       
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
        showDropDown: true
        
        
    });

    $("#exam10thDate").dxDateBox({
        type: "date",
        pickerType: "calendar",
        placeholder: "10th Exam Date",
        displayFormat: 'EEEE, MMM dd',
        useMaskBehavior: true
        
    });


    var dateBoxInstance = $("#exam12thDate").dxDateBox({
        type: "date",
        pickerType: "list",
        placeholder: "12th Exam Date",

        
    }).dxDateBox("instance"); // Get the instance using instance()

    // Begin update (stops UI updates to optimize performance)
    dateBoxInstance.beginUpdate();

    // Set an option dynamically
    dateBoxInstance.option("displayFormat", "EEEE, MMMM d, yyyy hh: mm a");

    // Open the date picker manually
    dateBoxInstance.open();

    // Focus on the input field
    dateBoxInstance.focus();

    // Register a custom key handler (e.g., when user presses "Enter")
    dateBoxInstance.registerKeyHandler("enter", function () {
        alert("Enter key pressed!");
    });

    

    

    // Reset the date selection
    dateBoxInstance.reset();

    // Dispose of the instance (removes from DOM)
    // dateBoxInstance.dispose(); // Uncomment if you want to remove it

    // Get instance using getInstance() (pure JavaScript)
    var instanceViaGet = DevExpress.ui.dxDateBox.getInstance($("#exam12thDate")[0]);
    console.log("Instance using getInstance():", instanceViaGet);

    // End update (resume UI updates)
    dateBoxInstance.endUpdate();
});

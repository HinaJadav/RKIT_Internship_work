$(function () {
    $("#birthDate").dxDateBox({
        type: "date", // or time or datetime
        text: "Birth Date",
        placeholder: "Select your birth date",
        displayFormat: "dd/MM/yyyy", // or shortdate or yearwise etc
        // dateSerializationFormat: "yyyy-MM-dd",
            // Used when the date value needs to be serialized for sending to a server or processing. If not needed for data binding or API calls,
        pickerType: "calendar",
        stylingMode: "underlined",
       
        tabIndex: 1,
        showClearButton: true, 
        rtlEnabled: false, // Switches the UI component to a right-to-left representation.
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
        dateOutOfRangeMessage: "Future dates are not allowed", // when this msg comes
        width: "500px",
        

    });

    function validateDateBox(value) {
        let dateBox = $("#graduationDate").dxDateBox("instance");
        if (!value || value > new Date()) {
            dateBox.option({
                validationStatus: "invalid",
                validationErrors: [{ message: "Invalid or future dates are not allowed!" }]
            });
        } else {
            dateBox.option({
                validationStatus: "valid",
                validationErrors: null
            });
        }
    }

    $("#graduationDate").dxDateBox({
        type: "date",
        text: "Graduation Date",
        placeholder: "Graduation Date",
        pickerType: "rollers",
        displayFormat: "MM/yyyy",
        showDropDown: true,
        width: "500px",
        validationStatus: "pending",
        onValueChanged: function (e) {
            alert("Date selected: " + e.value);
            validateDateBox(e.value);
        },
    });

    $("#exam10thDate").dxDateBox({
        type: "date",
        pickerType: "calendar",
        placeholder: "10th Exam Date",
        displayFormat: 'EEEE, MMM dd',
        useMaskBehavior: true,
         
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





    //---------------------------------------------
    // Que: Select Date from finaltial year wise

    let currentYear = new Date().getFullYear();
    let financialYearStart = new Date(currentYear, 3, 1); // 1st april of current year
    let financialYearEnd = new Date(currentYear + 1, 2, 31); // 31st march of next year

    function validateFinancialYear(value) {
        let dateBox = financialYearInstance;
        if (!value || value < financialYearStart || value > financialYearEnd) {
            dateBox.option({
                validationStatus: "invalid",
                validationErrors: [{ message: "Date must be within the financial year range!" }]
            });
        } else {
            dateBox.option({
                validationStatus: "valid",
                validationErrors: null
            });
        }
    }

    // Que: Select date input and send it through API
    function sendDateToAPI(dateValue) {

        // date formatting 
        let day = dateValue.toString().padStart(2, '0');
        let month = (dateValue.getMonth() + 1).toString().padStart(2, '0'); // ex. if month value = 3 it gives "03"
        let year = dateValue.getFullYear();

        let selectedFormattedDate = `${year}-${month}-${day}`; // format : YYY-MM-DD
        console.log(selectedFormattedDate);

        fetch("https://67c68a2f351c081993fdadd8.mockapi.io/Bug", { // bug api into mockApi 
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ selectedDate: selectedFormattedDate })
        })
        .then(response => response.json())
        .then(data => {
            console.log("Date sent successfully:", data);
            alert("Date successfully sent to API!");
        })
        .catch(error => {
            console.error("Error sending date:", error);
            alert("Failed to send date to API!");
        });

    }

    let financialYearInstance = $("#financialYear").dxDateBox({
        type: "date",
        text: "Financial Year",
        placeholder: "Select financial year start",
        displayFormat: "dd/MM/yyyy",
        pickerType: "calendar",
        stylingMode: "underlined",
        tabIndex: 2,
        showClearButton: true,
        rtlEnabled: false,
        readonly: false,
        openOnFieldClick: true,
        value: financialYearStart,
        min: financialYearStart,
        max: financialYearEnd,
        isValidDateMessage: "Invalid date",
        hoverStateEnabled: true,
        hint: "Select start date of the financial year",
        dateOutOfRangeMessage: "Date must be within the financial year range",
        validationStatus: "pending",
        onValueChanged: function (e) {
            validateFinancialYear(e.value);
            sendDateToAPI(e.value);
        }
    }).dxDateBox("instance");
});

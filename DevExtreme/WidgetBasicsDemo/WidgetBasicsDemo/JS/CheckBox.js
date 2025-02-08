$(function () {
    // Initialize the first CheckBox with various configurations
    let checkBox1 = $("#robotCheckBox").dxCheckBox({
        text: "Understanding Business Needs",
        value: true,
        accessKey: 'h',
        activeStateEnabled: true, // ?
        disabled: false,
        focusStateEnabled: true, // ?
        hoverStateEnabled: true,
        isValid: true, // if we do "false" here then we can see RED color boundries which show that checkbox is not validate 
        name: "acceptTerms",
        readOnly: false,
        rtlEnabled: false, // about location
        tabIndex: 3,
        validationMessagePosition: 'right',
        validationStatus: 'valid',
        width: "300px", // ?
        onInitialized: function () {
            console.log("CheckBox1 Initialized");
        },
        onValueChanged: function (e) {
            console.log("CheckBox1 Value Changed: ", e.value);
        }
    }).dxCheckBox("instance");

    // Initialize the second CheckBox with a different set of features
    let checkBox2 = $("#activitiesContainer").dxCheckBox({
        text: "Identifying User Requirements",
        value: false,
        //hint: "Receive updates via email",
        onValueChanged: function (e) {
            console.log("CheckBox2 Toggled: ", e.value);
        }
    }).dxCheckBox("instance");

    // Initialize the third CheckBox with validation and styling
    let checkBox3 = $("#studyHabitContainer").dxCheckBox({
        text: "Assessing Technical Feasibility",
        value: false,
        validationStatus: "pending",
        validationMessagePosition: "bottom",
        onValueChanged: function (e) {
            console.log("CheckBox3 Value: ", e.value);
        }
    }).dxCheckBox("instance");

    let checkBox4 = $("#extraCheckBox").dxCheckBox({
        text: "Ensuring Stakeholder Buy-In",
        value: false,
        elementAttr: {
            id: "checkbox1-container",
            class: "checkbox1-class",
            "aria-label": "Robot Verification"
        },
        validationError: "Your check box given first error1",
        onContentReady: function () {
            console.log("call onContentReady()");
        },
        onDisposing: function () {
            console.log("call onDisposing()");
        },
        onOptionChanged: function () {
            console.log("call onOptionChanged()");
        }
    }).dxCheckBox("instance");
    // Button to demonstrate a warning notification
    $("#submit").dxButton({
        text: "Submit Servey",
        onClick: function () {
            if (!checkBox2.option('value') || !checkBox3.option('value')|| !checkBox4.option('value')) {
                DevExpress.ui.notify("Please accept the Terms", "warning", 500);
            } else {
                DevExpress.ui.notify("Submission successful!", "success", 200);
            }
        }
    });

    checkBox1.beginUpdate();

    setTimeout(function () {
        checkBox1.element().blur();
        console.log("blur() method triggered for CheckBox1");
    }, 1000);

    $("#reset").dxButton({
        onClick: function () {
            checkBox1.reset();
            checkBox2.reset();
            checkBox3.reset();
            checkBox4.reset();

            resetOption("value");
            console.log("Reset checkBox1 value");

        }

    });

    // defaultOptions(rule) ? check this hjow to use with checkbocx and why we are using this

    // Dispose of CheckBox1 (Frees Resources)
    setTimeout(function () {
        checkBox1.dispose();
        console.log("CheckBox1 disposed.");
    }, 5000);
    // this dispose all the allocated resources to that checkBox instance (After calling this method it will return the DOM element associated with that UI component) // use this method if UI component only build using pure JS and jQuery

    let checkBoxInstance = DevExpress.ui.dxCheckBox.getInstance($("#robotCheckBox"));
    console.log("Retrieved instance using getInstance():", checkBoxInstance);

    checkBox1.focus();

    
    // Event Handling (on & off)
    function checkBoxEventHandler(e) {
        console.log("Event Triggered: ", e.value);
    }

    // on(eventName, eventHandler)
    checkBox1.on("valueChanged", checkBoxEventHandler);

    setTimeout(function () {
        // off(eventName, eventHandler)
        checkBox1.off("valueChanged", checkBoxEventHandler);
        console.log("Detached valueChanged event from CheckBox1");
    }, 3000);

    // raised when the UI component is rendered 
   
    checkBox1.endUpdate();
});



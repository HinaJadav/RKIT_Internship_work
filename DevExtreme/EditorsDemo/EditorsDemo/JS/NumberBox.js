

$(function () {

    

    // Initialize 10th Result NumberBox
    var tenthResult = $("#10Result").dxNumberBox({
        accessKey: "1",
        focusStateEnabled: true,
        type: "number",
        min: 0.35,  // 35%
        max: 1.00,  // 100%
        
        placeholder: "Enter 10th result",
        hoverStateEnabled: true,
        mode: 'number',
        
        buttons: ["clear", "spins"],

        placeholder: "10th result in percentage!",
        stylingMode: "filled",
        tabIndex: 1,
        validationStatus: 'pending',
        validationMessageMode: 'auto',
        value: null,
        width: "500px",

        // Events
        onValueChanged: function (e) {  
            if (e.previousValue === e.value) return;

            validateNumberBox(e);

            setTimeout(function () {
                //e.component.option("readOnly", true);
                $("#12Result").dxNumberBox("instance").focus();
            }, 100);
            
        },

        

         
    }).dxNumberBox("instance");

    // Register key handler to move to next field on Enter key
    tenthResult.registerKeyHandler("enter", function () {
        $("#12Result").dxNumberBox("instance").focus();
    });

    // Initialize 12th Result NumberBox
    var twelfthResult = $("#12Result").dxNumberBox({
        accessKey: "2",
        format: "#0'%' ", // This ensures users see "35%" instead of 3500%
        min: 35,
        max: 100,
        
        placeholder: "Enter 12th result",
        buttons: ["clear", "spins"],
        placeholder: "12th result in percentage!",
        stylingMode: "filled",
        tabIndex: 2,
        validationStatus: "pending",
        width: "500px",

        // Events
        onValueChanged: function (e) {
            validateNumberBox(e);
            if (e.component.Result("validationStatus") === "valid") {
                alert("Your 12th result changed successfully!");
                e.component.option("readOnly", true);
                $("#CollegeResult").dxNumberBox("instance").focus();
            }
        },

        onCopy: function () {
            alert("12th result copied!");
        },

        onCut: function () {
            alert("12th result cut!");
        },

        
    }).dxNumberBox("instance");

    // Register key handler to move to next field on Enter key
    twelfthResult.registerKeyHandler("enter", function () {
        $("#CollegeResult").dxNumberBox("instance").focus();
    });

    // Initialize College Result NumberBox
    var collegeResult = $("#CollegeResult").dxNumberBox({
        accessKey: "3",
        format: "#0.##", // Allows decimal input (e.g., 6.5, 7.8, 9.2)
        hint: "Enter your CPI (e.g., 6.5, 8.0, 9.1).",
        min: 0,
        max: 10,
        mode: "number",
        buttons: ["clear", "spins"],
        placeholder: "College result in percentage!",
        // placeholder: "College result in percentage!",
        stylingMode: "filled",
        tabIndex: 3,
        validationStatus: "pending",
        width: "500px",

        // Events
        onValueChanged: function (e) {
            alert("Your college result changed successfully!");
            //e.component.option("readOnly", true); // Make read-only after input
        },

        onCopy: function () {
            alert("College result copied!");
        },

        onCut: function () {
            alert("College result cut!");
        },

        
         
    }).dxNumberBox("instance");

    // Submit Button
    $("#submitBtn").dxButton({
        text: "Submit",
        type: "success",
        width: "100px",
        onClick: function () {
            var tenthValue = tenthResult.option("value");
            var twelfthValue = twelfthResult.option("value");
            var collegeValue = collegeResult.option("value");

            if (tenthValue !== null && twelfthValue !== null && collegeValue !== null) {
                DevExpress.ui.notify({
                    message: "Submission Successful!",
                    type: "success",
                    displayTime: 2000,
                });
            } else {
                DevExpress.ui.notify({
                    message: "Please enter all results before submitting!",
                    type: "warning",
                    displayTime: 2000,
                });
            }
        },

         
    });

    // Reset Button - To enable editing again
    $("#resetBtn").dxButton({
        text: "Reset",
        type: "danger",
        width: "100px",
        onClick: function () {
            // Reset values
            tenthResult.option("value", null);
            twelfthResult.option("value", null);
            collegeResult.option("value", null);

            // Remove read-only
            tenthResult.option("readOnly", false);
            twelfthResult.option("readOnly", false);
            collegeResult.option("readOnly", false);

            // Reset validation status
            tenthResult.option("validationStatus", "pending");
            twelfthResult.option("validationStatus", "pending");
            collegeResult.option("validationStatus", "pending");


            DevExpress.ui.notify({
                message: "Form Reset Successfully!",
                type: "info",
                displayTime: 2000,
            });
        }
    });

    // Reusable validation function
    function validateNumberBox(e) {
        if (e.name === "value" && e.value !== null) {
            let min = e.component.option("min");
            let max = e.component.option("max");

            let isValid = e.value >= min && e.value <= max;
            e.component.option("validationStatus", isValid ? "valid" : "invalid");

            if (!isValid) {
                DevExpress.ui.notify({
                    message: `Value should be between ${min} and ${max}`,
                    type: "error",
                    displayTime: 2000,
                });
            }
        }
    }
});

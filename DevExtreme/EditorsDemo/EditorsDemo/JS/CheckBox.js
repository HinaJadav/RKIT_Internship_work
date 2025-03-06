$(function () {
    // Initialize the CheckBox UI component
    let checkBox = $("#checkBoxContainer").dxCheckBox({

        // Accessibility: Set a keyboard shortcut key to focus on the UI component.
        accessKey: 'h',

        // Enables the active state when the user interacts with the checkbox (e.g., clicking).
        activeStateEnabled: true,

        // Determines whether the checkbox is disabled or not.
        disabled: false,

        // Used to set global attributes like `id`, `class`, `data-*` attributes, etc.
        elementAttr: {
            id: "checkbox1-container",
            class: "checkbox1-class"
        },

        // Allows the checkbox to be focused using keyboard navigation (e.g., Tab key). 
        focusStateEnabled: true,

        // Sets the hint text that appears when hovering over the checkbox.
        hint: "Verify yourself!",

        // Enables the hover state (visual effect when hovering over the checkbox).
        hoverStateEnabled: true,

        // Specifies whether the checkbox value is valid (can be used with validation rules).
        isValid: true,

        // Assigns a name attribute to the checkbox, useful when submitting forms.
        name: "acceptTerms",

        // Event handlers
        onContentReady: function (e) {
            console.log("call onContentReady()");
        },

        onDisposing: function (e) {
            console.log("call onDisposing()");
        },

        onInitialized: function (e) {
            console.log("call onInitialized");
        },

        onOptionChanged: function (e) {
            console.log("call onOptionChanged()");
        },

        onValueChanged: function (e) {
            console.log("call onValueChanged()");
            validateCheckBox(e.value);
        },

        // Specifies whether the editor is read-only.
        readOnly: false, // Changed to allow clicking

        // Switches UI component to right-to-left representation
        rtlEnabled: true,

        // Tab index for keyboard navigation
        tabIndex: 3,

        // Specifies text displayed by the checkbox
        text: "You are validated!",

        // Specifies the UI component state
        value: true,

        // Specifies whether the UI component is visible
        visible: true,

        // UI component width
        width: "55px"

    }).dxCheckBox("instance");

    // Toggle value on click
    $("#checkBoxContainer").on("click", function () {
        let currentValue = checkBox.option("value");
        checkBox.option("value", !currentValue);
    });

    // Function to validate the checkbox
    function validateCheckBox(value) {
        if (!value) {
            checkBox.option({
                validationStatus: "invalid",
                validationErrors: [{ message: "You must accept the terms!" }]
            });
        } else {
            checkBox.option({
                validationStatus: "valid",
                validationErrors: null
            });
        }
    }
});

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
            class: "checkbox1-class",
            // You can add other attributes like:
            // "data-custom": "customValue",
            // "aria-label": "Custom Checkbox"
        },

        // Allows the checkbox to be focused using keyboard navigation (e.g., Tab key). : 
        focusStateEnabled: true,
        // **How to see the effect?**  
        // - Navigate using the Tab key and check if the checkbox gets focused.

        // Sets the hint text that appears when hovering over the checkbox.
        hint: "Verify yourself!",

        // Enables the hover state (visual effect when hovering over the checkbox).
        hoverStateEnabled: true,
        // **How to see the effect?**  
        // - Hover over the checkbox and observe if there is a slight color change or animation.

        // Specifies whether the checkbox value is valid (can be used with validation rules).
        isValid: true,

        // Assigns a name attribute to the checkbox, useful when submitting forms.
        name: "acceptTerms",
        // **Where to see its value?**
        // - If inside a form, it will appear in the form submission data.
        // - Use browser dev tools (Inspect Element) to check the input element.

        // function that is execute when the UI component is ready and each time content is changed.

        onContentReady: function (e) {
            Console.log("call onContentReady()");
        },

        // function that is execute before the UI component is disposed of
        onDisposing: function (e) {
            Console.log("call onDisposing()");
        },

        // it will execute when checkbox is initialized
        onInitialized: function (e) {
            Console.log("call onInitialized");
        },

        // it will call when the UI component property is changed.
        onOptionChanged: function (e) {
            Console.log("call onOptionChanged()");
        },

        // it will execute after UI component's value is changed
        onValueChanged: function (e) {
            Console.log("call onValueChanged()");
        },

        // specifies that whether the editor is read-only.
        readOnly: true,

        // switches UI component to right-to-left representation
        rtlEnabled: true,


        // specify that on which number times of tan which component will be highlight --> when there are multipl
        tabIndex: 3,

        // specifies text display by checkbox
        text: "You are validate!",

        // Information on the broken validation rule
        // it will contains first item from the validationErrors array 
        validationError: "Your check box given first error1",

        // Array of messages when validation rules failed
        // validationErrors: --> implement this

        // It will show message about validation rules that are not satisfied 
        // auto : editor with message is displayed when the editor is in focus
        validationMessageMode: auto, // try always

        // decide about validation message position 
        validationMessagePosition: 'right',

        // Indication of the current validation status
        validationStatus: 'pending' // try "valid" & "invalid"

        // specifies the UI component state
        value: true,

        // specifies whether UI component is visible 
        visible: true,

        // for UI component width
        width: "55px",



    }).dxCheckBox("instance");
});

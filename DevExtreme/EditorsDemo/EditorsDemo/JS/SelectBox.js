$(function () {
  
    $("#informalPersonSelectBox").dxSelectBox({
        dataSource: ["Student", "Employee", "Freelancer", "Entrepreneur", "Retired", "Intern", "Employee of admin office head office representative"],
        valueExpr: (item) => item,
        displayExpr: (item) => item,
        value: "Student",
        // disabled: true, // Allows selection (set to `true` if it should remain disabled)
        labelMode: "floating", // Label will float when user interacts


        searchEnabled: true, // Enables search functionality
        searchMode: 'startswith',
        searchTimeout: 1000, // in ms


        //useItemTextAsTitle: true,

        showClearButton: true, // Allows clearing selection
        //placeholder: "Select your role", // User-friendly placeholder text
        width:200,
        onValueChanged: function (e) {
            console.log("Selected Role:", e.value);
        }
    });

    $("#studentSelectBox").dxSelectBox({
        dataSource: [
            { id: 1, name: "John Doe", standard: "Grade 10" },
            { id: 2, name: "Alice Smith", standard: "Grade 9" },
            { id: 3, name: "Bob Johnson", standard: "Grade 10" },
            { id: 4, name: "Charlie Brown", standard: "Grade 8" },
            { id: 5, name: "David White", standard: "Grade 9" },
            { id: 6, name: "Emma Wilson", standard: "Grade 10" },
            { id: 7, name: "Franklin Adams", standard: "Grade 8" },
            { id: 8, name: "Grace Scott", standard: "Grade 9" },
            { id: 9, name: "Henry Moore", standard: "Grade 10" },
            { id: 10, name: "Isabella Taylor", standard: "Grade 8" }
        ],
        valueExpr: "name", // Stores selected value as student name
        displayExpr: function (item) {
            return item ? `${item.name} (${item.standard})` : "";
        }, // Shows "Student Name (Grade)"

        searchEnabled: true, // Enables search functionality
        searchPlaceholder: "Search student by name...",

        placeholder: "Select a student",
        showClearButton: true, // Allows clearing selection
        width: 500,
        onValueChanged(e) {
            console.log("Selected Student:", e.value);
        }
    });

    $("#academicSkillsSelectBox").dxSelectBox({
        dataSource: ["Critical Thinking", "Time Management", "Research Skills", "Effective Communication", "Problem-Solving Ability"],
        deferRendering: true,
        disabled: false,
        //placeholder: "Select your academic skills",


        dropDownButtonTemplate(data, container) {
            $("<div>")
                .addClass("custom-button")
                .text("🔽")
                .appendTo(container);
        },

        dropDownOptions: {
            height: 200,
            width: 300
        },
        rtlEnabled: false,
        width: 500,
        fieldTemplate(data, container) {
            let $textBox = $("<div>").appendTo(container); // Append container first
            $textBox.dxTextBox({ // Initialize dxTextBox properly
                value: data ? data : "",
                readOnly: true,
                placeholder: "Select your academic skills"
            });
        },



        focusStateEnabled: true,
        hint: "Choose the skill that best represents you",

        inputAttr: {
            placeholder: "Select a skill",
            maxlength: "30",
            "aria-label": "Skill selection"
        },

        isValid: true,
        labelMode: "floating",
        openOnFieldClick: true, // Opens dropdown when clicking inside input
        opened: false, // By default, dropdown is closed

        // EVENTS
        onInitialized(e) {
            console.log("SelectBox Initialized:", e.component.option("dataSource"));
        },

        onInput(e) {
            console.log("User is typing:", e.event.target.value);
        },

        onItemClick(e) {
            console.log("User clicked on item:", e.itemData);
        },

        onSelectionChanged(e) {
            console.log("Selection Changed:", e.selectedItem);
        },

        onValueChanged(e) {
            console.log("Value Changed from:", e.previousValue, "to:", e.value);
        },

        onOpened(e) {
            console.log("Dropdown Opened");
        }
    });


    




    $("#extraordinarySkills").dxSelectBox({
        items: [
            { name: "Dance", emoji: "💃" },
            { name: "Reading", emoji: "📖", disabled: true }, // Disabled item
            { name: "Writing", emoji: "✍️" },
            { name: "Singing", emoji: "🎤" },
            { name: "Cooking", emoji: "🍳" },
            { name: "Photography", emoji: "📸" },
            { name: "Painting", emoji: "🎨", visible: false }, // Hidden item
            { name: "Gaming", emoji: "🎮" },
            { name: "Cycling", emoji: "🚴" },
            { name: "Swimming", emoji: "🏊", disabled: true } // Disabled item
        ],
        valueExpr: "name", // Stores selected value as the 'name'
        displayExpr: "name", // Displays the 'name' in the input box
        itemTemplate: function (data) {
            return $("<div>").html(`${data.emoji} ${data.name}`);
        },
        // placeholder: "Select a hobby", --> not works here becaus fieldTemplate Replaces Default Input Box
        labelMode: "floating", // Enables floating label

        // Shows emoji + name in dropdown list
        itemTemplate: function (data) {
            return $("<div>").html(`${data.emoji} ${data.name}`);
        },
        width: 500,

        // Shows emoji + name inside the input box
        fieldTemplate: function (data, container) {
            let $textBox = $("<div>").appendTo(container);
            $textBox.dxTextBox({
                value: data ? `${data.emoji} ${data.name}` : "",
                readOnly: true,
                placeholder: "Select a hobby" 
            });

        }


    });

    
    
    $("#sportsSkills").dxSelectBox({
        dataSource: new DevExpress.data.DataSource({
            store: [
                { id: 1, skill: "Football", type: "Outdoor" },
                { id: 2, skill: "Basketball", type: "Outdoor" },
                { id: 3, skill: "Table Tennis", type: "Indoor" },
                { id: 4, skill: "Swimming", type: "Outdoor" },
                { id: 5, skill: "Chess", type: "Indoor" },
                { id: 6, skill: "Cricket", type: "Outdoor" }
            ],
            key: "id",
            group: "type" // Group items by "type" (Indoor, Outdoor)
        }),
        valueExpr: "skill", // Stores only skill names
        displayExpr: "skill", // Displays skill names
        grouped: true, // Enables grouping
        searchEnabled: true, // Allows searching
        showClearButton: true, // Allows clearing selection
        placeholder: "Select a sports skill",
        labelMode: "floating",

        // Optional: Custom template to format group headers
        groupTemplate: function (data) {
            return $("<div>")
                .addClass("custom-group-header")
                .text(`${data.key}`); // Adds an emoji before group names
        },

        // Methods
        onInitialized: function (e) {
            console.log("SelectBox Initialized:", e.component);
        },
        onContentReady: function (e) {
            console.log("Content is Ready:", e.component.option("dataSource"));
        },
        width: 500,
        // Events
        onValueChanged: function (e) {
            console.log("Skill Selected:", e.value);
        },
        onSelectionChanged: function (e) {
            console.log("Selection Changed:", e);
        },
        onItemClick: function (e) {
            console.log("Item Clicked:", e.itemData.skill);
        },
        onCustomItemCreating: function (e) {
            console.log("Custom Item Added:", e.text);
            e.customItem = { skill: e.text, type: "Custom" }; 
        }
    });

    // Methods on instance
    let sportsInstance = $("#sportsSkills").dxSelectBox("instance");
    console.log("Element:", sportsInstance.element());
    console.log("Default Options:", sportsInstance.option());
    console.log("Value:", sportsInstance.option("value"));
    console.log("Data Source:", sportsInstance.getDataSource());



    $("#reset").dxButton({
        text: "Reset",
        onClick() {
            $(".dx-selectbox").not("#informalPersonSelectBox").each(function () {
                let selectBox = $(this).dxSelectBox("instance");
                if (selectBox) {
                    selectBox.reset(); // Reset each select box except the first one
                }
            });

            alert("Warning: All selected data (except Informal Person Type) will be lost!");
        }
    });


    $("#submit").dxButton({
        text: "Submit",
        onClick() {
            let selectedValues = [];

            //  Find all dxSelectBox elements and collect their values
            $(".dx-selectbox").each(function () {
                let selectBox = $(this).dxSelectBox("instance");
                let label = $(this).prev("label").text(); // Get the label above the select box
                let value = selectBox.option("value");

                if (!value) {
                    alert(`Please select a value for: ${label}`);
                    return false; // Stop submission if any value is missing
                }

                selectedValues.push(`${label}: ${value}`);
            });

            // If all values are selected, show them in an alert
            if (selectedValues.length) {
                alert("You selected:\n" + selectedValues.join("\n"));
            }
        }
    });

});






// what is difference between option , method, event and where we need to use what

// deferRendering: It determines whether the dropDown list should be rendered immediately when the component is initialized or only when the user first open it
// it controls when the dropdown list is rendered
// dropdown list items are not rendered until the user opens the dropdown

// dropDownButtonTemplate :  allows customization of the dropdown button inside the select box.

    // dropDownOptions : configures the appearance and behavior of the dropdown list.
// elementAttr  : allows to add custom attribute  like class, id etc. to the HTML element of selectBox. --> use to enhance style etc part of that component
// fieldTemplate: allows customization of the apperance of the selected value inside the input box


// inputAttr : allows to add custom attributes
// why? :
// 1) It gives many other options which are not directly avialable into dxSelectBox options
// 2) Handles multiple Attributes together
// 3) Allows extra customization for ccessibility and Data Handling (ex. aria-label(useful for screen reader), data-Info(use for jquery / Js referrence))


// items: allows for define list of values that users can select from as input of select box
// use it when list which we want to give for select box is small and fixed sized

// *for large and dynamic size list use : dataSource

// labelMode types: static, floating, hiddlen, outside



// Difference Between onValueChanged and onSelectionChanged
/*onSelectionChanged

Triggers whenever a user clicks on an item in the dropdown.
Fires even if the selected value does not change.
Useful for tracking clicks on items(e.g., user interactions, logging).

onValueChanged

Triggers only when the selected value actually changes.
Does not fire if the user selects the same value again.
Useful for validations, API calls, or performing actions when the value changes.*/




/*onValueChanged: is better when tracking actual changes.
onSelectionChanged: is useful when tracking clicks on items(even if the value stays the same).
onInput: captures typing inside the field.
onOpened: detects when the dropdown opens.*/



// valueExpr: stores the value
// defines that which property of data object is used as the actual value
// useful during api call, internal logic etc

// displayExpr: contols what the user sees
// define which property of the data object is displayed in the dropdown
// ex :  useful when the valueExpr is an ID but you want to show readable names.

// displayValue : readonly property bcz it shows currently selected text
// It automatically returns the displayed value based on displayExpr

// searchExpr : Specifies the name of a data source item field or an expression whose value is compared to the search criterion.

// useItemTextAsTitle: true   : The text of the selected item is used as the title attribute.
/*

<SelectBox 
    dataSource={["Apple", "Banana", "Cherry with a very long name"]} 
    useItemTextAsTitle={true} 
    width={150} 
/>

Effect:
If the selected value is "Cherry with a very long name", and it gets cut off,
Hovering over it shows a tooltip with "Cherry with a very long name".

*/
$(function () {
    let fullNameInstance = $("#fullName").dxTextArea({
        accessKey: 'N', // alt + Shift + f 
        placeholder: "Full name",
        // activeStateEnabled: it is allows UI component visually responds (changes its state) as a result of user interaction like touch, click, tap (mostly useful for mobile devices)
        // unnecessary for desktop apps because hover effects already exist
        activeStateEnabled: false,
        // autoResizeEnabled: it allows the text area automatically expand or shrink based on content inside it
        // it makes 
        autoResizeEnabled: true,
        minHeight: 40, // default parameter = px
        maxHeight: 100,
        focusStateEnabled: true,
        hint: "Enter student's full name.",
        hoverStateEnabled: true,
        inputAttr: {
            maxLength: 20
        },
        // used for form integration and backend data binding
        // assigns a name to the component
        // It makes easier to work with forms and server-side data processing
        name: "fullName",
        spellcheck: true,
        stylingMode: "Outlined",
        tabIndex: 1,
        width: 500,
        maxLength: 50,
        // event 
        onValueChanged: function (e) {
            let remaining = 50 - e.value.length;
            $("#charCount").text(`Remaining Characters: ${remaining}`);
        }
    }).dxTextArea("instance");

    let feedackInstance = $("#feedBack").dxTextArea({
        accessKey: 'F', // alt + Shift + f 
        placeholder: "Enter your full name",
        // autoResizeEnabled: it allows the text area automatically expand or shrink based on content inside it
        // it makes dynamically adjust text area size
        width: 500,
        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 100, 
        maxLength: 500,
        tabIndex: 2,
        inputAttr: { maxLength: 500 },
        focusStateEnabled: true,
        hoverStateEnabled: true,
        onValueChanged: function (e) {
            let wordCount = e.value.trim().split(/\s+/).filter(Boolean).length;
            $("#wordCount").text("Word Count: " + wordCount);
        }

    }).dxTextArea("instance");

    
    $("#reset").dxButton({
        text: "Reset",
        onClick: function () {
            ("#fullName").dxTextArea("instance").option("value", "");
            ("#feedBack").dxTextArea("instance").option("value", "");
            ("#charCount").text("Remaining Character : 50");
            ("#wordCount").text("Word Count: 0");
        }
    });

    $("#submit").dxButton({
        text: "Submit",
        onClick: function () {
            let fullName = $("#fullName").dxTextArea("instance").option("value").trim();
            let feedBack = $("#feedBack").dxTextArea("instance").option("value").trim();

            if (fullName.length === 0) {
                alert("Please! Enter your Full name.");
                fullNameInstance.focus();
                return;
            }
            if (feedBack.length < 20) {
                alert("Please! Enter proper feedback in detail.");
                feedackInstance.focus();
                return;
            }

            alert("Your feedback form submitted successfully.");
            fullNameInstance.reset();
            feedackInstance.reset();
        }
    })
});

// what is difference between inputAttr and elementAttr
// inputAtte:
   // 1) target : <input> inside the component
   // 2) common use cases: maxlength, readonly, autocomplete, aria-label
   // 3) ex: maxLength: 10 (it will limit input length)
   // 4) use to manage or modify input nehaviours and accessibility
// elementAttr:
    // 1) target: <outer <div> wrapper of the component
    // 2) common use cases: id, class etc.
    // 3) ex: class: "myClass" (now we ca use this call name for add CSS effect into this component)
    // 4) use for styling and metadata


// What is difference between onChange and onValueChanged
// onValueChanged: triggers when value of component is changed it gives real-time updates
    // mostly use for: validation, auto-save, enabling-disabling buttons
// onChange: triggers when input loses focus
// mostly use for: final validation, form submission

// What Happens when basic Event function runs?
// ex: I fire "onValueChanged" event on dxTextArea :
// first pass : "e" parameter which contains :
// e.value : new user input text
// e.preivousValue : old input value before new input 
// e.component : instance of user-type (for ex here is "dxTextArea")
// e.event : the event that triggered the change



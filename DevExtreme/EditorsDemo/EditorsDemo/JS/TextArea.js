$(function () {
    $("#fullName").dxTextArea({
        accessKey: 'N', // alt + Shift + f 
        placeholder: "Full name",
        // activeStateEnabled: it is allows UI component visually responds (changes its state) as a result of user interaction like touch, click, tap (mostly useful for mobile devices)
        // unnecessary for desktop apps because hover effects already exist
        activeStateEnabled: false,
        // autoResizeEnabled: it allows the text area automatically expand or shrink based on content inside it
        // it makes 
        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 200,
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
        width: 750,
        // event
        /*onChange: function(e) {

        }*/
    });
    $("#feedback").dxTextArea({
        accessKey: 'F', // alt + Shift + f 
        placeholder: "Enter your full name",
        // autoResizeEnabled: it allows the text area automatically expand or shrink based on content inside it
        // it makes dynamically adjust text area size

        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 200,

        // event
        onValueChanged: function (e) {
            let wordCount = e.value.trim().split(/\s+).filter(Boolean).length;
            // split(/\s+/) splits the string into an array of words, using one or more whitespace characters (\s+) as the delimiter.
            // filter(Boolean) removes any empty strings from the array.

            $("#wordCount").text("Word Count: " + wordCount);

        }
    });

    $("<div id='wordCount'>Word Count: 0</div>").insertAfter("#feedback");

    $("reset").dxButton({
        text: "Reset",
        onClick: function() {
            let fullName = $("#fullName").dxTextArea("instance");

            fullName.blur(); // remove focus and make it blur
            fullName.option("disabled", true); // disable the text are withput chnaging its value

            // Reset all text areas
            $("#fullName").dxTextArea("instance").option("value", "");
            $("#feedback").dxTextArea("instance").option("value", "");
        }
    })
});

// what is difference between inputAttr and elementAttr
// inputAtte:
   // 1) target : <input> inside the component
   // 2) common use cases: maxlength, readonly, autocomplete, aria-label
   // 3) ex: maxLength: 10 --> it will limit input length
   // 4) use to manage or modify input nehaviours and accessibility
// elementAttr:
    // 1) target: <outer <div> wrapper of the component
    // 2) common use cases: id, class etc.
    // 3) ex: c;ass: "myClass" --> now we ca use this call name for add CSS effect into this component
    // 4) use for styling and metadata


// What is difference between onChange and onValueChanged
// onValueChanged: triggers when value of component is changed it gives real-time updates
    // mostly use for: validation, auto-save, enabling-disabling buttons
// onChange: triggers when input loses focus
    // mostly use for: final validation, form submission



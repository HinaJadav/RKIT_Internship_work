$(function () {
    // Full Name TextArea
    $("#fullName").dxTextArea({
        accessKey: 'N',
        placeholder: "Full name",
        activeStateEnabled: false,
        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 200,
        focusStateEnabled: true,
        hint: "Enter student's full name.",
        hoverStateEnabled: true,
        inputAttr: {
            maxLength: 20
        },
        name: "fullName",
        spellcheck: true,
        stylingMode: "Outlined",
        tabIndex: 1,
        width: 750,
        onValueChanged: function (e) {
            // Live Character Counter
            let remaining = 20 - e.value.length;
            $("#charCount").text("Remaining Characters: " + remaining);

            // Auto-Save Simulation
            clearTimeout(window.saveTimeout);
            window.saveTimeout = setTimeout(() => {
                console.log("Auto-Saved Name:", e.value);
                $("#saveStatus").text("Draft saved!");
            }, 2000); // Auto-save after 2 seconds
        }
    });

    // Feedback TextArea
    $("#feedback").dxTextArea({
        accessKey: 'F',
        placeholder: "Enter your feedback",
        autoResizeEnabled: true,
        minHeight: 40,
        maxHeight: 200,
        onValueChanged: function (e) {
            // Live Word Counter
            let wordCount = e.value.trim().split(/\s+/).filter(Boolean).length;
            $("#wordCount").text("Word Count: " + wordCount);

            // Convert text to Title Case as user types
            let formattedText = e.value.replace(/\b\w/g, c => c.toUpperCase());
            $("#feedback").dxTextArea("instance").option("value", formattedText);
        }
    });

    // Append counters below the inputs
    $("<div id='charCount'>Remaining Characters: 20</div>").insertAfter("#fullName");
    $("<div id='wordCount'>Word Count: 0</div>").insertAfter("#feedback");
    $("<div id='saveStatus' style='color: green;'></div>").insertAfter("#fullName");

    // Reset Button
    $("#resetBtn").dxButton({
        text: "Reset",
        onClick: function () {
            // Disable and blur fullName input
            let fullName = $("#fullName").dxTextArea("instance");
            fullName.blur();
            fullName.option("disabled", true);

            // Reset all text areas
            $("#fullName").dxTextArea("instance").option("value", "");
            $("#feedback").dxTextArea("instance").option("value", "");

            // Reset counters
            $("#charCount").text("Remaining Characters: 20");
            $("#wordCount").text("Word Count: 0");
            $("#saveStatus").text("");

            console.log("All inputs reset!");
        }
    });
});

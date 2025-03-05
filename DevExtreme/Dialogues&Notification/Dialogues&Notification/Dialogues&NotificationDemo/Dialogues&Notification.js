$(document).ready(function () {
    // Initialize LoadIndicator for Initial Load
    var loadIndicator = $("#loadIndicator").dxLoadIndicator({
        visible: true,
        // indicatorSrc : use when we want to show some image as indicator then it will specifies path of that image
    }).dxLoadIndicator("instance");

    // Initialize LoadPanel for Reloading
    var loadPanel = $("#loadPanel").dxLoadPanel({
        message: "Reloading Data...", // text displayed in the load panel (default value: "Loading...")
        shading: true, // specifies to shade the backGround when the UI component is active
        shadingColor: "rgba(0,0,0,0.4)",
        visible: false, // Initially hidden
        showIndicator: true, // decides whether to show loading load indicator or not
        showPane: true, // use to show that UI box around message + loadindicator
        position: { of: window }, // Centered in the full page
        container: "window", // Covers the entire page // no need to specidies if input = "window" bcz ir's default behavior
        deferRendering: true, // improves performance by rendering content only when the component becomes visible, while false pre- renders it immediately to avoid delays. 
        // delay : in ms after time load panel is displayed
    }).dxLoadPanel("instance");

    // Initialize Popover
    var reportBugPopover = $("#reportBugPopover").dxPopover({
        target: "#reportBugInfo", // Set target to the button
        position: "bottom", // Display popover below the button
        width: 300,
        shading: true, // Enable background shading
        shadingColor: "rgba(0,0,0,0.4)", // Semi-transparent background
        closeOnOutsideClick: true, // Close popover when clicking outside
        hoverStateEnabled: true, // Enable hover effect
        showTitle: true, // Show title bar
        title: "How to Report a Bug", // Title for popover
        showCloseButton: true, // Enable close button
        showEvent: "dxclick", // Show popover on button click
       // hideEvent: "mouseleave", // Hide popover when mouse leaves
        titleTemplate: function (titleElement) { // Custom title template
            titleElement.append("<b>🛠 Bug Reporting Guide</b>");
        },
        contentTemplate: function (contentElement) { // Custom content template
            contentElement.append(`
            <p>1. Provide a detailed description of the issue.</p>
            <p>2. List the steps to reproduce the bug.</p>
            <p>3. Mention expected vs actual behavior.</p>
        `);
        },
        onTitleRendered: function (e) { // Event triggered when title is rendered
            console.log("Popover title rendered:", e.component.option("title"));
        }
    }).dxPopover("instance");

    // Initialize dxButton for "Report Bug Info"
    $("#reportBugInfo").dxButton({
        text: "Report Bug Steps",
        type: "default",
        onClick: function () {
            reportBugPopover.show(); // Show popover on button click
        }
    });


    /*var toast = $("#toast").dxToast({
        message: "dfhgfgh",
        type: "info", // Options: "info", "success", "warning", "error"
        displayTime: 3000,
        position: {
            my: "center top",
            at: "center top",
            of: window
        }
    }).dxToast("instance");*/
    




    // add scrolling ***************************

    // Initialize Popup
    var reportBugPopup = $("#reportBugPopup").dxPopup({
        title: "Report a Bug", // Popup Title
        width: 400,
        height: "auto",
        maxHeight: 500, // Limit height to enable scrolling
        showCloseButton: true, // Enable close button
        closeOnOutsideClick: true, // Close when clicking outside
        shading: true, // Enable background shading
        shadingColor: "rgba(0, 0, 0, 0.5)", // Semi-transparent background
        visible: false, // Initially hidden
        enableBodyScroll: true,
        fullScreen: false, //  display the Popup in full-screen mode
        hideOnParentScroll: false, // Prevent closing on parent scroll
        hideOnOutsideClick: true, // Close on outside click
        dragOutsideBoundary: false, // Prevent dragging outside viewport
        position: { my: 'center', at: 'center', of: window },
        /*
        my → Defines which part of the popup should be aligned.
        at → Defines which part of the target element should be aligned.
        of → Specifies which element or area the popup should be positioned relative to.
        */

        contentTemplate: function (contentElement) { // Define content inside popup
            contentElement.append(`
                <div id="bugForm">
                    <label><b>Bug Title:</b></label>
                    <div id="bugTitle"></div>

                    <label><b>Description:</b></label>
                    <div id="bugDescription"></div>

                    <label><b>Expected Outcome:</b></label>
                    <div id="expectedOutcome"></div>

                    <div style="margin-top: 15px; text-align: right;">
                        <button id="submitBug" class="dx-button dx-button-success">Submit</button>
                        <button id="cancelBug" class="dx-button dx-button-danger">Cancel</button>
                    </div>
                </div>
            `);

            // Initialize DevExtreme Inputs
            $("#bugTitle").dxTextBox({ placeholder: "Enter bug title..." });
            $("#bugDescription").dxTextArea({ height: 100, placeholder: "Describe the bug..." });
            $("#expectedOutcome").dxTextArea({ height: 80, placeholder: "Expected result..." });

            // Attach Event Handlers for Buttons
            $("#submitBug").on("click", function () {
                let title = $("#bugTitle").dxTextBox("instance").option("value");
                let description = $("#bugDescription").dxTextArea("instance").option("value");
                let expectedOutcome = $("#expectedOutcome").dxTextArea("instance").option("value");

                if (!title || !description || !expectedOutcome) {
                    alert("Please fill out all fields.");
                    return;
                }

                alert("Bug Reported Successfully!"); // Replace with actual API submission logic
                reportBugPopup.hide(); // Close popup after submission
            });
           /* $("#submitBug").on("click", function () {
                let title = $("#bugTitle").dxTextBox("instance").option("value");
                let description = $("#bugDescription").dxTextArea("instance").option("value");
                let expectedOutcome = $("#expectedOutcome").dxTextArea("instance").option("value");

                if (!title || !description || !expectedOutcome) {
                    toast.option({
                        message: "Please fill out all fields.",
                        type: "error",
                        displayTime: 3000
                    }).show();
                    return;
                }

                toast.option({
                    message: "Bug Reported Successfully!",
                    type: "success",
                    displayTime: 3000
                }).show();

                reportBugPopup.hide();
            });*/



            $("#cancelBug").on("click", function () {
                reportBugPopup.hide(); // Close popup on cancel
            });
        }
    }).dxPopup("instance");

    
   


    // Initialize dxButton for "Report Bug"
    $("#reportBug").dxButton({
        text: "Report Bug",
        type: "default",
        onClick: function () {
            reportBugPopup.show(); // Show popup on button click
        }
    });




    // Initialize dxButton for "Reload Data"
    $("#reloadData").dxButton({
        text: "Reload Data",
        type: "success",
        onClick: function () {
            loadData(true);  // Pass 'true' to show LoadPanel
        }
    });
   

    // Function to Load Data
    function loadData(showLoadPanel = false) {
        if (showLoadPanel) {
            loadPanel.show();  // Show LoadPanel when reloading
        } else {
            loadIndicator.option("visible", true);  // Show LoadIndicator for initial load
        }

        setTimeout(() => {  // Simulate Network Delay
            fetch("https://67c68a2f351c081993fdadd8.mockapi.io/Bug")
                .then(response => response.json())
                .then(data => {
                    let bugList = $("#bugList");
                    bugList.empty();
                    data.forEach(bug => {
                        bugList.append(`<li>${bug.title}</li>`);
                    });

                   /* $("#toast").dxToast("instance").option({
                        message: "Data Loaded Successfully!",
                        type: "success",
                        displayTime: 3000
                    }).show();*/
                })
                .catch(error => {
                    console.error("Error loading data:", error);
                   /* $("#toast").dxToast("instance").option({
                        message: "Error Loading Data!",
                        type: "error",
                        displayTime: 3000
                    }).show();*/
                })
                .finally(() => {
                    loadIndicator.option("visible", false);  // Hide LoadIndicator
                    loadPanel.hide();  // Hide LoadPanel after reload
                });
        }, 2000);  // Delay for demo effect
    }

    // Load Data on Page Load
    loadData();
});

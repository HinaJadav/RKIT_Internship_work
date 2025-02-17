$(function () {
    let aadharCardInstance = $("#aadharCard").dxFileUploader({
        accessKey: 'a',
        allowCanceling: true,
        allowdFileExtention: [".png", ".txt", ".pdf"],
        // use "elementAttr" in logical and effective sence
        focusStateEnabled: true,
        hint: "Enter your AadharCard.",
        hoverStateEnable: true,
        inputAttr: { 'aria-label': 'AadharCardUploader' },
        maxFileSize: 5000,
        invalidFileExtensionMessage: "Your file's extention in not supported",
        minFileSize: 1000,
        invalidMaxFileSizeMessage: "Your  select file size is more then default file size max-limit.",

        invalidMaxFileSizeMessage: "Your  select file size is less then default file size min-limit.",

        labelText: "Select your AadharCard from Here!",
        multiple: false,
        name: "aadharCardFileUploader",

        // event 
        onBeforeSend: function () {
            // into this event i want to shoe one confirmation pop to user with file name and it's size and after that based on user's input trigger next event or reverse this also do validation of size and extention do firstly 
        },

        onContentReady: function () {
            alert("Your Documentation prosess page ready!");
        },

        onDropZoneEnter: function () {
            // display hint message
        },

        onDropZoneLeave: function () {
            // remove that hint message
        },

        onFileUploaded: function () {
            // show success popUp
        },

        onProcess: function () {
            // see this in detail
        },

        onUploadAborted: function () {
            // load entire page again and start process frm init
        },

        onUploadded: function () {
            // move focus into next component or submit button
        },

        onUploadError: function () {
            // implement this as per basic real life problem execution
        },

        onUploadStarted: function () {
            // implement this into logical way
        },

        onValueChange: function () {
            // trigger this function when i upload new file before submit it --> is this good idea for use this event for this task --> give me suggestions for this 
        },

        readyToUploadMessage: "Now you can upload your aadharcard.", // when and where it will be useful or display or i need to implement extra ui component to see it's result?

        selectButtonText: "Select AadharCard",

        showFileList: true, // how can i see it's effect

        tabIndex: 1,

        uploatAbortedmessage: "Your file upload process is aborted!",

        uploadButtonText: "Upload AadharCard",

        uploadedMessage: "Your file is uploaded successfully",

        uploadFailedMessage: "Your file upload procee is failed.",

        // implement "validationError" options with init validationStatus = "pendind" and after that based on proper validation make it valid or invalid


        visible: true,
        width: 500,

    }).dxFileUploader("instance");

    // perform different ttype methods into above instance


    $("#reset").dxButton({

    });

    $("#submit").dxButton({

    });
});
// abortUpload
// accept
// chunlSize, which parameter take as default
// dialogTrigger
// dropZone
// process
// uploadChunk
// uploadCustomData
// uploadFile
// uploadHeaders
// uploadMethod
// uploadUrl
// value

// * methods and events are remainig


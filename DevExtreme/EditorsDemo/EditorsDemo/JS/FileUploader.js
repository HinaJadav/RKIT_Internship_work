$(function () {
    let fileUploadInstance = $("#aadharCard").dxFileUploader({
        accessKey: 'a',
        allowCanceling: true,
        allowdFileExtention: [".png", ".txt", ".pdf"],
       
       

        focusStateEnabled: true,
        hint: "Enter your AadharCard.",
        hoverStateEnable: true,
        inputAttr: { 'aria-label': 'AadharCardUploader' },
        maxFileSize: 1024 * 1024,
        invalidFileExtensionMessage: "Your file's extention in not supported",
        minFileSize: 1024,
        invalidMaxFileSizeMessage: "Your  select file size is more then default file size max-limit.",

        invalidMinFileSizeMessage: "Your  select file size is less then default file size min-limit.",

        accept: "image/png, application/pdf, text/plain", // it only affect file selection dialog (user can still drap & drop unsupported files OR they can enter manually file path) --> solution into below "process" option

        labelText: "Select your AadharCard from Here!",
        multiple: false,
        name: "fileUploader",

        // --------------
        // Chunk Uploading allows large files to be split into smaller parts, reducing the risk of upload failures.

        // chunkSize: Defines the chunk size (in bytes) for file uploads.
        // Default value: 0 (means entire file uploaded in one request)
        chunkSize: 1024 * 10, // 512 KB per chunk

        // uploadChunk: Handles chuncked file uploads manually
        uploadChunk: function (file, uploadInfo) {
            console.log(`Uploading chunk ${uploadInfo.chunkIndex + 1} of ${uploadInfo.chunkCount}`);
        },
        // ---------------


        // uploadCustomData: Sends additional data with each file upload request
        uploadCustomData: { userId: 101 },

       
        // uploadUrl: specifies URL to which the file will be uploaded
        // use to set the server endpoint to which the file will be sent
        uploadUrl: "https://example.com/upload", // cheange this as per your requirement

        // uploadMethod: specifies the HTTp method to use when uploading files (input type: POST(default), PUT)
        uploadMethod: "PUT", 

        
        
        // value: get or set the selected files
        value: [], // Bind the selected files  



        // Specifies an external element that triggers the file selection dialog.
        dialogTrigger: "#upload",

        // Defines a drop zone where users can drag and drop files
        dropZone: "#drop-area",

        // It allows to modify, validate or preprocess files before they are uploaded
        validationStatus :"pending",  // Initialize validation status

        process: function (file) {
            return new Promise((resolve, reject) => {
                if (file.size > 1024 * 1024 * 1024) {
                    validationStatus = "invalid";  // File size is too large
                    reject("File size exceeds the limit.");
                } else if (file.type !== "image/png") {
                    validationStatus = "invalid";  // Invalid file type
                    reject("Only PNG files are allowed.");
                } else {
                    validationStatus = "valid";  // File is valid
                    resolve(file);  // Proceed with the upload
                }
            });
        },

        // event 
        onBeforeSend: function (e) {
            const file = e.file;
            const confirmation = confirm(`Do you want to upload the file ${file.name} of size ${file.size / 1024} KB?`);
            if (!confirmation) {
                e.cancel = true;
            }
        },


        onContentReady: function () {
            alert("Your Documentation prosess page ready!");
        },

        onDropZoneEnter: function () {
            $("#hintMessage").text("Drop your file here to upload!").show();
        },

        onDropZoneLeave: function () {
            $("#hintMessage").hide();
        },

        onFileUploaded: function () {
            alert("File uploaded successfully!");
            $("#successPopup").dxPopup("show");
        },

        // this event fired during the file upload, it used to tracked the upload process of files in chunks
        // e.bytesUploaded: Number of bytes uploaded so far
        // e.file.size: Total file size
        onProcess: function (e) {
            let process = (e.bytesUploaded / e.file.size) * 100;

            $("#process").css("width", process + "%");
        },


        onUploadAborted: function () {
            location.reload();
        },

        onUploaded: function () {
            $("#submit").focus();
        },

        onUploadError: function (e) {
            alert("Upload failed: " + e.error.message);
            $("#errorMessage").text("Upload failed! Please try again.").show();
        },

        onUploadStarted: function () {
            $("#loadingSpinner").show();
        },

       

        onValueChanged: function (e) {
            var file = e.value[0];
            if (file) {
                if (file.size > 1024 * 1024 * 1024) {  // File size exceeds 2MB
                    alert("File size should not exceed 2MB.");
                    $("#aadharCard").dxFileUploader("reset");  // Reset the file uploader

                    // Hide the readyMessage if file is invalid
                    $("#readyMessage").hide();
                    $("#filePreview").text("");  // Clear the file preview
                } else {  // File is valid
                    $("#filePreview").text("Selected file: " + file.name);  // Show the file preview
                    $("#readyMessage").text("Now you can upload your AadharCard.");  // Display the ready message
                    $("#readyMessage").show();  // Show the message
                }
            } else {
                // No file selected, hide the readyMessage
                $("#readyMessage").hide();
                $("#filePreview").text("");  // Clear the file preview
            }
        },



        // cancel an ongoing file upload. 
        // It is a function that gets called when an upload is aborted manually or due to some condition.
        abortUpload: function (file, uploadInfo) {
            console.log("Upload aborted for:", file.name);
        },

        
        selectButtonText: "Select AadharCard",

        showFileList: true, // how can i see it's effect

        tabIndex: 1,

        uploadAbortedmessage: "Your file upload process is aborted!",

        uploadButtonText: "Upload AadharCard",

        uploadedMessage: "Your file is uploaded successfully",

        uploadFailedMessage: "Your file upload procee is failed.",


        visible: true,
        width: 500,

    }).dxFileUploader("instance");

    $("#cancelUpload").dxButton({
        text: "Cancel Upload",
        type: "normal",
        stylingMode: "contained",
        onClick: function () {
            // Abort the upload
            fileUploadInstance.option("value").forEach(file => {
                fileUploadInstance.abortUpload(file);
            });

            // Show a message to the user
            alert("Your file upload process is aborted.");
        }
    });



    $("#reset").dxButton({
        text: "Reset",
        type: "normal",
        stylingMode: "contained",
        onClick: function () {
            // Reset the file uploader to clear the selected file
            fileUploadInstance.reset(); // Clears the selected file from the uploader
            $("#filePreview").text(""); // Clear the file preview display (if you have it)
            $("#process").css("width", "0%"); // Reset the progress bar
            alert("File selection has been reset.");
        }
    });


    $("#submit").dxButton({
        text: "Submit",
        type: "normal",
        stylingMode: "contained",
        onClick: function () {
            // Check if a file has been selected and uploaded
            const uploadedFile = fileUploadInstance.option("value")[0]; // Get the first uploaded file

            if (!uploadedFile) {
                alert("Please upload a file first.");
                return;
            }

            // If file is uploaded, proceed with submission
            alert(`File '${uploadedFile.name}' has been uploaded successfully.`);

            
        }
    });
});
// abortUpload

// accept :  limits file selection in the file dialog.
/*
File Type	MIME Type
PNG Image	image / png
JPEG Image	image / jpeg
GIF Image	image / gif
PDF Document	application / pdf
Plain Text	text / plain
*/


// allowedFileExtensions: ensures invalid files are not uploaded


// uploadCustomData
// uploadFile
// uploadHeaders
// uploadMethod
// uploadUrl
// value

// * methods and events are remainig


// difference betweeen "accept" and "allowedFileExtensions"

// accept:
// browser level restriction
// not work with drag and drop
// not prevent file upload
// input type: fileExtention or MIME type

// allowedFileExtensions:
// widget level validation
// works with drag & drop
// input: file extention only
// it can prevent upload

